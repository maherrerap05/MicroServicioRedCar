using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface ICategoriaVehiculoRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<CategoriaVehiculoEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoEntity?> ObtenerPorIdAsync(int id_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoEntity?> ObtenerParaActualizarAsync(int id_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoEntity?> ObtenerPorGuidAsync(Guid categoria_vehiculo_guid, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoEntity?> ObtenerPorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<CategoriaVehiculoEntity?> ObtenerPorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(CategoriaVehiculoEntity categoriaVehiculo, CancellationToken cancellationToken = default);

        void Actualizar(CategoriaVehiculoEntity categoriaVehiculo);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default);

        Task<bool> ExistePorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default);
    }
}