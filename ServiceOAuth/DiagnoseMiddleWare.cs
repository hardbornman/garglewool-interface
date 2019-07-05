using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceOAuth.Controllers.Quickstart.Home;

namespace ServiceOAuth
{
    public class DiagnoseMiddleWare : IMiddleware, ITransientDependency
    {
        private readonly ILogger _logger;
        public DiagnoseMiddleWare(ILogger<DiagnoseMiddleWare> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var ctx = ActivatorUtilities.CreateFactory(typeof(HomeController), Type.EmptyTypes);
                var controller = ctx(context.RequestServices, null);
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogInformation(JsonConvert.SerializeObject(controller), null);
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug(ex, "", null);
            }

            await next(context);
        }
    }
}