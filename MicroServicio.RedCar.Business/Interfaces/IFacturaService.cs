using MicroServicio.RedCar.Business.DTOs.Factura;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IFacturaService
    {
        Task<FacturaResponse> CrearAsync(CrearFacturaRequest request, CancellationToken cancellationToken = default);

        Task<FacturaResponse> ActualizarAsync(ActualizarFacturaRequest request, CancellationToken cancellationToken = default);

        Task<FacturaResponse> ObtenerPorIdAsync(int id_factura, CancellationToken cancellationToken = default);

        Task<FacturaResponse> ObtenerPorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaResponse>> ObtenerPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaResponse>> ObtenerFacturasActivasAsync(CancellationToken cancellationToken = default);

        Task<FacturaResponse> ObtenerPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FacturaResponse>> ObtenerPorEstadoAsync(string estado, CancellationToken cancellationToken = default);

        Task<DataPagedResult<FacturaResponse>> BuscarAsync(FacturaFiltroRequest request, CancellationToken cancellationToken = default);

        Task<FacturaResponse> AprobarAsync(AprobarFacturaRequest request, CancellationToken cancellationToken = default);

        Task<FacturaResponse> AnularAsync(AnularFacturaRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_factura, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default);
    }
}