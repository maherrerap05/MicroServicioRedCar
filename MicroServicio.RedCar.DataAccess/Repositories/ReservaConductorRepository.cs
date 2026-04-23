using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class ReservaConductorRepository : IReservaConductorRepository
    {
        private readonly RedCarDBContext _context;

        public ReservaConductorRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<ReservaConductorEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .AsNoTracking()
                .OrderBy(r => r.id_reserva_conductor)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReservaConductorEntity?> ObtenerPorIdAsync(int id_reserva_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.id_reserva_conductor == id_reserva_conductor && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaConductorEntity?> ObtenerParaActualizarAsync(int id_reserva_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .FirstOrDefaultAsync(r => r.id_reserva_conductor == id_reserva_conductor && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaConductorEntity?> ObtenerPorGuidAsync(Guid reserva_conductor_guid, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.reserva_conductor_guid == reserva_conductor_guid && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaConductorEntity?> ObtenerPorReservaYConductorAsync(int id_reserva, int id_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.id_reserva == id_reserva &&
                    r.id_conductor == id_conductor &&
                    !r.es_eliminado, cancellationToken);
        }

        public async Task<IReadOnlyList<ReservaConductorEntity>> ObtenerPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .AsNoTracking()
                .Where(r => r.id_reserva == id_reserva && !r.es_eliminado)
                .OrderBy(r => r.id_reserva_conductor)
                .ToListAsync(cancellationToken);
        }

        // CORRECCIÓN: devuelve todos los conductores de la reserva incluyendo eliminados.
        // Necesario para detectar conductores previamente removidos y reactivarlos
        // en lugar de insertar un duplicado que violaría UQ_RES_X_CON_RESERVA_CONDUCTOR.
        public async Task<IReadOnlyList<ReservaConductorEntity>> ObtenerTodosPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .Where(r => r.id_reserva == id_reserva)
                .OrderBy(r => r.id_reserva_conductor)
                .ToListAsync(cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(ReservaConductorEntity reservaConductor, CancellationToken cancellationToken = default)
        {
            await _context.ReservasConductores.AddAsync(reservaConductor, cancellationToken);
        }

        public void Actualizar(ReservaConductorEntity reservaConductor)
        {
            _context.ReservasConductores.Update(reservaConductor);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorReservaYConductorAsync(int id_reserva, int id_conductor, CancellationToken cancellationToken = default)
        {
            return await _context.ReservasConductores
                .AsNoTracking()
                .AnyAsync(r =>
                    r.id_reserva == id_reserva &&
                    r.id_conductor == id_conductor &&
                    !r.es_eliminado, cancellationToken);
        }
    }
}