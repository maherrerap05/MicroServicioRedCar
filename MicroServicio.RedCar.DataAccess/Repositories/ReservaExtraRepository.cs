using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class ReservaExtraRepository : IReservaExtraRepository
    {
        private readonly RedCarDBContext _context;

        public ReservaExtraRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<ReservaExtraEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .AsNoTracking()
                .OrderBy(r => r.id_reserva_extra)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReservaExtraEntity?> ObtenerPorIdAsync(int id_reserva_extra, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.id_reserva_extra == id_reserva_extra && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaExtraEntity?> ObtenerParaActualizarAsync(int id_reserva_extra, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .FirstOrDefaultAsync(r => r.id_reserva_extra == id_reserva_extra && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaExtraEntity?> ObtenerPorGuidAsync(Guid reserva_extra_guid, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.reserva_extra_guid == reserva_extra_guid && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaExtraEntity?> ObtenerPorReservaYExtraAsync(int id_reserva, int id_extra, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.id_reserva == id_reserva &&
                    r.id_extra == id_extra &&
                    !r.es_eliminado, cancellationToken);
        }

        public async Task<IReadOnlyList<ReservaExtraEntity>> ObtenerPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .AsNoTracking()
                .Where(r => r.id_reserva == id_reserva && !r.es_eliminado)
                .OrderBy(r => r.id_reserva_extra)
                .ToListAsync(cancellationToken);
        }

        // CORRECCIÓN: devuelve todos los extras de la reserva incluyendo eliminados.
        // Necesario para detectar extras previamente removidos y reactivarlos
        // en lugar de insertar un duplicado que violaría UQ_RES_X_XTRAS_RESERVA_EXTRA.
        public async Task<IReadOnlyList<ReservaExtraEntity>> ObtenerTodosPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .Where(r => r.id_reserva == id_reserva)
                .OrderBy(r => r.id_reserva_extra)
                .ToListAsync(cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(ReservaExtraEntity reservaExtra, CancellationToken cancellationToken = default)
        {
            await _context.ReservasExtras.AddAsync(reservaExtra, cancellationToken);
        }

        public void Actualizar(ReservaExtraEntity reservaExtra)
        {
            _context.ReservasExtras.Update(reservaExtra);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorReservaYExtraAsync(int id_reserva, int id_extra, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasExtras
                .AsNoTracking()
                .AnyAsync(r =>
                    r.id_reserva == id_reserva &&
                    r.id_extra == id_extra &&
                    !r.es_eliminado, cancellationToken);
        }
    }
}