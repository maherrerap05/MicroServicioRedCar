using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Reserva;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reservas")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{reservaGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid reservaGuid, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorGuidAsync(reservaGuid, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigoReserva}")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoReserva, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorCodigoAsync(codigoReserva, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("cliente/{idCliente:int}/historial")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerHistorialPorCliente(int idCliente, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerHistorialPorClienteAsync(idCliente, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta de historial exitosa."));
    }

    [HttpGet("activas")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerReservasActivas(CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerReservasActivasAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta de reservas activas exitosa."));
    }

    [HttpGet("vehiculo/{idVehiculo:int}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerReservasPorVehiculo(int idVehiculo, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerReservasPorVehiculoAsync(idVehiculo, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta por vehículo exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<ReservaResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] ReservaFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _reservaService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<ReservaResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearReservaRequest request, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.creado_por_usuario = usuario;
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "MicroServicio.RedCar.Api";

        foreach (var conductor in request.conductores)
        {
            conductor.creado_por_usuario = usuario;
            conductor.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            conductor.origen_registro = "MicroServicio.RedCar.Api";
        }

        foreach (var extra in request.extras)
        {
            extra.creado_por_usuario = usuario;
            extra.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            extra.origen_registro = "MicroServicio.RedCar.Api";
        }

        var result = await _reservaService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva creada exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarReservaRequest request, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.id_reserva = id;
        request.modificado_por_usuario = usuario;
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "MicroServicio.RedCar.Api";

        foreach (var conductor in request.conductores)
        {
            conductor.creado_por_usuario = usuario;
            conductor.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            conductor.origen_registro = "MicroServicio.RedCar.Api";
        }

        foreach (var extra in request.extras)
        {
            extra.creado_por_usuario = usuario;
            extra.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            extra.origen_registro = "MicroServicio.RedCar.Api";
        }

        var result = await _reservaService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva actualizada exitosamente."));
    }

    [HttpPost("{id:int}/confirmar")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Confirmar(int id, CancellationToken cancellationToken)
    {
        var request = new ConfirmarReservaRequest
        {
            id_reserva = id,
            modificado_por_usuario =
                User.Identity?.Name ??
                User.FindFirst("unique_name")?.Value ??
                "api_user",
            modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString(),
            servicio_origen = "MicroServicio.RedCar.Api"
        };

        var result = await _reservaService.ConfirmarAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva confirmada exitosamente."));
    }

    [HttpPost("{id:int}/cancelar")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancelar(
        int id,
        [FromBody] CancelarReservaRequest request,
        CancellationToken cancellationToken)
    {
        request.id_reserva = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "MicroServicio.RedCar.Api";

        var result = await _reservaService.CancelarAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva cancelada exitosamente."));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarLogico(int id, [FromQuery] string? motivo, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        await _reservaService.EliminarLogicoAsync(id, usuario, motivo, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Reserva eliminada lógicamente."));
    }
}