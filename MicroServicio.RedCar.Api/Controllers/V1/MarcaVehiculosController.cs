using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.MarcaVehiculo;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marcas-vehiculo")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class MarcaVehiculosController : ControllerBase
{
    private readonly IMarcaVehiculoService _marcaVehiculoService;

    public MarcaVehiculosController(IMarcaVehiculoService marcaVehiculoService)
    {
        _marcaVehiculoService = marcaVehiculoService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<MarcaVehiculoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos()
    {
        var result = await _marcaVehiculoService.ObtenerTodosAsync();

        return Ok(ApiResponse<IReadOnlyList<MarcaVehiculoResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<MarcaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var result = await _marcaVehiculoService.ObtenerPorIdAsync(id);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Marca de vehículo no encontrada."));

        return Ok(ApiResponse<MarcaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{marcaVehiculoGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<MarcaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid marcaVehiculoGuid)
    {
        var result = await _marcaVehiculoService.ObtenerPorGuidAsync(marcaVehiculoGuid);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Marca de vehículo no encontrada."));

        return Ok(ApiResponse<MarcaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigoMarcaVehiculo}")]
    [ProducesResponseType(typeof(ApiResponse<MarcaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoMarcaVehiculo)
    {
        var result = await _marcaVehiculoService.ObtenerPorCodigoAsync(codigoMarcaVehiculo);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Marca de vehículo no encontrada."));

        return Ok(ApiResponse<MarcaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<MarcaVehiculoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] MarcaVehiculoFiltroRequest request)
    {
        var result = await _marcaVehiculoService.BuscarAsync(request);

        return Ok(ApiResponse<DataPagedResult<MarcaVehiculoResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<MarcaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearMarcaVehiculoRequest request)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "MicroServicio.RedCar.Api";

        var result = await _marcaVehiculoService.CrearAsync(request);

        return Ok(ApiResponse<MarcaVehiculoResponse>.Ok(result, "Marca de vehículo creada exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<MarcaVehiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarMarcaVehiculoRequest request)
    {
        request.id_marca_vehiculo = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "MicroServicio.RedCar.Api";

        var result = await _marcaVehiculoService.ActualizarAsync(request);

        if (result is null)
            return NotFound(ApiErrorResponse.Fail("Marca de vehículo no encontrada."));

        return Ok(ApiResponse<MarcaVehiculoResponse>.Ok(result, "Marca de vehículo actualizada exitosamente."));
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

        var eliminado = await _marcaVehiculoService.EliminarAsync(id, usuario, motivo);

        if (!eliminado)
            return NotFound(ApiErrorResponse.Fail("Marca de vehículo no encontrada."));

        return Ok(ApiResponse<string>.Ok("OK", "Marca de vehículo eliminada lógicamente."));
    }
}