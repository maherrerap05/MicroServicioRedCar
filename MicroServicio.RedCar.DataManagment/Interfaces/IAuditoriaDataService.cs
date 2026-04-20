using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IAuditoriaDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<AuditoriaDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<AuditoriaDataModel?> ObtenerPorIdAsync(long id_auditoria, CancellationToken cancellationToken = default);

        Task<AuditoriaDataModel?> ObtenerPorGuidAsync(Guid auditoria_guid, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<AuditoriaDataModel> RegistrarAsync(AuditoriaDataModel model, CancellationToken cancellationToken = default);
    }
}