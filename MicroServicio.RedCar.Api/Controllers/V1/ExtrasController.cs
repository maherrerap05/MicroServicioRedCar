using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Extra;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/extras")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class ExtrasController : ControllerBase
{
    private readonly IExtraService _extraService;

    public ExtrasController(IExtraService extraService)
    {
        _extraService = extraService;
    }

    // =========================
    // CONSULTAS
    // =========================

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ExtraResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerTodos(CancellationToken cancellationToken)
    {
        var result = await _extraService.ObtenerTodosAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<ExtraResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ExtraResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(int id, CancellationToken cancellationToken)
    {
        var result = await _extraService.ObtenerPorIdAsync(id, cancellationToken);

        return Ok(ApiResponse<ExtraResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("guid/{extraGuid:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ExtraResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorGuid(Guid extraGuid, CancellationToken cancellationToken)
    {
        var result = await _extraService.ObtenerPorGuidAsync(extraGuid, cancellationToken);

        return Ok(ApiResponse<ExtraResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("codigo/{codigoExtra}")]
    [ProducesResponseType(typeof(ApiResponse<ExtraResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCodigo(string codigoExtra, CancellationToken cancellationToken)
    {
        var result = await _extraService.ObtenerPorCodigoAsync(codigoExtra, cancellationToken);

        return Ok(ApiResponse<ExtraResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<ExtraResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromBody] ExtraFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _extraService.BuscarAsync(request, cancellationToken);

        return Ok(ApiResponse<DataPagedResult<ExtraResponse>>.Ok(result, "Consulta paginada exitosa."));
    }

    // =========================
    // COMANDOS
    // =========================

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ExtraResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearExtraRequest request, CancellationToken cancellationToken)
    {
        request.creado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "MicroServicio.RedCar.Api";

        var result = await _extraService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ExtraResponse>.Ok(result, "Extra creado exitosamente."));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ExtraResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarExtraRequest request, CancellationToken cancellationToken)
    {
        request.id_extra = id;
        request.modificado_por_usuario =
            User.Identity?.Name ??
            User.FindFirst("unique_name")?.Value ??
            "api_user";

        request.modificado_desde_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.origen_registro = "MicroServicio.RedCar.Api";

        var result = await _extraService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<ExtraResponse>.Ok(result, "Extra actualizado exitosamente."));
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

        await _extraService.EliminarLogicoAsync(id, usuario, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Extra eliminado lógicamente."));
    }
}