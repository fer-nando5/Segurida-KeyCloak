using System.Eclaim.Application.Dto;
using System.Eclaim.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace System.Eclaim.API.Middlewares
{
    public class ExceptionHandLingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandLingMiddleware> _logger;

        public ExceptionHandLingMiddleware(RequestDelegate next, ILogger<ExceptionHandLingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, $"Domain exception ocurred: {ex.Message}");
                await HandleExceptionAsync(context, ex.Message, "DOMAIN_ERROR", HttpStatusCode.BadRequest);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, $"Unauthorized request: {ex.Message}");
                await HandleExceptionAsync(context, ex.Message, "Unauthorized", HttpStatusCode.Unauthorized);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, $"Application exception ocurred: {ex.Message}");
                await HandleExceptionAsync(context, ex.Message, "APP_ERROR", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Unexpected error ocurred: {ex.Message}");
                await HandleExceptionAsync(context, ex.Message, "SERVER_ERROR", HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, string errorCode, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = BaseResponse<string>.Failure(message, errorCode);
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            await context.Response.WriteAsync(json);

        }
    }

    public static class ExceptionHandLingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandLingMiddleware>();
        }
    }
}
