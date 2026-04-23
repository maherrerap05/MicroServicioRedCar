using MicroServicio.RedCar.DataAcces.Common;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class ConductorRepository : IConductorRepository
    {
        private readonly RedCarDBContext _context;

        public ConductorRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<ConductorEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .Where(c => !c.es_eliminado)
                .OrderBy(c => c.id_conductor)
                .ToListAsync(cancellationToken);
        }

        public async Task<ConductorEntity?> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id_conductor == id_conductor, cancellationToken);
        }

        public async Task<ConductorEntity?> ObtenerParaActualizarAsync(int id_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .FirstOrDefaultAsync(c => c.id_conductor == id_conductor && !c.es_eliminado, cancellationToken);
        }

        public async Task<ConductorEntity?> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.conductor_guid == conductor_guid, cancellationToken);
        }

        public async Task<ConductorEntity?> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.codigo_conductor == codigo_conductor, cancellationToken);
        }

        public async Task<ConductorEntity?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.numero_identificacion == numero_identificacion, cancellationToken);
        }

        public async Task<ConductorEntity?> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.numero_licencia == numero_licencia, cancellationToken);
        }

        public async Task<PagedResult<ConductorEntity>> BuscarAsync(
            string? codigo_conductor,
            string? tipo_identificacion,
            string? numero_identificacion,
            string? con_nombre1,
            string? con_nombre2,
            string? con_apellido1,
            string? con_apellido2,
            string? numero_licencia,
            DateTime? fecha_vencimiento_licencia_desde,
            DateTime? fecha_vencimiento_licencia_hasta,
            byte? edad_conductor,
            string? con_telefono,
            string? con_correo,
            string? estado_conductor,
            string? origen_registro,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Conductores
                .AsNoTracking()
                .Where(c => !c.es_eliminado)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(codigo_conductor))
                query = query.Where(c => c.codigo_conductor.Contains(codigo_conductor));

            if (!string.IsNullOrWhiteSpace(tipo_identificacion))
                query = query.Where(c => c.tipo_identificacion == tipo_identificacion);

            if (!string.IsNullOrWhiteSpace(numero_identificacion))
                query = query.Where(c => c.numero_identificacion.Contains(numero_identificacion));

            if (!string.IsNullOrWhiteSpace(con_nombre1))
                query = query.Where(c => c.con_nombre1.Contains(con_nombre1));

            if (!string.IsNullOrWhiteSpace(con_nombre2))
                query = query.Where(c => c.con_nombre2 != null && c.con_nombre2.Contains(con_nombre2));

            if (!string.IsNullOrWhiteSpace(con_apellido1))
                query = query.Where(c => c.con_apellido1.Contains(con_apellido1));

            if (!string.IsNullOrWhiteSpace(con_apellido2))
                query = query.Where(c => c.con_apellido2 != null && c.con_apellido2.Contains(con_apellido2));

            if (!string.IsNullOrWhiteSpace(numero_licencia))
                query = query.Where(c => c.numero_licencia.Contains(numero_licencia));

            if (fecha_vencimiento_licencia_desde.HasValue)
                query = query.Where(c => c.fecha_vencimiento_licencia >= fecha_vencimiento_licencia_desde.Value);

            if (fecha_vencimiento_licencia_hasta.HasValue)
                query = query.Where(c => c.fecha_vencimiento_licencia <= fecha_vencimiento_licencia_hasta.Value);

            if (edad_conductor.HasValue)
                query = query.Where(c => c.edad_conductor == edad_conductor.Value);

            if (!string.IsNullOrWhiteSpace(con_telefono))
                query = query.Where(c => c.con_telefono.Contains(con_telefono));

            if (!string.IsNullOrWhiteSpace(con_correo))
                query = query.Where(c => c.con_correo.Contains(con_correo));

            if (!string.IsNullOrWhiteSpace(estado_conductor))
                query = query.Where(c => c.estado_conductor == estado_conductor);

            if (!string.IsNullOrWhiteSpace(origen_registro))
                query = query.Where(c => c.origen_registro == origen_registro);

            var totalRecords = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(c => c.id_conductor)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ConductorEntity>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(ConductorEntity conductor, CancellationToken cancellationToken = default)
        {
            await _context.Conductores.AddAsync(conductor, cancellationToken);
        }

        public void Actualizar(ConductorEntity conductor)
        {
            _context.Conductores.Update(conductor);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .AnyAsync(c => c.numero_identificacion == numero_identificacion && !c.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .AnyAsync(c => c.numero_licencia == numero_licencia && !c.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.Conductores
                .AsNoTracking()
                .AnyAsync(c => c.codigo_conductor == codigo_conductor && !c.es_eliminado, cancellationToken);
        }
    }
}