using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Vehiculo;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1.Internal;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/vehiculos")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoService _vehiculoService;

    public VehiculosController(IVehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerTodosAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<VehiculoResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{vehiculoGuid:guid}")]
    public async Task<IActionResult> ObtenerPorGuid(Guid vehiculoGuid, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerPorGuidAsync(vehiculoGuid, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigo}")]
    public async Task<IActionResult> ObtenerPorCodigo(string codigo, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerPorCodigoAsync(codigo, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("placa/{placa}")]
    public async Task<IActionResult> ObtenerPorPlaca(string placa, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerPorPlacaAsync(placa, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    // =========================
    // DISPONIBILIDAD
    // =========================

    [HttpGet("disponibles")]
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

    [HttpGet("disponibles/categoria")]
    public async Task<IActionResult> ObtenerDisponiblesPorCategoria(
        [FromQuery] int id_localizacion_recogida,
        [FromQuery] DateTime fecha_hora_recogida,
        [FromQuery] DateTime fecha_hora_devolucion,
        [FromQuery] int id_categoria_vehiculo,
        CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerDisponiblesPorCategoriaAsync(
            id_localizacion_recogida,
            fecha_hora_recogida,
            fecha_hora_devolucion,
            id_categoria_vehiculo,
            cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<VehiculoResponse>>.Ok(result, "Consulta de disponibilidad por categoría exitosa."));
    }

    [HttpGet("{id:int}/disponible")]
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

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearVehiculoRequest request, CancellationToken cancellationToken)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _vehiculoService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Vehículo creado exitosamente."));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarVehiculoRequest request, CancellationToken cancellationToken)
    {
        // Validar mismatch ANTES de pisar el id con el del path
        if (request.id_vehiculo != 0 && request.id_vehiculo != id)
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "El ID del vehículo en el body no coincide con el ID del path."
            });

        request.id_vehiculo = id;

        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _vehiculoService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Vehículo actualizado exitosamente."));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> EliminarLogico(int id, [FromQuery] string? motivo, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        await _vehiculoService.EliminarLogicoAsync(id, usuario, motivo, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Vehículo eliminado lógicamente."));
    }

    [HttpPost("buscar")]
    public async Task<IActionResult> Buscar([FromBody] VehiculoFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<VehiculoResponse>>.Ok(result, "Consulta paginada exitosa."));
    }
}