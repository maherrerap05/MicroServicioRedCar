using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IReservaDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<ReservaDataModel?> ObtenerPorIdAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<ReservaDataModel?> ObtenerPorGuidAsync(Guid guid_reserva, CancellationToken cancellationToken = default);

        Task<ReservaDataModel?> ObtenerPorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<ReservaDataModel>> BuscarAsync(ReservaFiltroDataModel filtro, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaDataModel>> ObtenerHistorialPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaDataModel>> ObtenerReservasActivasAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaDataModel>> ObtenerReservasPorVehiculoAsync(int id_vehiculo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaExtraDataModel>> ObtenerExtrasPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaConductorDataModel>> ObtenerConductoresPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<ReservaDataModel> CrearAsync(
            ReservaDataModel model,
            IReadOnlyList<ReservaExtraDataModel>? extras = null,
            IReadOnlyList<ReservaConductorDataModel>? conductores = null,
            CancellationToken cancellationToken = default);

        Task<ReservaDataModel?> ActualizarAsync(
            ReservaDataModel model,
            IReadOnlyList<ReservaExtraDataModel>? extras = null,
            IReadOnlyList<ReservaConductorDataModel>? conductores = null,
            CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(
            int id_reserva,
            string usuario,
            string? motivo,
            CancellationToken cancellationToken = default);

        Task AprobarConductoresYExtrasAsync(
            int id_reserva,
            string modificado_por_usuario,
            CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default);
    }
}