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
[Authorize]  // solo exige autenticación a nivel de clase, los roles se definen por método
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    // =========================
    // MÉTODO AUXILIAR
    // =========================

    /// <summary>
    /// Lee el claim id_cliente del JWT del usuario autenticado.
    /// Devuelve null si el usuario no tiene cliente asociado (ADMIN/VENDEDOR).
    /// </summary>
    private int? ObtenerIdClienteDelToken()
    {
        var claim = User.FindFirst("id_cliente")?.Value;
        return int.TryParse(claim, out var id) ? id : null;
    }

    // =========================
    // CONSULTAS
    // =========================

    // Solo personal interno
    [HttpGet]
    [Authorize(Roles = "ADMIN,VENDEDOR")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta exitosa."));
    }

    // ADMIN + VENDEDOR + CLIENTE
    // El cliente solo puede ver reservas que le pertenecen
    [HttpGet("{id:int}")]
    [Authorize(Roles = "ADMIN,VENDEDOR,CLIENTE")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorIdAsync(id, cancellationToken);

        if (User.IsInRole("CLIENTE"))
        {
            var idClienteToken = ObtenerIdClienteDelToken();

            if (idClienteToken is null || result.id_cliente != idClienteToken)
                return Forbid();
        }

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Consulta exitosa."));
    }

    // ADMIN + VENDEDOR + CLIENTE
    // El cliente solo puede ver reservas que le pertenecen
    [HttpGet("guid/{reservaGuid:guid}")]
    [Authorize(Roles = "ADMIN,VENDEDOR,CLIENTE")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid reservaGuid, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorGuidAsync(reservaGuid, cancellationToken);

        if (User.IsInRole("CLIENTE"))
        {
            var idClienteToken = ObtenerIdClienteDelToken();

            if (idClienteToken is null || result.id_cliente != idClienteToken)
                return Forbid();
        }

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Consulta exitosa."));
    }

    // ADMIN + VENDEDOR + CLIENTE
    // El cliente solo puede ver reservas que le pertenecen
    [HttpGet("codigo/{codigoReserva}")]
    [Authorize(Roles = "ADMIN,VENDEDOR,CLIENTE")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoReserva, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorCodigoAsync(codigoReserva, cancellationToken);

        if (User.IsInRole("CLIENTE"))
        {
            var idClienteToken = ObtenerIdClienteDelToken();

            if (idClienteToken is null || result.id_cliente != idClienteToken)
                return Forbid();
        }

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Consulta exitosa."));
    }

    // ADMIN + VENDEDOR + CLIENTE
    // El cliente solo puede consultar su propio historial
    [HttpGet("cliente/{idCliente:int}/historial")]
    [Authorize(Roles = "ADMIN,VENDEDOR,CLIENTE")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObtenerHistorialPorCliente(int idCliente, CancellationToken cancellationToken)
    {
        if (User.IsInRole("CLIENTE"))
        {
            var idClienteToken = ObtenerIdClienteDelToken();

            if (idClienteToken is null || idClienteToken != idCliente)
                return Forbid();
        }

        var result = await _reservaService.ObtenerHistorialPorClienteAsync(idCliente, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta de historial exitosa."));
    }

    // Solo personal interno
    [HttpGet("activas")]
    [Authorize(Roles = "ADMIN,VENDEDOR")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerReservasActivas(CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerReservasActivasAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta de reservas activas exitosa."));
    }

    // Solo personal interno
    [HttpGet("vehiculo/{idVehiculo:int}")]
    [Authorize(Roles = "ADMIN,VENDEDOR")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReservaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerReservasPorVehiculo(int idVehiculo, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerReservasPorVehiculoAsync(idVehiculo, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta por vehículo exitosa."));
    }

    // Solo personal interno
    [HttpPost("buscar")]
    [Authorize(Roles = "ADMIN,VENDEDOR")]
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

    // ADMIN + VENDEDOR + CLIENTE
    [HttpPost]
    [Authorize(Roles = "ADMIN,VENDEDOR,CLIENTE")]
    [ProducesResponseType(typeof(ApiResponse<ReservaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearReservaRequest request, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        // CORRECCIÓN: si es CLIENTE, forzar que solo pueda crear reservas
        // para su propio id_cliente, ignorando el que venga en el body.
        // El id_cliente se toma directamente del JWT para evitar suplantación.
        if (User.IsInRole("CLIENTE"))
        {
            var idClienteToken = ObtenerIdClienteDelToken();

            if (idClienteToken is null)
                return Forbid();

            request.id_cliente = idClienteToken.Value;
        }

        request.creado_por_usuario = usuario;
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "API";

        foreach (var conductor in request.conductores)
        {
            conductor.creado_por_usuario = usuario;
            conductor.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            conductor.origen_registro = "API";
        }

        foreach (var extra in request.extras)
        {
            extra.creado_por_usuario = usuario;
            extra.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            extra.origen_registro = "API";
        }

        var result = await _reservaService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva creada exitosamente."));
    }

    // Solo personal interno
    [HttpPut("{id:int}")]
    [Authorize(Roles = "ADMIN,VENDEDOR")]
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
        request.servicio_origen = "API";

        foreach (var conductor in request.conductores)
        {
            conductor.creado_por_usuario = usuario;
            conductor.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            conductor.origen_registro = "API";
        }

        foreach (var extra in request.extras)
        {
            extra.creado_por_usuario = usuario;
            extra.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            extra.origen_registro = "API";
        }

        var result = await _reservaService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva actualizada exitosamente."));
    }

    // Solo personal interno
    [HttpPost("{id:int}/confirmar")]
    [Authorize(Roles = "ADMIN,VENDEDOR")]
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
            servicio_origen = "API"
        };

        var result = await _reservaService.ConfirmarAsync(request, cancellationToken);

        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva confirmada exitosamente."));
    }

    // Solo ADMIN
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
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