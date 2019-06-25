using Abp.MultiTenancy;
using GargleWool.Core.Authorization.Users;

namespace GargleWool.Core.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {

        }
    }
}