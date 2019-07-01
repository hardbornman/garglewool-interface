using Abp.Modules;
using Abp.Reflection.Extensions;
using GargleWool.Core;
using GargleWool.Core.Authorization;

namespace GargleWool.Application
{
    [DependsOn(
        typeof(GargleWoolCoreModule))]
    public class GargleWoolApplicationModule : AbpModule
    {
        public override void PostInitialize()
        {
            Configuration.Authorization.Providers.Add<GargleWoolAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GargleWoolApplicationModule).GetAssembly());
        }
    }
}