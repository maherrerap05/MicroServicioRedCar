using MicroServicio.RedCar.Business.DTOs.Cliente;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IClienteService
    {
        // =========================
        // COMANDOS
        // =========================
        Task<ClienteResponse> CrearAsync(CrearClienteRequest request, CancellationToken cancellationToken = default);

        Task<ClienteResponse> ActualizarAsync(ActualizarClienteRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_cliente, string usuario, string? motivo, CancellationToken cancellationToken = default);

        // =========================
        // CONSULTAS
        // =========================
        Task<ClienteResponse> ObtenerPorIdAsync(int id_cliente, CancellationToken cancellationToken = default);

        Task<ClienteResponse> ObtenerPorGuidAsync(Guid cliente_guid, CancellationToken cancellationToken = default);

        Task<ClienteResponse> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<ClienteResponse> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ClienteResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<ClienteResponse>> BuscarAsync(ClienteFiltroRequest request, CancellationToken cancellationToken = default);
    }
}