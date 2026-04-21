using MicroServicio.RedCar.Business.DTOs.Auth;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.DataManagement.Interfaces;

namespace MicroServicio.RedCar.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
                throw new ValidationException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ValidationException("La contraseña es obligatoria.");

            var usuario = await _unitOfWork.UsuarioAppRepository
                .ObtenerPorUserNameAsync(request.UserName, cancellationToken);

            if (usuario is null)
                throw new UnauthorizedBusinessException("Usuario o contraseña inválidos.");

            if (!usuario.activo)
                throw new UnauthorizedBusinessException("El usuario se encuentra inactivo.");

            // V1 temporal — en V2 implementar hashing real con BCrypt
            if (usuario.password_hash != request.Password)
                throw new UnauthorizedBusinessException("Usuario o contraseña inválidos.");

            return new LoginResponse
            {
                UserName = usuario.username,
                Correo = usuario.correo,
                Activo = usuario.activo,

                // AGREGADO: se incluye id_cliente en el response para que el AuthController
                // lo agregue como claim al JWT. Es 0 para ADMIN/VENDEDOR sin cliente asociado,
                // por lo que se convierte a null para evitar claims inválidos.
                IdCliente = usuario.id_cliente > 0 ? usuario.id_cliente : null,

                Roles = usuario.UsuarioRoles
                    .Where(ur => ur.activo && !ur.es_eliminado && ur.estado_usuario_rol == "ACT")
                    .Select(ur => ur.Rol.nombre_rol)
                    .Distinct()
                    .ToList(),

                Token = string.Empty,
                ExpirationUtc = DateTime.MinValue
            };
        }
    }
}