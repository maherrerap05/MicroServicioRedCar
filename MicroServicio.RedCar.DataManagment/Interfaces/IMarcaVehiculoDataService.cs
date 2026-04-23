using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IMarcaVehiculoDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<MarcaVehiculoDataModel?> ObtenerPorIdAsync(int id_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoDataModel?> ObtenerPorGuidAsync(Guid marca_vehiculo_guid, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoDataModel?> ObtenerPorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoDataModel?> ObtenerPorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<MarcaVehiculoDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<MarcaVehiculoDataModel> CrearAsync(MarcaVehiculoDataModel model, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoDataModel?> ActualizarAsync(MarcaVehiculoDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_marca_vehiculo, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<bool> ExistePorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default);
    }
}