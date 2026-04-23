using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class ExtraRepository : IExtraRepository
    {
        private readonly RedCarDBContext _context;

        public ExtraRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<ExtraEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Extras
                .AsNoTracking()
                .OrderBy(e => e.id_extra)
                .ToListAsync(cancellationToken);
        }

        public async Task<ExtraEntity?> ObtenerPorIdAsync(int id_extra, CancellationToken cancellationToken = default)
        {
            return await _context.Extras
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.id_extra == id_extra && !e.es_eliminado, cancellationToken);
        }

        public async Task<ExtraEntity?> ObtenerParaActualizarAsync(int id_extra, CancellationToken cancellationToken = default)
        {
            return await _context.Extras
                .FirstOrDefaultAsync(e => e.id_extra == id_extra && !e.es_eliminado, cancellationToken);
        }

        public async Task<ExtraEntity?> ObtenerPorGuidAsync(Guid extra_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Extras
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.extra_guid == extra_guid && !e.es_eliminado, cancellationToken);
        }

        public async Task<ExtraEntity?> ObtenerPorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default)
        {
            return await _context.Extras
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.codigo_extra == codigo_extra && !e.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(ExtraEntity extra, CancellationToken cancellationToken = default)
        {
            await _context.Extras.AddAsync(extra, cancellationToken);
        }

        public void Actualizar(ExtraEntity extra)
        {
            _context.Extras.Update(extra);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default)
        {
            return await _context.Extras
                .AsNoTracking()
                .AnyAsync(e => e.codigo_extra == codigo_extra && !e.es_eliminado, cancellationToken);
        }
    }
}