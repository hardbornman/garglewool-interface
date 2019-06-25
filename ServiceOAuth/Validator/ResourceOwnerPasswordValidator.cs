using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using GargleWool.Application.Authorization;
using GargleWool.Core.Authorization;
using GargleWool.Core.Authorization.Users;
using GargleWool.Core.MultiTenancy;
using GargleWool.Web.Core.Authentication.External;
using GargleWool.Web.Core.Authentication.JwtBearer;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ServiceOAuth.Validator
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public IAbpSession AbpSession { get; set; }
        private readonly IConfiguration _appConfiguration;
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public ResourceOwnerPasswordValidator(
            IConfiguration appConfiguration,
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager)
        {
            _appConfiguration = appConfiguration;
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
        }


        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var loginResult = await GetLoginResultAsync(
                    context.UserName,
                    context.Password,
                    GetTenancyNameOrNull()
                );

                context.Result = new GrantValidationResult(
                    context.UserName,
                    "password",
                    CreateJwtClaims(loginResult.Identity)
                );
            }
            catch (Exception e)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, e.Message);
            }
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == JwtRegisteredClaimNames.Sub);

            claims.AddRange(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            });

            return claims;
        }
    }
}