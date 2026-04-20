using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IClienteDataService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<ClienteDataModel?> ObtenerPorIdAsync(int id_cliente, CancellationToken cancellationToken = default);

        Task<ClienteDataModel?> ObtenerPorGuidAsync(Guid cliente_guid, CancellationToken cancellationToken = default);

        Task<ClienteDataModel?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<ClienteDataModel?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ClienteDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<ClienteDataModel>> BuscarAsync(ClienteFiltroDataModel filtro, CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task<ClienteDataModel> CrearAsync(ClienteDataModel model, CancellationToken cancellationToken = default);

        Task<ClienteDataModel?> ActualizarAsync(ClienteDataModel model, CancellationToken cancellationToken = default);

        Task<bool> EliminarLogicoAsync(int id_cliente, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default);
    }
}