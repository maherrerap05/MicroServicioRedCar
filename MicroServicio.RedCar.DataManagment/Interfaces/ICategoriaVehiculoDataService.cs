using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface ICategoriaVehiculoDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<CategoriaVehiculoDataModel?> ObtenerPorIdAsync(int id_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoDataModel?> ObtenerPorGuidAsync(Guid categoria_vehiculo_guid, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoDataModel?> ObtenerPorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoDataModel?> ObtenerPorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CategoriaVehiculoDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<CategoriaVehiculoDataModel> CrearAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoDataModel?> ActualizarAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_categoria_vehiculo, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<bool> ExistePorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default);
    }
}