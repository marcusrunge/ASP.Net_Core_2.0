using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
{
    public class EnvironmentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        public EnvironmentMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = Stopwatch.StartNew();
            context.Response.Headers.Add("X-HostingEnvironmentName", new[] { _env.EnvironmentName });
            await _next(context);
            if (_env.IsDevelopment() && context.Response.ContentType != null && context.Response.ContentType.Equals("text/html"))
            {
                await context.Response.WriteAsync($"<p>From {_env.EnvironmentName} in {timer.ElapsedMilliseconds}");
            }
        }
    }

    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UseEnvironmentMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<EnvironmentMiddleware>();
        }
    }
}
