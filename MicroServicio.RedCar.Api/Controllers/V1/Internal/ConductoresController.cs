using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Conductor;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1.Internal;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/conductores")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class ConductoresController : ControllerBase
{
    private readonly IConductorService _conductorService;

    public ConductoresController(IConductorService conductorService)
    {
        _conductorService = conductorService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ConductorResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ConductorResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{conductorGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid conductorGuid, CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerPorGuidAsync(conductorGuid, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigoConductor}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoConductor, CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerPorCodigoAsync(codigoConductor, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("identificacion/{numeroIdentificacion}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorIdentificacion(string numeroIdentificacion, CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerPorIdentificacionAsync(numeroIdentificacion, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("licencia/{numeroLicencia}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorLicencia(string numeroLicencia, CancellationToken cancellationToken)
    {
        var result = await _conductorService.ObtenerPorLicenciaAsync(numeroLicencia, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<ConductorResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] ConductorFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _conductorService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<ConductorResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearConductorRequest request, CancellationToken cancellationToken)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _conductorService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Conductor creado exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ConductorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarConductorRequest request, CancellationToken cancellationToken)
    {
        request.id_conductor = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _conductorService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<ConductorResponse>.Ok(result, "Conductor actualizado exitosamente."));
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

        await _conductorService.EliminarLogicoAsync(id, usuario, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Conductor eliminado lógicamente."));
    }
}