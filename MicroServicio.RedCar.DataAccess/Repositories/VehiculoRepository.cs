using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly RedCarDBContext _context;

        public VehiculoRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<VehiculoEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .Where(v => !v.es_eliminado)
                .OrderBy(v => v.id_vehiculo)
                .ToListAsync(cancellationToken);
        }

        public async Task<VehiculoEntity?> ObtenerPorIdAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.id_vehiculo == id_vehiculo && !v.es_eliminado, cancellationToken);
        }

        public async Task<VehiculoEntity?> ObtenerParaActualizarAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .FirstOrDefaultAsync(v => v.id_vehiculo == id_vehiculo && !v.es_eliminado, cancellationToken);
        }

        public async Task<VehiculoEntity?> ObtenerPorGuidAsync(Guid vehiculo_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.vehiculo_guid == vehiculo_guid && !v.es_eliminado, cancellationToken);
        }

        public async Task<VehiculoEntity?> ObtenerPorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.codigo_interno_vehiculo == codigo_interno_vehiculo && !v.es_eliminado, cancellationToken);
        }

        public async Task<VehiculoEntity?> ObtenerPorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.placa_vehiculo == placa_vehiculo && !v.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(VehiculoEntity vehiculo, CancellationToken cancellationToken = default)
        {
            await _context.Vehiculos.AddAsync(vehiculo, cancellationToken);
        }

        public void Actualizar(VehiculoEntity vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .AnyAsync(v => v.codigo_interno_vehiculo == codigo_interno_vehiculo && !v.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.Vehiculos
                .AsNoTracking()
                .AnyAsync(v => v.placa_vehiculo == placa_vehiculo && !v.es_eliminado, cancellationToken);
        }
    }
}