using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly RedCarDBContext _context;

        public AuditoriaRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<AuditoriaEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Auditoria
                .AsNoTracking()
                .OrderByDescending(a => a.fecha_evento_utc)
                .ToListAsync(cancellationToken);
        }

        public async Task<AuditoriaEntity?> ObtenerPorIdAsync(long id_auditoria, CancellationToken cancellationToken = default)
        {
            return await _context.Auditoria
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.id_auditoria == id_auditoria, cancellationToken);
        }

        public async Task<AuditoriaEntity?> ObtenerPorGuidAsync(Guid auditoria_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Auditoria
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.auditoria_guid == auditoria_guid, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(AuditoriaEntity auditoria, CancellationToken cancellationToken = default)
        {
            await _context.Auditoria.AddAsync(auditoria, cancellationToken);
        }
    }
}