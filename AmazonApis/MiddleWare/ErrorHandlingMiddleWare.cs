using AmazonApis.Errors;
using System;
using System.Net;
using System.Text.Json;

namespace AmazonApis.MiddleWare
{
    public class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleWare> _logger;
        private readonly IHostEnvironment _environment;

        public ErrorHandlingMiddleWare(RequestDelegate next, ILogger<ErrorHandlingMiddleWare> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";


                var response = _environment.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.StackTrace.ToString(), ex.Message) :
                    new ApiExceptionResponse(500);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy= JsonNamingPolicy.CamelCase
                };
                await context.Response.WriteAsJsonAsync(response, options);
            }
        }
    }

}
