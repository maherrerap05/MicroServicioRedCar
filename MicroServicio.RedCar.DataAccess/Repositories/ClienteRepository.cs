using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly RedCarDBContext _context;

        public ClienteRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<ClienteEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .Where(c => !c.es_eliminado)
                .OrderBy(c => c.id_cliente)
                .ToListAsync(cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerPorIdAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id_cliente == id_cliente && !c.es_eliminado, cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerParaActualizarAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.id_cliente == id_cliente && !c.es_eliminado, cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerPorGuidAsync(Guid cliente_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.cliente_guid == cliente_guid && !c.es_eliminado, cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.numero_identificacion == numero_identificacion && !c.es_eliminado, cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.correo == correo && !c.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(ClienteEntity cliente, CancellationToken cancellationToken = default)
        {
            await _context.Clientes.AddAsync(cliente, cancellationToken);
        }

        public void Actualizar(ClienteEntity cliente)
        {
            _context.Clientes.Update(cliente);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .AnyAsync(c => c.numero_identificacion == numero_identificacion && !c.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .AnyAsync(c => c.correo == correo && !c.es_eliminado, cancellationToken);
        }
    }
}