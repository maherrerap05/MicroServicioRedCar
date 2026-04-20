using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IUsuarioAppRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<UsuarioAppEntity?> ObtenerPorIdAsync(int id_usuario, CancellationToken cancellationToken = default);

        Task<UsuarioAppEntity?> ObtenerParaActualizarAsync(int id_usuario, CancellationToken cancellationToken = default);

        Task<UsuarioAppEntity?> ObtenerPorGuidAsync(Guid usuario_guid, CancellationToken cancellationToken = default);

        Task<UsuarioAppEntity?> ObtenerPorUserNameAsync(string userName, CancellationToken cancellationToken = default);

        Task<UsuarioAppEntity?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(UsuarioAppEntity usuario, CancellationToken cancellationToken = default);

        void Actualizar(UsuarioAppEntity usuario);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorUserNameAsync(string userName, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default);
    }
}