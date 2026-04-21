using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IVehiculoRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<VehiculoEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<VehiculoEntity?> ObtenerPorIdAsync(int id_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoEntity?> ObtenerParaActualizarAsync(int id_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoEntity?> ObtenerPorGuidAsync(Guid vehiculo_guid, CancellationToken cancellationToken = default);

        Task<VehiculoEntity?> ObtenerPorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default);

        Task<VehiculoEntity?> ObtenerPorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(VehiculoEntity vehiculo, CancellationToken cancellationToken = default);

        void Actualizar(VehiculoEntity vehiculo);

        Task<bool> ActualizarLocalizacionAsync(
            int id_vehiculo,
            int id_localizacion,
            string modificado_por_usuario,
            CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default);

        Task<bool> ExistePorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default);
    }
}