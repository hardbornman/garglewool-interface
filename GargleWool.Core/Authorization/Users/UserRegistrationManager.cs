using Abp.Domain.Services;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using GargleWool.Core.Authorization.Roles;

namespace GargleWool.Core.Authorization.Users
{
    public class UserRegistrationManager:DomainService
    {
        public IAbpSession AbpSession { get; set; }

        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
    }
}