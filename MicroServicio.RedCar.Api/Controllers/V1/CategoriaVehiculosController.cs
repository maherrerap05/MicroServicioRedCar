using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categorias-vehiculo")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class CategoriaVehiculosController : ControllerBase
{
    private readonly ICategoriaVehiculoService _categoriaVehiculoService;

    public CategoriaVehiculosController(ICategoriaVehiculoService categoriaVehiculoService)
    {
        _categoriaVehiculoService = categoriaVehiculoService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoriaVehiculoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos()
    {
        var result = await _categoriaVehiculoService.ObtenerTodosAsync();

        return Ok(ApiResponse<IReadOnlyList<CategoriaVehiculoResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var result = await _categoriaVehiculoService.ObtenerPorIdAsync(id);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Categoría de vehículo no encontrada."));

        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{categoriaVehiculoGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid categoriaVehiculoGuid)
    {
        var result = await _categoriaVehiculoService.ObtenerPorGuidAsync(categoriaVehiculoGuid);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Categoría de vehículo no encontrada."));

        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigoCategoriaVehiculo}")]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoCategoriaVehiculo)
    {
        var result = await _categoriaVehiculoService.ObtenerPorCodigoAsync(codigoCategoriaVehiculo);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Categoría de vehículo no encontrada."));

        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<CategoriaVehiculoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] CategoriaVehiculoFiltroRequest request)
    {
        var result = await _categoriaVehiculoService.BuscarAsync(request);

        return Ok(ApiResponse<DataPagedResult<CategoriaVehiculoResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearCategoriaVehiculoRequest request)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _categoriaVehiculoService.CrearAsync(request);

        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Categoría de vehículo creada exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCategoriaVehiculoRequest request)
    {
        request.id_categoria_vehiculo = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _categoriaVehiculoService.ActualizarAsync(request);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Categoría de vehículo no encontrada."));

        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Categoría de vehículo actualizada exitosamente."));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarLogico(int id, [FromQuery] string? motivo)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        var eliminado = await _categoriaVehiculoService.EliminarAsync(id, usuario, motivo);

        if (!eliminado)
            return NotFound(ApiErrorResponse.Fail("Categoría de vehículo no encontrada."));

        return Ok(ApiResponse<string>.Ok("OK", "Categoría de vehículo eliminada lógicamente."));
    }
}