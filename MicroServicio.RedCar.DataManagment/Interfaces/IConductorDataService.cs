using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IConductorDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<ConductorDataModel?> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default);

        Task<ConductorDataModel?> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default);

        Task<ConductorDataModel?> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);

        Task<ConductorDataModel?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<ConductorDataModel?> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ConductorDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<ConductorDataModel> CrearAsync(ConductorDataModel model, CancellationToken cancellationToken = default);

        Task<ConductorDataModel?> ActualizarAsync(ConductorDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_conductor, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<bool> ExistePorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);
    }
}