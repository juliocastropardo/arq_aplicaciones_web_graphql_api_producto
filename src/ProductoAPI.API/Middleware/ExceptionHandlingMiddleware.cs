using System.Net;
using System.Text.Json;
using FluentValidation;

namespace ProductoAPI_GraphQL.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Ignorar rutas de Swagger
        var path = context.Request.Path.Value?.ToLower() ?? "";
        if (path.StartsWith("/swagger") || path.StartsWith("/swaggerui"))
        {
            await _next(context);
            return;
        }

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no manejado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    message = "Error de validación",
                    errors = validationException.Errors.Select(e => new
                    {
                        property = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
                break;
            case KeyNotFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new { message = exception.Message });
                break;
            case ArgumentException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { message = exception.Message });
                break;
            default:
                result = JsonSerializer.Serialize(new
                {
                    message = "Error interno del servidor",
                    error = exception.Message
                });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}

