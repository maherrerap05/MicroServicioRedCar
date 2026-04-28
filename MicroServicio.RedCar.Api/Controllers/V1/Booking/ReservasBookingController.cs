using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Reserva;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de reservas para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Solo expone la creación de reserva durante el proceso de pago.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/reservas")]
public class ReservasBookingController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasBookingController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    /// <summary>
    /// Crea una nueva reserva desde el marketplace con estado APR (Aprobada).
    /// Llamado al momento de confirmar el pago. No requiere autenticación.
    /// El usuario auditado se registra como BOOKING_WEB para trazabilidad.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearReservaRequest request, CancellationToken cancellationToken)
    {
        // En el marketplace no hay usuario autenticado.
        // Se registra el origen como BOOKING_WEB para trazabilidad completa.
        request.creado_por_usuario = "BOOKING_WEB";
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "WEB";

        foreach (var conductor in request.conductores)
        {
            conductor.creado_por_usuario = "BOOKING_WEB";
            conductor.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            conductor.origen_registro = "WEB";
        }

        foreach (var extra in request.extras)
        {
            extra.creado_por_usuario = "BOOKING_WEB";
            extra.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            extra.origen_registro = "WEB";
        }

        var result = await _reservaService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva creada exitosamente."));
    }

    /// <summary>
    /// Confirma una reserva pendiente desde el marketplace.
    /// Cambia el estado de PEN a CON para permitir la facturación.
    /// </summary>
    [HttpPost("{id:int}/confirmar")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Confirmar(int id, CancellationToken cancellationToken)
    {
        var request = new ConfirmarReservaRequest
        {
            id_reserva = id,
            modificado_por_usuario = "BOOKING_WEB",
            modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString(),
            servicio_origen = "WEB"
        };

        var result = await _reservaService.ConfirmarAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva confirmada exitosamente."));
    }
}