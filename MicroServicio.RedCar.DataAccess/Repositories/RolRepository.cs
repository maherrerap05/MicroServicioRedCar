using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly RedCarDBContext _context;

        public RolRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<RolEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .OrderBy(r => r.id_rol)
                .ToListAsync(cancellationToken);
        }

        public async Task<RolEntity?> ObtenerPorIdAsync(int id_rol, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.id_rol == id_rol && !r.es_eliminado, cancellationToken);
        }

        public async Task<RolEntity?> ObtenerParaActualizarAsync(int id_rol, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.id_rol == id_rol && !r.es_eliminado, cancellationToken);
        }

        public async Task<RolEntity?> ObtenerPorGuidAsync(Guid rol_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.rol_guid == rol_guid && !r.es_eliminado, cancellationToken);
        }

        public async Task<RolEntity?> ObtenerPorNombreAsync(string nombre_rol, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.nombre_rol == nombre_rol && !r.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(RolEntity rol, CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(rol, cancellationToken);
        }

        public void Actualizar(RolEntity rol)
        {
            _context.Roles.Update(rol);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorNombreAsync(string nombre_rol, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .AnyAsync(r => r.nombre_rol == nombre_rol && !r.es_eliminado, cancellationToken);
        }
    }
}