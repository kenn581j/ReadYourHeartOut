using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ReadYourHeartOut.Utilities
{
    public class MiddlewareForTheSakeOfMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MiddlewareForTheSakeOfMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;

            _logger = logger.CreateLogger("CustomMiddleware");
        }

        public Task Invoke(HttpContext httpContext)
        {
            _logger.LogDebug("Custom Middleware Invoked - Yey");

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareForTheSakeOfMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareForTheSakeOfMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareForTheSakeOfMiddleware>();
        }
    }
}
