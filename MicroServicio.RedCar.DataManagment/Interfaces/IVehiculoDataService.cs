using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IVehiculoDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<VehiculoDataModel?> ObtenerPorIdAsync(int id_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoDataModel?> ObtenerPorGuidAsync(Guid vehiculo_guid, CancellationToken cancellationToken = default);

        Task<VehiculoDataModel?> ObtenerPorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoDataModel?> ObtenerPorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<VehiculoDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<VehiculoDataModel>> BuscarAsync(VehiculoFiltroDataModel filtro, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<VehiculoDataModel>> ObtenerDisponiblesAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<VehiculoDataModel>> ObtenerDisponiblesPorCategoriaAsync(
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

        // =========================
        // COMANDOS
        // =========================
        Task<VehiculoDataModel> CrearAsync(VehiculoDataModel model, CancellationToken cancellationToken = default);

        Task<VehiculoDataModel?> ActualizarAsync(VehiculoDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_vehiculo, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default);

        Task<bool> ExistePorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default);
    }
}