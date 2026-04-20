using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<ReservaEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<ReservaEntity?> ObtenerPorIdAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<ReservaEntity?> ObtenerParaActualizarAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<ReservaEntity?> ObtenerPorGuidAsync(Guid guid_reserva, CancellationToken cancellationToken = default);

        Task<ReservaEntity?> ObtenerPorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(ReservaEntity reserva, CancellationToken cancellationToken = default);

        void Actualizar(ReservaEntity reserva);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default);
    }
}