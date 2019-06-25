using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using GargleWool.Core;

namespace GargleWool.EntityFrameworkCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(GargleWoolCoreModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule))]
    public class GargleWoolEntityFrameworkCoreModule : AbpModule
    {
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<GargleWoolDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        GargleWoolDbContextConfigure.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        GargleWoolDbContextConfigure.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GargleWoolEntityFrameworkCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            //if (!SkipDbSeed)
            //{
            //    SeedHelper.SeedHostDb(IocManager);
            //}
        }
    }
}