using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Conductor;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de conductores para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Expone búsqueda por número de identificación y creación de conductor nuevo durante la reserva.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/conductores")]
public class ConductoresBookingController : ControllerBase
{
    private readonly IConductorService _conductorService;

    public ConductoresBookingController(IConductorService conductorService)
    {
        _conductorService = conductorService;
    }

    /// <summary>
    /// Busca un conductor por número de identificación (cédula/RUC).
    /// Si existe, devuelve sus datos para autocompletar el formulario.
    /// Si no existe, el frontend habilita el registro de uno nuevo.
    /// </summary>
    [HttpGet("identificacion/{numeroIdentificacion}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorIdentificacion(
        string numeroIdentificacion,
        CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerPorIdentificacionAsync(
            numeroIdentificacion, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Consulta exitosa."));
    }

    /// <summary>
    /// Crea un nuevo conductor durante el proceso de reserva del marketplace.
    /// El usuario auditado se registra como BOOKING_WEB ya que es una acción anónima.
    /// El numero_licencia se asigna automáticamente igual al numero_identificacion
    /// dado que en Ecuador ambos valores son equivalentes.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear(
        [FromBody] CrearConductorRequest request,
        CancellationToken cancellationToken)
    {
        // En Ecuador el número de licencia es igual al número de identificación.
        // Se asigna automáticamente para no exponer este campo al usuario.
        if (string.IsNullOrWhiteSpace(request.numero_licencia))
            request.numero_licencia = request.numero_identificacion;

        // En el marketplace no hay usuario autenticado.
        // Se registra el origen como BOOKING_WEB para trazabilidad.
        request.creado_por_usuario = "BOOKING_WEB";
        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "WEB";

        var result = await _conductorService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Conductor creado exitosamente."));
    }
}