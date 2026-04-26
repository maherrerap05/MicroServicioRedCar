using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Queries
{
    public class ClienteQueryRepository
    {
        private readonly RedCarDBContext _context;

        public ClienteQueryRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // ============================================
        // CLIENTES ACTIVOS
        // ============================================
        public async Task<IEnumerable<ClienteEntity>> ObtenerClientesActivosAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Where(c => !c.es_eliminado && c.estado == "ACT")
                .OrderBy(c => c.apellidos)
                .ThenBy(c => c.nombres)
                .ToListAsync();
        }

        // ============================================
        // CLIENTES CON RESERVAS
        // ============================================
        public async Task<IEnumerable<ClienteEntity>> ObtenerClientesConReservasAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Where(c =>
                    !c.es_eliminado &&
                    _context.Reservas.Any(r => r.id_cliente == c.id_cliente && !r.es_eliminado))
                .OrderBy(c => c.apellidos)
                .ThenBy(c => c.nombres)
                .ToListAsync();
        }

        // ============================================
        // CLIENTES CON FACTURAS
        // ============================================
        public async Task<IEnumerable<ClienteEntity>> ObtenerClientesConFacturasAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .Where(c =>
                    !c.es_eliminado &&
                    _context.Facturas.Any(f => f.id_cliente == c.id_cliente && !f.es_eliminado))
                .OrderBy(c => c.apellidos)
                .ThenBy(c => c.nombres)
                .ToListAsync();
        }

        // ============================================
        // OBTENER CLIENTE POR IDENTIFICACIÓN
        // ============================================
        public async Task<ClienteEntity?> ObtenerClientePorIdentificacionAsync(string numero_identificacion)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.numero_identificacion == numero_identificacion &&
                    !c.es_eliminado);
        }

        // ============================================
        // OBTENER CLIENTE POR CORREO
        // ============================================
        public async Task<ClienteEntity?> ObtenerClientePorCorreoAsync(string correo)
        {
            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.correo == correo &&
                    !c.es_eliminado);
        }

        // ============================================
        // BÚSQUEDA PAGINADA DE CLIENTES
        // ============================================
        public async Task<(IReadOnlyList<ClienteEntity> Items, int PageNumber, int PageSize, long TotalRecords)>
            BuscarAsync(
                string? tipo_identificacion,
                string? numero_identificacion,
                string? razon_social,
                string? nombres,
                string? apellidos,
                string? correo,
                string? telefono,
                string? estado,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken = default)
        {
            var query = _context.Clientes.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado))
            {
                query = query.Where(c => c.estado == estado);
            }

            if (!string.IsNullOrWhiteSpace(tipo_identificacion))
                query = query.Where(c => c.tipo_identificacion == tipo_identificacion);

            if (!string.IsNullOrWhiteSpace(numero_identificacion))
                query = query.Where(c => c.numero_identificacion.Contains(numero_identificacion));

            if (!string.IsNullOrWhiteSpace(razon_social))
                query = query.Where(c => c.razon_social != null && c.razon_social.Contains(razon_social));

            if (!string.IsNullOrWhiteSpace(nombres))
                query = query.Where(c => c.nombres.Contains(nombres));

            if (!string.IsNullOrWhiteSpace(apellidos))
                query = query.Where(c => c.apellidos.Contains(apellidos));

            if (!string.IsNullOrWhiteSpace(correo))
                query = query.Where(c => c.correo.Contains(correo));

            if (!string.IsNullOrWhiteSpace(telefono))
                query = query.Where(c => c.telefono != null && c.telefono.Contains(telefono));

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .OrderBy(c => c.apellidos)
                .ThenBy(c => c.nombres)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, pageNumber, pageSize, totalRecords);
        }
    }
}