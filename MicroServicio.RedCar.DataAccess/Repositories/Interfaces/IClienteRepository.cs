using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IReadOnlyList<ClienteEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerPorIdAsync(int id_cliente, CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerParaActualizarAsync(int id_cliente, CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerPorGuidAsync(Guid cliente_guid, CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default);

        Task AgregarAsync(ClienteEntity cliente, CancellationToken cancellationToken = default);
        void Actualizar(ClienteEntity cliente);

        Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);
        Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default);
    }
}