using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Business.DTOs.Cliente;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Booking;

/// <summary>
/// Endpoints públicos de clientes para el marketplace.
/// No requiere autenticación — acceso anónimo permitido.
/// Expone búsqueda por correo y creación de cliente nuevo durante el proceso de reserva.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/marketplace/clientes")]
public class ClientesBookingController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesBookingController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Busca un cliente por su correo electrónico.
    /// Si existe, devuelve sus datos para autocompletar el formulario.
    /// Si no existe, el frontend habilita el registro de uno nuevo.
    /// </summary>
    [HttpGet("correo/{correo}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorCorreo(string correo, CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerPorCorreoAsync(correo, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Consulta exitosa."));
    }

    /// <summary>
    /// Crea un nuevo cliente durante el proceso de reserva del marketplace.
    /// El usuario auditado se registra como BOOKING_WEB ya que es una acción anónima.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearClienteRequest request, CancellationToken cancellationToken)
    {
        // En el marketplace no hay usuario autenticado.
        // Se registra el origen como BOOKING_WEB para trazabilidad.
        request.creado_por_usuario = "BOOKING_WEB";
        request.modificacion_ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.servicio_origen = "WEB";

        var result = await _clienteService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Cliente creado exitosamente."));
    }
}