using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Framework.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();

            switch (exception)
            {
                case Exception ex when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized; 
                    break;

                case DomainException ex when exceptionType == typeof(DomainException):
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = ex.Code;
                    break;

                case ServiceException ex when exceptionType == typeof(ServiceException):
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = ex.Code;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var reponse = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(reponse);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;

            return httpContext.Response.WriteAsync(payload);
        }
    }
}
