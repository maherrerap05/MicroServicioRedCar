using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface ILocalizacionDataService
    {
        Task<LocalizacionDataModel?> ObtenerPorIdAsync(int id_localizacion, CancellationToken cancellationToken = default);

        Task<LocalizacionDataModel?> ObtenerPorGuidAsync(Guid localizacion_guid, CancellationToken cancellationToken = default);

        Task<LocalizacionDataModel?> ObtenerPorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<LocalizacionDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<LocalizacionDataModel> CrearAsync(LocalizacionDataModel model, CancellationToken cancellationToken = default);

        Task<LocalizacionDataModel?> ActualizarAsync(LocalizacionDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_localizacion, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default);
    }
}