using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class MarcaVehiculoRepository : IMarcaVehiculoRepository
    {
        private readonly RedCarDBContext _context;

        public MarcaVehiculoRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<MarcaVehiculoEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .Where(m => !m.es_eliminado)
                .OrderBy(m => m.id_marca_vehiculo)
                .ToListAsync(cancellationToken);
        }

        public async Task<MarcaVehiculoEntity?> ObtenerPorIdAsync(int id_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.id_marca_vehiculo == id_marca_vehiculo && !m.es_eliminado, cancellationToken);
        }

        public async Task<MarcaVehiculoEntity?> ObtenerParaActualizarAsync(int id_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .FirstOrDefaultAsync(m => m.id_marca_vehiculo == id_marca_vehiculo && !m.es_eliminado, cancellationToken);
        }

        public async Task<MarcaVehiculoEntity?> ObtenerPorGuidAsync(Guid marca_vehiculo_guid, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.marca_vehiculo_guid == marca_vehiculo_guid && !m.es_eliminado, cancellationToken);
        }

        public async Task<MarcaVehiculoEntity?> ObtenerPorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.codigo_marca_vehiculo == codigo_marca_vehiculo && !m.es_eliminado, cancellationToken);
        }

        public async Task<MarcaVehiculoEntity?> ObtenerPorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.nombre_marca_vehiculo == nombre_marca_vehiculo && !m.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(MarcaVehiculoEntity marcaVehiculo, CancellationToken cancellationToken = default)
        {
            await _context.MarcasVehiculo.AddAsync(marcaVehiculo, cancellationToken);
        }

        public void Actualizar(MarcaVehiculoEntity marcaVehiculo)
        {
            _context.MarcasVehiculo.Update(marcaVehiculo);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .AnyAsync(m => m.codigo_marca_vehiculo == codigo_marca_vehiculo && !m.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.MarcasVehiculo
                .AsNoTracking()
                .AnyAsync(m => m.nombre_marca_vehiculo == nombre_marca_vehiculo && !m.es_eliminado, cancellationToken);
        }
    }
}