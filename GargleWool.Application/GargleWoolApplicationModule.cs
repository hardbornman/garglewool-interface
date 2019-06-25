using Abp.Modules;
using Abp.Reflection.Extensions;
using GargleWool.Core;

namespace GargleWool.Application
{
    [DependsOn(
        typeof(GargleWoolCoreModule))]
    public class GargleWoolApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GargleWoolApplicationModule).GetAssembly());
        }
    }
}