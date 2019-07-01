using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ServiceOAuth
{
    /// <summary>
    /// FabricRuntime 为每个服务类型实例创建此类的一个实例。 
    /// </summary>
    internal sealed class ServiceOAuth : StatelessService
    {
        public ServiceOAuth(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// 可选择性地替代以创建此服务实例的侦听器(如 TCP、http)。
        /// </summary>
        /// <returns>侦听器集合。</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        services => services
                                            .AddSingleton<StatelessServiceContext>(serviceContext))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .ConfigureLogging(logging=>
                                    {
                                        logging.ClearProviders();
                                        logging.AddDebug();
                                    })
                                    .Build();
                    }))
            };
        }
    }
}
