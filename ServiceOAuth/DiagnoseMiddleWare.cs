using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceOAuth.Controllers.Quickstart.Home;

namespace ServiceOAuth
{
    public class DiagnoseMiddleWare : IMiddleware, ITransientDependency
    {
        private readonly RequestDelegate _next;

        public DiagnoseMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var ctx = ActivatorUtilities.CreateFactory(typeof(HomeController), Type.EmptyTypes);
                var controller = ctx(context.RequestServices, null);
            }
            catch (Exception ex)
            {
                //
            }

            await next(context);
        }
    }
}