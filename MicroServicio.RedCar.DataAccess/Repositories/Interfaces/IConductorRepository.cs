using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IConductorRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<ConductorEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerParaActualizarAsync(int id_conductor, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(ConductorEntity conductor, CancellationToken cancellationToken = default);

        void Actualizar(ConductorEntity conductor);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<bool> ExistePorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);
    }
}