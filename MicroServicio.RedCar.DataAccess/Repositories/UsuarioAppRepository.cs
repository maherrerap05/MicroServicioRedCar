using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class UsuarioAppRepository : IUsuarioAppRepository
    {
        private readonly RedCarDBContext _context;

        public UsuarioAppRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<UsuarioAppEntity?> ObtenerPorIdAsync(int id_usuario, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.id_usuario == id_usuario && !u.es_eliminado, cancellationToken);
        }

        public async Task<UsuarioAppEntity?> ObtenerParaActualizarAsync(int id_usuario, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .FirstOrDefaultAsync(u => u.id_usuario == id_usuario && !u.es_eliminado, cancellationToken);
        }

        public async Task<UsuarioAppEntity?> ObtenerPorGuidAsync(Guid usuario_guid, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.usuario_guid == usuario_guid && !u.es_eliminado, cancellationToken);
        }

        public async Task<UsuarioAppEntity?> ObtenerPorUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .AsNoTracking()
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(
                    u => u.username == userName && !u.es_eliminado && u.activo,
                    cancellationToken);
        }

        public async Task<UsuarioAppEntity?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.correo == correo && !u.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(UsuarioAppEntity usuario, CancellationToken cancellationToken = default)
        {
            await _context.UsuariosApp.AddAsync(usuario, cancellationToken);
        }

        public void Actualizar(UsuarioAppEntity usuario)
        {
            _context.UsuariosApp.Update(usuario);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .AsNoTracking()
                .AnyAsync(u => u.username == userName && !u.es_eliminado, cancellationToken);
        }

        public async Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            return await _context.UsuariosApp
                .AsNoTracking()
                .AnyAsync(u => u.correo == correo && !u.es_eliminado, cancellationToken);
        }
    }
}