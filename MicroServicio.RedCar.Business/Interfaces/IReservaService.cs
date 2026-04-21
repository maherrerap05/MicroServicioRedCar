using MicroServicio.RedCar.Business.DTOs.Reserva;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IReservaService
    {
        // =========================
        // COMANDOS
        // =========================
        Task<ReservaResponse> CrearAsync(CrearReservaRequest request, CancellationToken cancellationToken = default);

        Task<ReservaResponse> ActualizarAsync(ActualizarReservaRequest request, CancellationToken cancellationToken = default);

        Task<ReservaResponse> ConfirmarAsync(ConfirmarReservaRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_reserva, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // CONSULTAS
        // =========================
        Task<ReservaResponse> ObtenerPorIdAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<ReservaResponse> ObtenerPorGuidAsync(Guid guid_reserva, CancellationToken cancellationToken = default);

        Task<ReservaResponse> ObtenerPorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<ReservaResponse>> BuscarAsync(ReservaFiltroRequest request, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaResponse>> ObtenerHistorialPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaResponse>> ObtenerReservasActivasAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ReservaResponse>> ObtenerReservasPorVehiculoAsync(int id_vehiculo, CancellationToken cancellationToken = default);
    }
}