using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataAccess.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly RedCarDBContext _context;

        public FacturaRepository(RedCarDBContext context)
        {
            _context = context;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<FacturaEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .Where(f => !f.es_eliminado)
                .OrderBy(f => f.id_factura)
                .ToListAsync(cancellationToken);
        }

        public async Task<FacturaEntity?> ObtenerPorIdAsync(int id_factura, CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.id_factura == id_factura && !f.es_eliminado, cancellationToken);
        }

        public async Task<FacturaEntity?> ObtenerParaActualizarAsync(int id_factura, CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .FirstOrDefaultAsync(f => f.id_factura == id_factura && !f.es_eliminado, cancellationToken);
        }

        public async Task<FacturaEntity?> ObtenerPorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.guid_factura == guid_factura && !f.es_eliminado, cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task AgregarAsync(FacturaEntity factura, CancellationToken cancellationToken = default)
        {
            await _context.Facturas.AddAsync(factura, cancellationToken);
        }

        public void Actualizar(FacturaEntity factura)
        {
            _context.Facturas.Update(factura);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default)
        {
            return await _context.Facturas
                .AsNoTracking()
                .AnyAsync(f => f.guid_factura == guid_factura && !f.es_eliminado, cancellationToken);
        }
    }
}