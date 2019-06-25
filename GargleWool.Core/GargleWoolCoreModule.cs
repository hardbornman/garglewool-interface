using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using GargleWool.Core.Authorization.Roles;
using GargleWool.Core.Authorization.Users;
using GargleWool.Core.MultiTenancy;
using GargleWool.Core.Timing;

namespace GargleWool.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class GargleWoolCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
        }

        public override void Initialize() => IocManager.RegisterAssemblyByConvention(typeof(GargleWoolCoreModule).GetAssembly());

        public override void PostInitialize() => IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
    }
}