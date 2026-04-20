using MicroServicio.RedCar.Business.DTOs.Vehiculo;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IVehiculoService
    {
        // =========================
        // COMANDOS
        // =========================
        Task<VehiculoResponse> CrearAsync(CrearVehiculoRequest request, CancellationToken cancellationToken = default);

        Task<VehiculoResponse> ActualizarAsync(ActualizarVehiculoRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_vehiculo, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // CONSULTAS
        // =========================
        Task<VehiculoResponse> ObtenerPorIdAsync(int id_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoResponse> ObtenerPorGuidAsync(Guid vehiculo_guid, CancellationToken cancellationToken = default);

        Task<VehiculoResponse> ObtenerPorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoResponse> ObtenerPorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<VehiculoResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<VehiculoResponse>> BuscarAsync(VehiculoFiltroRequest request, CancellationToken cancellationToken = default);

        // =========================
        // DISPONIBILIDAD
        // =========================
        Task<IReadOnlyList<VehiculoResponse>> ObtenerDisponiblesAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<VehiculoResponse>> ObtenerDisponiblesPorCategoriaAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            int id_categoria_vehiculo,
            CancellationToken cancellationToken = default);

        Task<bool> EstaDisponibleAsync(
            int id_vehiculo,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default);
    }
}