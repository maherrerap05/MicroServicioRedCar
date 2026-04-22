using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
        {
            var mensaje = ExtraerMensajeUnicidad(sqlEx.Message);

            await WriteErrorAsync(
                context,
                HttpStatusCode.Conflict,
                ApiErrorResponse.Fail(mensaje));
        }
        catch (Exception ex)
        {
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

        // ── RESPUESTAS HTTP SIN EXCEPCIÓN (401 / 403) ────────────────────────
        // ASP.NET Core escribe estos status codes directamente sin lanzar
        // excepciones, por lo que no se capturan en los catch anteriores.
        if (!context.Response.HasStarted)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                await WriteErrorAsync(
                    context,
                    HttpStatusCode.Unauthorized,
                    ApiErrorResponse.Fail("No autenticado. Debe enviar un token JWT válido en el header Authorization."));
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                await WriteErrorAsync(
                    context,
                    HttpStatusCode.Forbidden,
                    ApiErrorResponse.Fail("Acceso denegado. No tiene permisos suficientes para ejecutar esta acción."));
            }
        }
    }

    private static string ExtraerMensajeUnicidad(string sqlMessage)
    {
        if (sqlMessage.Contains("UQ_FACTURAS_NUMERO"))
            return "Ya existe una factura registrada con ese número de factura.";

        if (sqlMessage.Contains("UQ_FACTURAS_GUID"))
            return "Ya existe una factura registrada con ese identificador único.";

        if (sqlMessage.Contains("UQ_RESERVAS_CODIGO"))
            return "Ya existe una reserva registrada con ese código.";

        if (sqlMessage.Contains("UQ_RESERVAS_GUID"))
            return "Ya existe una reserva registrada con ese identificador único.";

        return "Ya existe un registro con los mismos datos únicos. Verifique los campos e intente nuevamente.";
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