using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Cliente;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/clientes")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ClienteResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ClienteResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{clienteGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid clienteGuid, CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerPorGuidAsync(clienteGuid, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("identificacion/{numeroIdentificacion}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorIdentificacion(string numeroIdentificacion, CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerPorIdentificacionAsync(numeroIdentificacion, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("correo/{correo}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCorreo(string correo, CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerPorCorreoAsync(correo, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Crear([FromBody] CrearClienteRequest request, CancellationToken cancellationToken)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "API";

        var result = await _clienteService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Cliente creado exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarClienteRequest request, CancellationToken cancellationToken)
    {
        request.id_cliente = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "API";

        var result = await _clienteService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Cliente actualizado exitosamente."));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> EliminarLogico(int id, [FromQuery] string? motivo, CancellationToken cancellationToken)
    {
        var usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        await _clienteService.EliminarLogicoAsync(id, usuario, motivo, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Cliente eliminado lógicamente."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<ClienteResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Buscar([FromBody] ClienteFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _clienteService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<ClienteResponse>>.Ok(result, "Consulta paginada exitosa."));
    }
}