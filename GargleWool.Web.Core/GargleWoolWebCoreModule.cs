using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using GargleWool.Application;
using GargleWool.Core;
using GargleWool.EntityFrameworkCore.EntityFrameworkCore;
using GargleWool.Web.Core.Authentication.JwtBearer;
using GargleWool.Web.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace GargleWool.Web.Core
{
    [DependsOn(
        typeof(GargleWoolApplicationModule),
        typeof(GargleWoolEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule)
    )]
    public class GargleWoolWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GargleWoolWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                GargleWoolConstants.ConnectionStringName
            );

            // TODO:将所有错误信息显示到客户端
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(GargleWoolApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();

            // TODO:读取Token配置

            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.Authority = _appConfiguration["Authentication:IdentityServer4:Authority"];
            tokenAuthConfig.ClientId = _appConfiguration["Authentication:IdentityServer4:ClientId"];
            tokenAuthConfig.Secret = _appConfiguration["Authentication:IdentityServer4:Secret"];
    

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GargleWoolWebCoreModule).GetAssembly());
        }
    }
}