using Abp.Authorization;
using GargleWool.Core.Authorization.Roles;
using GargleWool.Core.Authorization.Users;

namespace GargleWool.Core.Authorization
{
    public class PermissionChecker:PermissionChecker<Role,User>
    {
        public PermissionChecker(UserManager userManager) 
            : base(userManager)
        {
        }
    }
}