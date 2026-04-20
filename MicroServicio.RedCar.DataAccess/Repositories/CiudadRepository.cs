using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class CiudadRepository : ICiudadRepository
    {
        private readonly RedCarDBContext _context;

        public CiudadRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<CiudadEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Ciudades
                .AsNoTracking()
                .Where(c => !c.es_eliminado)
                .OrderBy(c => c.id_ciudad)
                .ToListAsync(cancellationToken);
        }

        public async Task<CiudadEntity?> ObtenerPorIdAsync(int id_ciudad, CancellationToken cancellationToken = default)
        {
            return await _context.Ciudades
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id_ciudad == id_ciudad && !c.es_eliminado, cancellationToken);
        }

        public async Task<CiudadEntity?> ObtenerParaActualizarAsync(int id_ciudad, CancellationToken cancellationToken = default)
        {
            return await _context.Ciudades
                .FirstOrDefaultAsync(c => c.id_ciudad == id_ciudad && !c.es_eliminado, cancellationToken);
        }

        public async Task<CiudadEntity?> ObtenerPorGuidAsync(Guid ciudad_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Ciudades
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ciudad_guid == ciudad_guid && !c.es_eliminado, cancellationToken);
        }

        public async Task<CiudadEntity?> ObtenerPorNombreAsync(string nombre_ciudad, CancellationToken cancellationToken = default)
        {
            return await _context.Ciudades
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.nombre_ciudad == nombre_ciudad && !c.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(CiudadEntity ciudad, CancellationToken cancellationToken = default)
        {
            await _context.Ciudades.AddAsync(ciudad, cancellationToken);
        }

        public void Actualizar(CiudadEntity ciudad)
        {
            _context.Ciudades.Update(ciudad);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorNombreAsync(string nombre_ciudad, CancellationToken cancellationToken = default)
        {
            return await _context.Ciudades
                .AsNoTracking()
                .AnyAsync(c => c.nombre_ciudad == nombre_ciudad && !c.es_eliminado, cancellationToken);
        }
    }
}