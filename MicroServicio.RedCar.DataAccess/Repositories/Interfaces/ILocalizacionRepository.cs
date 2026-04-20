using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface ILocalizacionRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<LocalizacionEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<LocalizacionEntity?> ObtenerPorIdAsync(int id_localizacion, CancellationToken cancellationToken = default);

        Task<LocalizacionEntity?> ObtenerParaActualizarAsync(int id_localizacion, CancellationToken cancellationToken = default);

        Task<LocalizacionEntity?> ObtenerPorGuidAsync(Guid localizacion_guid, CancellationToken cancellationToken = default);

        Task<LocalizacionEntity?> ObtenerPorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(LocalizacionEntity localizacion, CancellationToken cancellationToken = default);

        void Actualizar(LocalizacionEntity localizacion);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default);
    }
}