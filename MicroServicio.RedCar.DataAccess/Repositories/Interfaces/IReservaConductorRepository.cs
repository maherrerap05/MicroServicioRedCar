using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IReservaConductorRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<ReservaConductorEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<ReservaConductorEntity?> ObtenerPorIdAsync(int id_reserva_conductor, CancellationToken cancellationToken = default);

        Task<ReservaConductorEntity?> ObtenerParaActualizarAsync(int id_reserva_conductor, CancellationToken cancellationToken = default);

        Task<ReservaConductorEntity?> ObtenerPorGuidAsync(Guid reserva_conductor_guid, CancellationToken cancellationToken = default);

        Task<ReservaConductorEntity?> ObtenerPorReservaYConductorAsync(int id_reserva, int id_conductor, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaConductorEntity>> ObtenerPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        // CORRECCIÓN: incluye eliminados para evitar violación de UQ_RES_X_CON_RESERVA_CONDUCTOR
        // al reactivar un conductor que fue removido previamente de la reserva.
        Task<IReadOnlyList<ReservaConductorEntity>> ObtenerTodosPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(ReservaConductorEntity reservaConductor, CancellationToken cancellationToken = default);

        void Actualizar(ReservaConductorEntity reservaConductor);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorReservaYConductorAsync(int id_reserva, int id_conductor, CancellationToken cancellationToken = default);
    }
}