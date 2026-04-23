using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly RedCarDBContext _context;

        public ReservaRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<ReservaEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .OrderBy(r => r.id_reserva)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReservaEntity?> ObtenerPorIdAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.id_reserva == id_reserva && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaEntity?> ObtenerParaActualizarAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .FirstOrDefaultAsync(r => r.id_reserva == id_reserva && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaEntity?> ObtenerPorGuidAsync(Guid guid_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.guid_reserva == guid_reserva && !r.es_eliminado, cancellationToken);
        }

        public async Task<ReservaEntity?> ObtenerPorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.codigo_reserva == codigo_reserva && !r.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(ReservaEntity reserva, CancellationToken cancellationToken = default)
        {
            await _context.Reservas.AddAsync(reserva, cancellationToken);
        }

        public void Actualizar(ReservaEntity reserva)
        {
            _context.Reservas.Update(reserva);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AsNoTracking()
                .AnyAsync(r => r.codigo_reserva == codigo_reserva && !r.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExisteReservaActivaPorVehiculoAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Reservas
                .AnyAsync(r => r.id_vehiculo == id_vehiculo
                            && r.estado_reserva == "PEN"
                            && !r.es_eliminado,
                          cancellationToken);
        }
    }
}