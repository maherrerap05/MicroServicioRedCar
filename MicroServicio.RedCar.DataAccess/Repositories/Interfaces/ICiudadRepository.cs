using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface ICiudadRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<CiudadEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<CiudadEntity?> ObtenerPorIdAsync(int id_ciudad, CancellationToken cancellationToken = default);

        Task<CiudadEntity?> ObtenerParaActualizarAsync(int id_ciudad, CancellationToken cancellationToken = default);

        Task<CiudadEntity?> ObtenerPorGuidAsync(Guid ciudad_guid, CancellationToken cancellationToken = default);

        Task<CiudadEntity?> ObtenerPorNombreAsync(string nombre_ciudad, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(CiudadEntity ciudad, CancellationToken cancellationToken = default);

        void Actualizar(CiudadEntity ciudad);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorNombreAsync(string nombre_ciudad, CancellationToken cancellationToken = default);
    }
}