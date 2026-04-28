using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Vehiculo;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de vehículos para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Expone consulta de disponibilidad y detalle de vehículo.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/vehiculos")]
public class VehiculosBookingController : ControllerBase
{
    private readonly IVehiculoService _vehiculoService;

    public VehiculosBookingController(IVehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }

    /// <summary>
    /// Lista vehículos disponibles según localización y fechas de recogida/devolución.
    /// Punto de entrada principal del catálogo del marketplace.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<VehiculoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerDisponibles(
        [FromQuery] int id_localizacion_recogida,
        [FromQuery] DateTime fecha_hora_recogida,
        [FromQuery] DateTime fecha_hora_devolucion,
        CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerDisponiblesAsync(
            id_localizacion_recogida,
            fecha_hora_recogida,
            fecha_hora_devolucion,
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<VehiculoResponse>>.Ok(result, "Consulta de disponibilidad exitosa."));
    }

    /// <summary>
    /// Detalle completo de un vehículo específico por ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<VehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    /// <summary>
    /// Verifica si un vehículo específico sigue disponible para las fechas dadas.
    /// Se invoca justo antes de confirmar la reserva.
    /// </summary>
    [HttpGet("{id:int}/disponible")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EstaDisponible(
        int id,
        [FromQuery] DateTime fecha_hora_recogida,
        [FromQuery] DateTime fecha_hora_devolucion,
        CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.EstaDisponibleAsync(
            id,
            fecha_hora_recogida,
            fecha_hora_devolucion,
            cancellationToken);

        return Ok(ApiResponse<bool>.Ok(result, "Consulta de disponibilidad exitosa."));
    }
}