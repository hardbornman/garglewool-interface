using Abp.Authorization;
using GargleWool.Core.Authorization.Roles;
using GargleWool.Core.Authorization.Users;
using GargleWool.Core.MultiTenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace GargleWool.Core.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            ISystemClock systemClock)
            : base(
                options,
                signInManager,
                systemClock)
        {
        }
    }
}