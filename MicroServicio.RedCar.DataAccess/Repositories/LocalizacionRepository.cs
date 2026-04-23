using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class LocalizacionRepository : ILocalizacionRepository
    {
        private readonly RedCarDBContext _context;

        public LocalizacionRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<LocalizacionEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Localizaciones
                .AsNoTracking()
                .OrderBy(l => l.id_localizacion)
                .ToListAsync(cancellationToken);
        }

        public async Task<LocalizacionEntity?> ObtenerPorIdAsync(int id_localizacion, CancellationToken cancellationToken = default)
        {
            return await _context.Localizaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.id_localizacion == id_localizacion, cancellationToken);
        }

        public async Task<LocalizacionEntity?> ObtenerParaActualizarAsync(int id_localizacion, CancellationToken cancellationToken = default)
        {
            return await _context.Localizaciones
                .FirstOrDefaultAsync(l => l.id_localizacion == id_localizacion && !l.es_eliminado, cancellationToken);
        }

        public async Task<LocalizacionEntity?> ObtenerPorGuidAsync(Guid localizacion_guid, CancellationToken cancellationToken = default)
        {
            return await _context.Localizaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.localizacion_guid == localizacion_guid, cancellationToken);
        }

        public async Task<LocalizacionEntity?> ObtenerPorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default)
        {
            return await _context.Localizaciones
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.codigo_localizacion == codigo_localizacion, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(LocalizacionEntity localizacion, CancellationToken cancellationToken = default)
        {
            await _context.Localizaciones.AddAsync(localizacion, cancellationToken);
        }

        public void Actualizar(LocalizacionEntity localizacion)
        {
            _context.Localizaciones.Update(localizacion);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default)
        {
            return await _context.Localizaciones
                .AsNoTracking()
                .AnyAsync(l => l.codigo_localizacion == codigo_localizacion, cancellationToken);
        }
    }
}