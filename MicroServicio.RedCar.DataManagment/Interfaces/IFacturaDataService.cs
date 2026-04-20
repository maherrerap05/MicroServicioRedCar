using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IFacturaDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<FacturaDataModel?> ObtenerPorIdAsync(int id_factura, CancellationToken cancellationToken = default);

        Task<FacturaDataModel?> ObtenerPorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<FacturaDataModel>> BuscarAsync(FacturaFiltroDataModel filtro, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaDataModel>> ObtenerPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaDataModel>> ObtenerFacturasActivasAsync(CancellationToken cancellationToken = default);

        Task<FacturaDataModel?> ObtenerFacturaPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaDataModel>> ObtenerPorEstadoAsync(string estado, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<FacturaDataModel> CrearAsync(FacturaDataModel model, CancellationToken cancellationToken = default);

        Task<FacturaDataModel?> ActualizarAsync(FacturaDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_factura, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default);
    }
}