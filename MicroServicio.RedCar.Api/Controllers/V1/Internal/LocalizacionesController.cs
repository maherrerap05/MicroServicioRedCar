using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Localizacion;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1.Internal;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/localizaciones")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class LocalizacionesController : ControllerBase
{
    private readonly ILocalizacionService _localizacionService;

    public LocalizacionesController(ILocalizacionService localizacionService)
    {
        _localizacionService = localizacionService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<LocalizacionResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _localizacionService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<LocalizacionResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<LocalizacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _localizacionService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<LocalizacionResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{localizacionGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<LocalizacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid localizacionGuid, CancellationToken cancellationToken)
    {
        var result = await _localizacionService.ObtenerPorGuidAsync(localizacionGuid, cancellationToken);

        return Ok(ApiResponse<LocalizacionResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigoLocalizacion}")]
    [ProducesResponseType(typeof(ApiResponse<LocalizacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoLocalizacion, CancellationToken cancellationToken)
    {
        var result = await _localizacionService.ObtenerPorCodigoAsync(codigoLocalizacion, cancellationToken);

        return Ok(ApiResponse<LocalizacionResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<LocalizacionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] LocalizacionFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _localizacionService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<LocalizacionResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<LocalizacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearLocalizacionRequest request, CancellationToken cancellationToken)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _localizacionService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<LocalizacionResponse>.Ok(result, "Localización creada exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<LocalizacionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarLocalizacionRequest request, CancellationToken cancellationToken)
    {
        request.id_localizacion = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "API";

        var result = await _localizacionService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<LocalizacionResponse>.Ok(result, "Localización actualizada exitosamente."));
    }

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

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        await _localizacionService.EliminarLogicoAsync(id, usuario, motivo, ip, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Localización eliminada lógicamente."));
    }
}