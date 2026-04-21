using System.Net;
using System.Text.Json;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.Exceptions;

namespace MicroServicio.RedCar.Api.Middleware;

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
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                ApiErrorResponse.Fail(ex.Message, ex.Errors));
        }
        catch (NotFoundException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.NotFound,
                ApiErrorResponse.Fail(ex.Message));
        }
        catch (UnauthorizedBusinessException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.Unauthorized,
                ApiErrorResponse.Fail(ex.Message));
        }
        catch (BusinessException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                ApiErrorResponse.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            // ── TEMPORAL DEBUG ───────────────────────────────────────────────
            // Expone el error real para diagnóstico. Revertir antes de producción.
            _logger.LogError(ex, "Error interno no controlado en {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            var mensaje = $"{ex.GetType().Name}: {ex.Message}";

            if (ex.InnerException is not null)
                mensaje += $" | Inner: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}";

            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                ApiErrorResponse.Fail(mensaje));
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        ApiErrorResponse response)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}