using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.IdentityServer4;
using Castle.Facilities.Logging;
using GargleWool.Core.Authorization.Users;
using GargleWool.Core.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceOAuth.Configuration;
using ServiceOAuth.Extension;
using ServiceOAuth.Service.Profiles;
using ServiceOAuth.Validator;

namespace ServiceOAuth
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            _environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string cerfFile = $"{_environment.ContentRootPath}{Path.DirectorySeparatorChar}Certificates{Path.DirectorySeparatorChar}graglewool.pfx";
            var cert = new X509Certificate2(cerfFile, "zhuxinghan");

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // TODO:IdentityServer Config Add
            IdentityRegistrar.Register(services);

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(InMemoryConfiguration.IdentityResources())
                .AddInMemoryApiResources(InMemoryConfiguration.ApiResources())
                .AddInMemoryClients(InMemoryConfiguration.Clients())
                .AddAbpPersistedGrants<IAbpPersistedGrantDbContext>()
                .AddAbpIdentityServer<User>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();


            // Configure Abp and Dependency Injection
            return services.AddAbp<IdentityServerModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
