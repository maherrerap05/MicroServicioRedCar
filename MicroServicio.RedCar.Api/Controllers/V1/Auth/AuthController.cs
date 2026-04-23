using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MicroServicio.RedCar.Api.Models.Common;
using MicroServicio.RedCar.Api.Models.Settings;
using MicroServicio.RedCar.Business.DTOs.Auth;
using MicroServicio.RedCar.Business.Interfaces;

namespace MicroServicio.RedCar.Api.Controllers.V1.Auth;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtOptions)
    {
        _authService = authService;
        _jwtSettings = jwtOptions.Value;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,        result.UserName),
            new(JwtRegisteredClaimNames.UniqueName, result.UserName),
            new(JwtRegisteredClaimNames.Email,      result.Correo ?? string.Empty)
        };

        // AGREGADO: incluir id_cliente como claim solo si el usuario tiene cliente asociado.
        // Los usuarios ADMIN y VENDEDOR no tienen id_cliente, por lo que IdCliente es null.
        // Este claim es leído en ReservasController para restringir el acceso del CLIENTE
        // únicamente a sus propias reservas.
        if (result.IdCliente.HasValue)
        {
            claims.Add(new Claim("id_cliente", result.IdCliente.Value.ToString()));
        }

        claims.AddRange(result.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        result.Token = new JwtSecurityTokenHandler().WriteToken(token);
        result.ExpirationUtc = expiration;

        return Ok(ApiResponse<LoginResponse>.Ok(result, "Login exitoso."));
    }
}