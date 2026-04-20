using MicroServicio.RedCar.Business.DTOs.Auth;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}