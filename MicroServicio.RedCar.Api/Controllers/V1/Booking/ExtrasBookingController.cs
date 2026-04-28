using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Extra;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de extras para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Solo expone el catálogo de extras disponibles para agregar a una reserva.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/extras")]
public class ExtrasBookingController : ControllerBase
{
    private readonly IExtraService _extraService;

    public ExtrasBookingController(IExtraService extraService)
    {
        _extraService = extraService;
    }

    /// <summary>
    /// Lista todos los extras disponibles para agregar a una reserva.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ExtraResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _extraService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ExtraResponse>>.Ok(result, "Consulta exitosa."));
    }
}