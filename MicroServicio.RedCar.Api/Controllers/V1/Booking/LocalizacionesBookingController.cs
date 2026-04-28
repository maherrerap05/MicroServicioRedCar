using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Localizacion;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de localizaciones para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Solo expone consultas de lectura necesarias para el buscador.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/localizaciones")]
public class LocalizacionesBookingController : ControllerBase
{
    private readonly ILocalizacionService _localizacionService;

    public LocalizacionesBookingController(ILocalizacionService localizacionService)
    {
        _localizacionService = localizacionService;
    }

    /// <summary>
    /// Lista todas las localizaciones disponibles para recogida y devolución.
    /// Usado para poblar el selector del buscador.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LocalizacionResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _localizacionService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<LocalizacionResponse>>.Ok(result, "Consulta exitosa."));
    }

    /// <summary>
    /// Detalle de una localización específica por ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<LocalizacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _localizacionService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<LocalizacionResponse>.Ok(result, "Consulta exitosa."));
    }
}