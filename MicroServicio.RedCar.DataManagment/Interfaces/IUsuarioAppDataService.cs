using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IUsuarioAppDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<UsuarioAppDataModel?> ObtenerPorIdAsync(int id_usuario, CancellationToken cancellationToken = default);

        Task<UsuarioAppDataModel?> ObtenerPorGuidAsync(Guid usuario_guid, CancellationToken cancellationToken = default);

        Task<UsuarioAppDataModel?> ObtenerPorUserNameAsync(string userName, CancellationToken cancellationToken = default);

        Task<UsuarioAppDataModel?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<UsuarioAppDataModel> CrearAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default);

        Task<UsuarioAppDataModel?> ActualizarAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_usuario, string usuario, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorUserNameAsync(string userName, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default);
    }
}