using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using GargleWool.Application;
using GargleWool.Application.Authorization;
using GargleWool.Core;
using GargleWool.Core.Authorization;
using GargleWool.Core.Authorization.Users;
using GargleWool.Web.Core.Authentication.External;
using GargleWool.Web.Core.Authentication.JwtBearer;
using GargleWool.Web.Core.Models.TokenAuth;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GargleWool.Web.Core.Controllers
{
    public class TokenAuthController : GargleWoolControllerBase
    {
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _tokenAuthConfiguration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IConfigurationRoot _appConfiguration;

        public TokenAuthController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration tokenAuthConfiguration,
            IExternalAuthConfiguration externalAuthConfiguration,
            UserRegistrationManager userRegistrationManager,
            IConfigurationRoot appConfiguration,
            IExternalAuthManager externalAuthManager)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _tokenAuthConfiguration = tokenAuthConfiguration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _userRegistrationManager = userRegistrationManager;
            _appConfiguration = appConfiguration;
            _externalAuthManager = externalAuthManager;
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var httpHandler = new HttpClientHandler();
            // Set TenantId
            httpHandler.CookieContainer.Add(
                new Uri(_tokenAuthConfiguration.Authority), new Cookie("Abp.TenantId", AbpSession.TenantId?.ToString()));

            var tokenClient = new HttpClient(httpHandler);
            var tokenResponse = await tokenClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{_tokenAuthConfiguration.Authority}/connect/token",

                ClientId = _tokenAuthConfiguration.ClientId,
                ClientSecret = _tokenAuthConfiguration.Secret,
                Scope = GargleWoolConstants.ScopeApi,

                UserName = model.UserNameOrEmailAddress,
                Password = model.Password,
            });

            if (tokenResponse.IsError)
            {
                throw new UserFriendlyException(tokenResponse.Error);
            }

            return new AuthenticateResultModel
            {
                AccessToken = tokenResponse.AccessToken,
                EncryptedAccessToken = GetEncryptedAccessToken(tokenResponse.AccessToken),
                ExpireInSeconds = (int)_tokenAuthConfiguration.Expiration.TotalSeconds,
            };

            //#region default auth

            //var loginResult = await _logInManager.LoginAsync(
            //    model.UserNameOrEmailAddress,
            //    model.Password,
            //    GetTenancyNameOrNull()
            //);

            //var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            //return new AuthenticateResultModel
            //{
            //    AccessToken = accessToken,
            //    EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
            //    ExpireInSeconds = (int)_tokenAuthConfiguration.Expiration.TotalSeconds,
            //    UserId = loginResult.User.Id
            //};

            //#endregion
        }

        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);

            var loginResult = await _logInManager.LoginAsync(
                new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                            ExpireInSeconds = (int)_tokenAuthConfiguration.Expiration.TotalSeconds,
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true,
                            };
                        }

                        // Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(
                            new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider),
                            GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
                            ExpireInSeconds = (int)_tokenAuthConfiguration.Expiration.TotalSeconds,
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                GargleWool.Core.Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId,
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private IEnumerable<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
            });

            return claims;
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                _tokenAuthConfiguration.Issuer,
                _tokenAuthConfiguration.Audience,
                claims,
                now,
                now.Add(expiration ?? _tokenAuthConfiguration.Expiration),
                _tokenAuthConfiguration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }


        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConstants.DefaultPassPhrase);
        }
    }
}