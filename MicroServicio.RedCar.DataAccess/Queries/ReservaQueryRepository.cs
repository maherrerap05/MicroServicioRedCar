using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Queries
{
    public class ReservaQueryRepository
    {
        private readonly RedCarDBContext _context;

        public ReservaQueryRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // ============================================
        // BÚSQUEDA PAGINADA DE RESERVAS
        // ============================================
        public async Task<(IReadOnlyList<ReservaEntity> Items, int PageNumber, int PageSize, long TotalRecords)>
            BuscarAsync(
                string? codigo_reserva,
                int? id_cliente,
                int? id_vehiculo,
                int? id_localizacion_recogida,
                int? id_localizacion_devolucion,
                string? estado_reserva,
                string? origen_canal_reserva,
                DateTime? fecha_inicio_desde,
                DateTime? fecha_inicio_hasta,
                DateTime? fecha_fin_desde,
                DateTime? fecha_fin_hasta,
                decimal? total_min,
                decimal? total_max,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken = default)
        {
            var query = _context.Reservas
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(codigo_reserva))
            {
                query = query.Where(r => r.codigo_reserva.Contains(codigo_reserva));
            }

            if (id_cliente.HasValue)
            {
                query = query.Where(r => r.id_cliente == id_cliente.Value);
            }

            if (id_vehiculo.HasValue)
            {
                query = query.Where(r => r.id_vehiculo == id_vehiculo.Value);
            }

            if (id_localizacion_recogida.HasValue)
            {
                query = query.Where(r => r.id_localizacion_recogida == id_localizacion_recogida.Value);
            }

            if (id_localizacion_devolucion.HasValue)
            {
                query = query.Where(r => r.id_localizacion_devolucion == id_localizacion_devolucion.Value);
            }

            if (!string.IsNullOrWhiteSpace(estado_reserva))
            {
                query = query.Where(r => r.estado_reserva == estado_reserva);
            }

            if (!string.IsNullOrWhiteSpace(origen_canal_reserva))
            {
                query = query.Where(r => r.origen_canal_reserva == origen_canal_reserva);
            }

            if (fecha_inicio_desde.HasValue)
            {
                query = query.Where(r => r.fecha_inicio >= fecha_inicio_desde.Value);
            }

            if (fecha_inicio_hasta.HasValue)
            {
                query = query.Where(r => r.fecha_inicio <= fecha_inicio_hasta.Value);
            }

            if (fecha_fin_desde.HasValue)
            {
                query = query.Where(r => r.fecha_fin >= fecha_fin_desde.Value);
            }

            if (fecha_fin_hasta.HasValue)
            {
                query = query.Where(r => r.fecha_fin <= fecha_fin_hasta.Value);
            }

            if (total_min.HasValue)
            {
                query = query.Where(r => r.total_reserva >= total_min.Value);
            }

            if (total_max.HasValue)
            {
                query = query.Where(r => r.total_reserva <= total_max.Value);
            }

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(r => r.fecha_reserva_utc)
                .ThenByDescending(r => r.id_reserva)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, pageNumber, pageSize, totalRecords);
        }

        // ============================================
        // HISTORIAL DE RESERVAS POR CLIENTE
        // ============================================
        public async Task<IReadOnlyList<ReservaEntity>> ObtenerHistorialPorClienteAsync(
            int id_cliente,
            CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Where(r => r.id_cliente == id_cliente && !r.es_eliminado)
                .OrderByDescending(r => r.fecha_reserva_utc)
                .ToListAsync(cancellationToken);
        }

        // ============================================
        // RESERVAS ACTIVAS (NO CANCELADAS)
        // ============================================
        public async Task<IReadOnlyList<ReservaEntity>> ObtenerReservasActivasAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Where(r => !r.es_eliminado && r.estado_reserva != "CAN")
                .OrderByDescending(r => r.fecha_reserva_utc)
                .ToListAsync(cancellationToken);
        }

        // ============================================
        // RESERVAS POR VEHÍCULO (IMPORTANTE PARA DISPONIBILIDAD)
        // ============================================
        public async Task<IReadOnlyList<ReservaEntity>> ObtenerReservasPorVehiculoAsync(
            int id_vehiculo,
            CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Where(r => r.id_vehiculo == id_vehiculo && !r.es_eliminado)
                .OrderByDescending(r => r.fecha_hora_recogida)
                .ToListAsync(cancellationToken);
        }
    }
}