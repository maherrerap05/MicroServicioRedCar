using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IPaisRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<PaisEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<PaisEntity?> ObtenerPorIdAsync(int id_pais, CancellationToken cancellationToken = default);

        Task<PaisEntity?> ObtenerParaActualizarAsync(int id_pais, CancellationToken cancellationToken = default);

        Task<PaisEntity?> ObtenerPorGuidAsync(Guid pais_guid, CancellationToken cancellationToken = default);

        Task<PaisEntity?> ObtenerPorNombreAsync(string nombre_pais, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(PaisEntity pais, CancellationToken cancellationToken = default);

        void Actualizar(PaisEntity pais);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorNombreAsync(string nombre_pais, CancellationToken cancellationToken = default);
    }
}