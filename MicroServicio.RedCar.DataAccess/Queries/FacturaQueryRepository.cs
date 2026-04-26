using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Queries
{
    public class FacturaQueryRepository
    {
        private readonly RedCarDBContext _context;

        public FacturaQueryRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // ============================================
        // BÚSQUEDA PAGINADA DE FACTURAS
        // ============================================
        public async Task<(IReadOnlyList<FacturaEntity> Items, int PageNumber, int PageSize, long TotalRecords)>
            BuscarAsync(
                string? numero_factura,
                int? id_cliente,
                int? id_reserva,
                string? estado,
                string? origen_canal_factura,
                DateTime? fecha_emision_desde,
                DateTime? fecha_emision_hasta,
                decimal? total_min,
                decimal? total_max,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken = default)
        {
            var query = _context.Facturas
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado))
            {
                query = query.Where(f => f.estado == estado);
            }

            if (!string.IsNullOrWhiteSpace(numero_factura))
            {
                query = query.Where(f => f.numero_factura.Contains(numero_factura));
            }

            if (id_cliente.HasValue)
            {
                query = query.Where(f => f.id_cliente == id_cliente.Value);
            }

            if (id_reserva.HasValue)
            {
                query = query.Where(f => f.id_reserva == id_reserva.Value);
            }

            if (!string.IsNullOrWhiteSpace(origen_canal_factura))
            {
                query = query.Where(f => f.origen_canal_factura == origen_canal_factura);
            }

            if (fecha_emision_desde.HasValue)
            {
                query = query.Where(f => f.fecha_emision >= fecha_emision_desde.Value);
            }

            if (fecha_emision_hasta.HasValue)
            {
                query = query.Where(f => f.fecha_emision <= fecha_emision_hasta.Value);
            }

            if (total_min.HasValue)
            {
                query = query.Where(f => f.total >= total_min.Value);
            }

            if (total_max.HasValue)
            {
                query = query.Where(f => f.total <= total_max.Value);
            }

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(f => f.fecha_emision)
                .ThenByDescending(f => f.id_factura)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, pageNumber, pageSize, totalRecords);
        }

        // ============================================
        // FACTURAS POR CLIENTE
        // ============================================
        public async Task<IReadOnlyList<FacturaEntity>> ObtenerPorClienteAsync(
            int id_cliente,
            CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .Where(f => f.id_cliente == id_cliente && !f.es_eliminado)
                .OrderByDescending(f => f.fecha_emision)
                .ToListAsync(cancellationToken);
        }

        // ============================================
        // FACTURAS ACTIVAS
        // ============================================
        public async Task<IReadOnlyList<FacturaEntity>> ObtenerFacturasActivasAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .Where(f => !f.es_eliminado && f.estado != "INA")
                .OrderByDescending(f => f.fecha_emision)
                .ToListAsync(cancellationToken);
        }

        // ============================================
        // OBTENER FACTURA ASOCIADA A UNA RESERVA
        // ============================================
        public async Task<FacturaEntity?> ObtenerFacturaPorReservaAsync(
            int id_reserva,
            CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.id_reserva == id_reserva && !f.es_eliminado, cancellationToken);
        }

        // ============================================
        // OBTENER FACTURAS POR ESTADO
        // ============================================
        public async Task<IReadOnlyList<FacturaEntity>> ObtenerPorEstadoAsync(
            string estado,
            CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .Where(f => f.estado == estado && !f.es_eliminado)
                .OrderByDescending(f => f.fecha_emision)
                .ToListAsync(cancellationToken);
        }
    }
}