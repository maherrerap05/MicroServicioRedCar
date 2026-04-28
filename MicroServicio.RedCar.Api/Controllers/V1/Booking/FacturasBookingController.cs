using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Factura;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de facturas para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Solo expone la creación de factura vinculada a una reserva confirmada.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/facturas")]
public class FacturasBookingController : ControllerBase
{
    private readonly IFacturaService _facturaService;

    public FacturasBookingController(IFacturaService facturaService)
    {
        _facturaService = facturaService;
    }

    /// <summary>
    /// Crea una nueva factura en estado ABI (Abierta) asociada a una reserva del marketplace.
    /// Se llama inmediatamente después de crear la reserva en el proceso de confirmación.
    /// El usuario auditado se registra como BOOKING_WEB para trazabilidad.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearFacturaRequest request, CancellationToken cancellationToken)
    {
        // En el marketplace no hay usuario autenticado.
        // Se registra el origen como BOOKING_WEB para trazabilidad completa.
        request.creado_por_usuario = "BOOKING_WEB";
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "WEB";

        var result = await _facturaService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Factura creada exitosamente."));
    }
}