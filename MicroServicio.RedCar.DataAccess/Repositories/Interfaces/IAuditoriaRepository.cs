using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IAuditoriaRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<AuditoriaEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<AuditoriaEntity?> ObtenerPorIdAsync(long id_auditoria, CancellationToken cancellationToken = default);

        Task<AuditoriaEntity?> ObtenerPorGuidAsync(Guid auditoria_guid, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(AuditoriaEntity auditoria, CancellationToken cancellationToken = default);
    }
}