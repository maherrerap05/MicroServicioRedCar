using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de categorías de vehículos para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Solo expone el catálogo de categorías para poblar los filtros del buscador.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/categorias-vehiculo")]
public class CategoriasBookingController : ControllerBase
{
    private readonly ICategoriaVehiculoService _categoriaVehiculoService;

    public CategoriasBookingController(ICategoriaVehiculoService categoriaVehiculoService)
    {
        _categoriaVehiculoService = categoriaVehiculoService;
    }

    /// <summary>
    /// Lista todas las categorías de vehículos disponibles.
    /// Usado para poblar el filtro de categorías en el buscador del marketplace.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoriaVehiculoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos()
    {
        var result = await _categoriaVehiculoService.ObtenerTodosAsync();

        return Ok(ApiResponse<IReadOnlyList<CategoriaVehiculoResponse>>.Ok(result, "Consulta exitosa."));
    }
}