using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IExtraDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<ExtraDataModel?> ObtenerPorIdAsync(int id_extra, CancellationToken cancellationToken = default);

        Task<ExtraDataModel?> ObtenerPorGuidAsync(Guid extra_guid, CancellationToken cancellationToken = default);

        Task<ExtraDataModel?> ObtenerPorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ExtraDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);


        // =========================
        // COMANDOS
        // =========================
        Task<ExtraDataModel> CrearAsync(ExtraDataModel model, CancellationToken cancellationToken = default);

        Task<ExtraDataModel?> ActualizarAsync(ExtraDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_extra, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default);
    }
}