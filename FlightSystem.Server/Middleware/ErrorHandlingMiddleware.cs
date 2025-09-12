using System.Net;
using System.Text.Json;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.Server.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ApiResponseDto<object>
        {
            Success = false,
            Message = "Серверийн алдаа гарлаа",
            Errors = [exception.Message]
        };

        context.Response.StatusCode = exception switch
        {
            ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        response.Message = context.Response.StatusCode switch
        {
            (int)HttpStatusCode.BadRequest => "Хүсэлт буруу байна",
            (int)HttpStatusCode.Unauthorized => "Нэвтрэх эрхгүй",
            (int)HttpStatusCode.NotFound => "Хүссэн мэдээлэл олдсонгүй",
            _ => "Серверийн алдаа"
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
