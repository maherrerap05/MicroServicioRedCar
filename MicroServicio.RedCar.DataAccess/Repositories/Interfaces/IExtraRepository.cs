using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IExtraRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<ExtraEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<ExtraEntity?> ObtenerPorIdAsync(int id_extra, CancellationToken cancellationToken = default);

        Task<ExtraEntity?> ObtenerParaActualizarAsync(int id_extra, CancellationToken cancellationToken = default);

        Task<ExtraEntity?> ObtenerPorGuidAsync(Guid extra_guid, CancellationToken cancellationToken = default);

        Task<ExtraEntity?> ObtenerPorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default);


        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(ExtraEntity extra, CancellationToken cancellationToken = default);

        void Actualizar(ExtraEntity extra);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default);
    }
}