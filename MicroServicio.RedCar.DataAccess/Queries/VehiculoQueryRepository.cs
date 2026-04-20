using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Queries
{
    public class VehiculoQueryRepository
    {
        private readonly RedCarDBContext _context;

        public VehiculoQueryRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // ============================================
        // BÚSQUEDA PAGINADA DE VEHÍCULOS
        // ============================================
        public async Task<(IReadOnlyList<VehiculoEntity> Items, int PageNumber, int PageSize, long TotalRecords)>
            BuscarAsync(
                string? codigo_interno_vehiculo,
                string? placa_vehiculo,
                string? modelo_vehiculo,
                string? tipo_combustible,
                string? tipo_transmision,
                int? id_marca_vehiculo,
                int? id_categoria_vehiculo,
                int? localizacion_actual,
                string? estado_vehiculo,
                decimal? precio_min,
                decimal? precio_max,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken = default)
        {
            var query = _context.Vehiculos
                .AsNoTracking()
                .Where(v => !v.es_eliminado);

            if (!string.IsNullOrWhiteSpace(codigo_interno_vehiculo))
            {
                query = query.Where(v => v.codigo_interno_vehiculo.Contains(codigo_interno_vehiculo));
            }

            if (!string.IsNullOrWhiteSpace(placa_vehiculo))
            {
                query = query.Where(v => v.placa_vehiculo.Contains(placa_vehiculo));
            }

            if (!string.IsNullOrWhiteSpace(modelo_vehiculo))
            {
                query = query.Where(v => v.modelo_vehiculo.Contains(modelo_vehiculo));
            }

            if (!string.IsNullOrWhiteSpace(tipo_combustible))
            {
                query = query.Where(v => v.tipo_combustible == tipo_combustible);
            }

            if (!string.IsNullOrWhiteSpace(tipo_transmision))
            {
                query = query.Where(v => v.tipo_transmision == tipo_transmision);
            }

            if (id_marca_vehiculo.HasValue)
            {
                query = query.Where(v => v.id_marca_vehiculo == id_marca_vehiculo.Value);
            }

            if (id_categoria_vehiculo.HasValue)
            {
                query = query.Where(v => v.id_categoria_vehiculo == id_categoria_vehiculo.Value);
            }

            if (localizacion_actual.HasValue)
            {
                query = query.Where(v => v.localizacion_actual == localizacion_actual.Value);
            }

            if (!string.IsNullOrWhiteSpace(estado_vehiculo))
            {
                query = query.Where(v => v.estado_vehiculo == estado_vehiculo);
            }

            if (precio_min.HasValue)
            {
                query = query.Where(v => v.precio_base_dia >= precio_min.Value);
            }

            if (precio_max.HasValue)
            {
                query = query.Where(v => v.precio_base_dia <= precio_max.Value);
            }

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .OrderBy(v => v.precio_base_dia)
                .ThenBy(v => v.id_marca_vehiculo)
                .ThenBy(v => v.modelo_vehiculo)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, pageNumber, pageSize, totalRecords);
        }

        // ============================================
        // VEHÍCULOS DISPONIBLES POR LOCALIZACIÓN Y RANGO
        // ============================================
        public async Task<IReadOnlyList<VehiculoEntity>> ObtenerDisponiblesAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .Where(v =>
                    !v.es_eliminado &&
                    v.estado_vehiculo == "ACT" &&
                    v.localizacion_actual == id_localizacion_recogida &&
                    !_context.Reservas.Any(r =>
                        !r.es_eliminado &&
                        r.id_vehiculo == v.id_vehiculo &&
                        (r.estado_reserva == "PEN" || r.estado_reserva == "CON") &&
                        fecha_hora_recogida < r.fecha_hora_devolucion &&
                        fecha_hora_devolucion > r.fecha_hora_recogida
                    )
                )
                .OrderBy(v => v.precio_base_dia)
                .ThenBy(v => v.id_marca_vehiculo)
                .ThenBy(v => v.modelo_vehiculo)
                .ToListAsync(cancellationToken);
        }

        // ============================================
        // VALIDAR SI UN VEHÍCULO ES DISPONIBLE
        // EN UN RANGO ESPECÍFICO
        // ============================================
        public async Task<bool> EstaDisponibleAsync(
            int id_vehiculo,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default)
        {
            var existeConflicto = await _context.Reservas
                .AsNoTracking()
                .AnyAsync(r =>
                    !r.es_eliminado &&
                    r.id_vehiculo == id_vehiculo &&
                    (r.estado_reserva == "PEN" || r.estado_reserva == "CON") &&
                    fecha_hora_recogida < r.fecha_hora_devolucion &&
                    fecha_hora_devolucion > r.fecha_hora_recogida,
                    cancellationToken);

            return !existeConflicto;
        }

        // ============================================
        // VEHÍCULOS DISPONIBLES POR LOCALIZACIÓN,
        // RANGO Y CATEGORÍA
        // ============================================
        public async Task<IReadOnlyList<VehiculoEntity>> ObtenerDisponiblesPorCategoriaAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            int id_categoria_vehiculo,
            CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .Where(v =>
                    !v.es_eliminado &&
                    v.estado_vehiculo == "ACT" &&
                    v.localizacion_actual == id_localizacion_recogida &&
                    v.id_categoria_vehiculo == id_categoria_vehiculo &&
                    !_context.Reservas.Any(r =>
                        !r.es_eliminado &&
                        r.id_vehiculo == v.id_vehiculo &&
                        (r.estado_reserva == "PEN" || r.estado_reserva == "CON") &&
                        fecha_hora_recogida < r.fecha_hora_devolucion &&
                        fecha_hora_devolucion > r.fecha_hora_recogida
                    )
                )
                .OrderBy(v => v.precio_base_dia)
                .ThenBy(v => v.id_marca_vehiculo)
                .ThenBy(v => v.modelo_vehiculo)
                .ToListAsync(cancellationToken);
        }
    }
}