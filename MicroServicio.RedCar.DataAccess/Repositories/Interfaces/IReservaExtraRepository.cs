using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IReservaExtraRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<ReservaExtraEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<ReservaExtraEntity?> ObtenerPorIdAsync(int id_reserva_extra, CancellationToken cancellationToken = default);

        Task<ReservaExtraEntity?> ObtenerParaActualizarAsync(int id_reserva_extra, CancellationToken cancellationToken = default);

        Task<ReservaExtraEntity?> ObtenerPorGuidAsync(Guid reserva_extra_guid, CancellationToken cancellationToken = default);

        Task<ReservaExtraEntity?> ObtenerPorReservaYExtraAsync(int id_reserva, int id_extra, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaExtraEntity>> ObtenerPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(ReservaExtraEntity reservaExtra, CancellationToken cancellationToken = default);

        void Actualizar(ReservaExtraEntity reservaExtra);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorReservaYExtraAsync(int id_reserva, int id_extra, CancellationToken cancellationToken = default);
    }
}