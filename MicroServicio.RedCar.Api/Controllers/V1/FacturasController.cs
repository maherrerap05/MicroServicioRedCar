using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Factura;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/facturas")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class FacturasController : ControllerBase
{
    private readonly IFacturaService _facturaService;

    public FacturasController(IFacturaService facturaService)
    {
        _facturaService = facturaService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<FacturaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<FacturaResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{facturaGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid facturaGuid, CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerPorGuidAsync(facturaGuid, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("cliente/{idCliente:int}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<FacturaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorCliente(int idCliente, CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerPorClienteAsync(idCliente, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<FacturaResponse>>.Ok(result, "Consulta por cliente exitosa."));
    }

    [HttpGet("activas")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<FacturaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerFacturasActivas(CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerFacturasActivasAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<FacturaResponse>>.Ok(result, "Consulta de facturas activas exitosa."));
    }

    [HttpGet("reserva/{idReserva:int}")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorReserva(int idReserva, CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerPorReservaAsync(idReserva, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Consulta por reserva exitosa."));
    }

    [HttpGet("estado/{estado}")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<FacturaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorEstado(string estado, CancellationToken cancellationToken)
    {
        var result = await _facturaService.ObtenerPorEstadoAsync(estado, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<FacturaResponse>>.Ok(result, "Consulta por estado exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<FacturaResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] FacturaFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _facturaService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<FacturaResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearFacturaRequest request, CancellationToken cancellationToken)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "MicroServicio.RedCar.Api";

        var result = await _facturaService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Factura creada exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarFacturaRequest request, CancellationToken cancellationToken)
    {
        request.id_factura = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "MicroServicio.RedCar.Api";

        var result = await _facturaService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Factura actualizada exitosamente."));
    }

    [HttpPost("{id:int}/aprobar")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Aprobar(int id, CancellationToken cancellationToken)
    {
        var request = new AprobarFacturaRequest
        {
            id_factura = id,
            modificado_por_usuario =
                User.Identity?.Name ??
                User.FindFirst("unique_name")?.Value ??
                "api_user",
            modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString(),
            servicio_origen = "MicroServicio.RedCar.Api"
        };

        var result = await _facturaService.AprobarAsync(request, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Factura aprobada exitosamente."));
    }

    [HttpPost("{id:int}/anular")]
    [ProducesResponseType(typeof(ApiResponse<FacturaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Anular(
        int id,
        [FromBody] AnularFacturaRequest request,
        CancellationToken cancellationToken)
    {
        request.id_factura = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "MicroServicio.RedCar.Api";

        var result = await _facturaService.AnularAsync(request, cancellationToken);

        return Ok(ApiResponse<FacturaResponse>.Ok(result, "Factura anulada exitosamente."));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarLogico(int id, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        await _facturaService.EliminarLogicoAsync(id, usuario, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Factura eliminada lógicamente."));
    }
}