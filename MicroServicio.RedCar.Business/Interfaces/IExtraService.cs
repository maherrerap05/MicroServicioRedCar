using MicroServicio.RedCar.Business.DTOs.Extra;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IExtraService
    {
        Task<ExtraResponse> CrearAsync(CrearExtraRequest request, CancellationToken cancellationToken = default);

        Task<ExtraResponse> ActualizarAsync(ActualizarExtraRequest request, CancellationToken cancellationToken = default);

        Task<ExtraResponse> ObtenerPorIdAsync(int id_extra, CancellationToken cancellationToken = default);

        Task<ExtraResponse> ObtenerPorGuidAsync(Guid extra_guid, CancellationToken cancellationToken = default);

        Task<ExtraResponse> ObtenerPorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ExtraResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<ExtraResponse>> BuscarAsync(ExtraFiltroRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_extra, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default);
    }
}