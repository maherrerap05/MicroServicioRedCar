using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class CategoriaVehiculoRepository : ICategoriaVehiculoRepository
    {
        private readonly RedCarDBContext _context;

        public CategoriaVehiculoRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<CategoriaVehiculoEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .Where(c => !c.es_eliminado)
                .OrderBy(c => c.id_categoria_vehiculo)
                .ToListAsync(cancellationToken);
        }

        public async Task<CategoriaVehiculoEntity?> ObtenerPorIdAsync(int id_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id_categoria_vehiculo == id_categoria_vehiculo && !c.es_eliminado, cancellationToken);
        }

        public async Task<CategoriaVehiculoEntity?> ObtenerParaActualizarAsync(int id_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .FirstOrDefaultAsync(c => c.id_categoria_vehiculo == id_categoria_vehiculo && !c.es_eliminado, cancellationToken);
        }

        public async Task<CategoriaVehiculoEntity?> ObtenerPorGuidAsync(Guid categoria_vehiculo_guid, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.categoria_vehiculo_guid == categoria_vehiculo_guid && !c.es_eliminado, cancellationToken);
        }

        public async Task<CategoriaVehiculoEntity?> ObtenerPorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.codigo_categoria_vehiculo == codigo_categoria_vehiculo && !c.es_eliminado, cancellationToken);
        }

        public async Task<CategoriaVehiculoEntity?> ObtenerPorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.nombre_categoria_vehiculo == nombre_categoria_vehiculo && !c.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(CategoriaVehiculoEntity categoriaVehiculo, CancellationToken cancellationToken = default)
        {
            await _context.CategoriasVehiculo.AddAsync(categoriaVehiculo, cancellationToken);
        }

        public void Actualizar(CategoriaVehiculoEntity categoriaVehiculo)
        {
            _context.CategoriasVehiculo.Update(categoriaVehiculo);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .AnyAsync(c => c.codigo_categoria_vehiculo == codigo_categoria_vehiculo && !c.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _context.CategoriasVehiculo
                .AsNoTracking()
                .AnyAsync(c => c.nombre_categoria_vehiculo == nombre_categoria_vehiculo && !c.es_eliminado, cancellationToken);
        }
    }
}