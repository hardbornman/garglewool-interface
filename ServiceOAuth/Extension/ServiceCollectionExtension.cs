using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceOAuth.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IdentityBuilder AddAbpIdentityServer(this IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant,User,Role>>()
        }
    }
}