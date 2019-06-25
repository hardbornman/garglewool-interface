using GargleWool.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ServiceOAuth.Extension
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}