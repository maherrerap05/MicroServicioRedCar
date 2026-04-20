using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly RedCarDBContext _context;

        public PaisRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<PaisEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Paises
                .AsNoTracking()
                .Where(p => !p.es_eliminado)
                .OrderBy(p => p.id_pais)
                .ToListAsync(cancellationToken);
        }

        public async Task<PaisEntity?> ObtenerPorIdAsync(int id_pais, CancellationToken cancellationToken = default)
        {
            return await _context.Paises
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id_pais == id_pais && !p.es_eliminado, cancellationToken);
        }

        public async Task<PaisEntity?> ObtenerParaActualizarAsync(int id_pais, CancellationToken cancellationToken = default)
        {
            return await _context.Paises
                .FirstOrDefaultAsync(p => p.id_pais == id_pais && !p.es_eliminado, cancellationToken);
        }

        public async Task<PaisEntity?> ObtenerPorGuidAsync(Guid pais_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Paises
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.pais_guid == pais_guid && !p.es_eliminado, cancellationToken);
        }

        public async Task<PaisEntity?> ObtenerPorNombreAsync(string nombre_pais, CancellationToken cancellationToken = default)
        {
            return await _context.Paises
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.nombre_pais == nombre_pais && !p.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(PaisEntity pais, CancellationToken cancellationToken = default)
        {
            await _context.Paises.AddAsync(pais, cancellationToken);
        }

        public void Actualizar(PaisEntity pais)
        {
            _context.Paises.Update(pais);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorNombreAsync(string nombre_pais, CancellationToken cancellationToken = default)
        {
            return await _context.Paises
                .AsNoTracking()
                .AnyAsync(p => p.nombre_pais == nombre_pais && !p.es_eliminado, cancellationToken);
        }
    }
}