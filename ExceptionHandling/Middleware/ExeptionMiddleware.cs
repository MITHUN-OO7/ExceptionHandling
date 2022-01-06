using ExceptionHandling.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExceptionHandling.Middleware
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExeptionMiddleware(RequestDelegate next, ILogger<ExeptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) : new ApiException(context.Response.StatusCode, "Internal Server Error");
                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, option);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
