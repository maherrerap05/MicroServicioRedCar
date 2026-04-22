using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

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