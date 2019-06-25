using GargleWool.Core.Authorization;
using GargleWool.Core.Authorization.Roles;
using GargleWool.Core.Authorization.Users;
using GargleWool.Core.Editions;
using GargleWool.Core.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GargleWool.Core.Identity
{
    public class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}