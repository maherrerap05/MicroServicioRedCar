using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IFacturaRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<FacturaEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<FacturaEntity?> ObtenerPorIdAsync(int id_factura, CancellationToken cancellationToken = default);

        Task<FacturaEntity?> ObtenerParaActualizarAsync(int id_factura, CancellationToken cancellationToken = default);

        Task<FacturaEntity?> ObtenerPorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(FacturaEntity factura, CancellationToken cancellationToken = default);

        void Actualizar(FacturaEntity factura);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default);
    }
}