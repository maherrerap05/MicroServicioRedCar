using MicroServicio.RedCar.Business.DTOs.Localizacion;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface ILocalizacionService
    {
        Task<LocalizacionResponse> CrearAsync(CrearLocalizacionRequest request, CancellationToken cancellationToken = default);

        Task<LocalizacionResponse> ActualizarAsync(ActualizarLocalizacionRequest request, CancellationToken cancellationToken = default);

        Task<LocalizacionResponse> ObtenerPorIdAsync(int id_localizacion, CancellationToken cancellationToken = default);

        Task<LocalizacionResponse> ObtenerPorGuidAsync(Guid localizacion_guid, CancellationToken cancellationToken = default);

        Task<LocalizacionResponse> ObtenerPorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<LocalizacionResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<LocalizacionResponse>> BuscarAsync(LocalizacionFiltroRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_localizacion, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default);
    }
}