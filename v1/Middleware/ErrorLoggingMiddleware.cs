using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using v1.Services;

namespace v1.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLogService _errorLogService;

        public ErrorLoggingMiddleware(RequestDelegate next, IErrorLogService errorLogService)
        {
            _next = next;
            _errorLogService = errorLogService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await _errorLogService.LogErrorAsync(ex);
                throw;
            }
        }
    }

    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
