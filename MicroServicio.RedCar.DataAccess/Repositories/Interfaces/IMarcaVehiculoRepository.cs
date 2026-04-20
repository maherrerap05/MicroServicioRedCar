using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IMarcaVehiculoRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<MarcaVehiculoEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<MarcaVehiculoEntity?> ObtenerPorIdAsync(int id_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoEntity?> ObtenerParaActualizarAsync(int id_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoEntity?> ObtenerPorGuidAsync(Guid marca_vehiculo_guid, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoEntity?> ObtenerPorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<MarcaVehiculoEntity?> ObtenerPorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(MarcaVehiculoEntity marcaVehiculo, CancellationToken cancellationToken = default);

        void Actualizar(MarcaVehiculoEntity marcaVehiculo);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default);

        Task<bool> ExistePorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default);
    }
}