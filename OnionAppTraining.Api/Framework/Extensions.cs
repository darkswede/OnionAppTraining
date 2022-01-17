using Microsoft.AspNetCore.Builder;
using OnionAppTraining.Api.Framework.Middleware;

namespace OnionAppTraining.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}
