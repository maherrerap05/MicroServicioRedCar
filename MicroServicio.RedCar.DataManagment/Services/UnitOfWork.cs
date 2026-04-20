using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataAccess.Queries;
using MicroServicio.RedCar.DataAccess.Repositories;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;
using MicroServicio.RedCar.DataManagement.Interfaces;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RedCarDBContext _context;

        // =========================
        // REPOSITORIES - NÚCLEO
        // =========================
        public IClienteRepository ClienteRepository { get; }
        public IVehiculoRepository VehiculoRepository { get; }
        public IReservaRepository ReservaRepository { get; }
        public IReservaExtraRepository ReservaExtraRepository { get; }
        public IReservaConductorRepository ReservaConductorRepository { get; }
        public IFacturaRepository FacturaRepository { get; }

        // =========================
        // REPOSITORIES - SEGURIDAD
        // =========================
        public IUsuarioAppRepository UsuarioAppRepository { get; }
        public IRolRepository RolRepository { get; }
        public IAuditoriaRepository AuditoriaRepository { get; }

        // =========================
        // REPOSITORIES - CATÁLOGOS / ADMINISTRACIÓN
        // =========================
        public IMarcaVehiculoRepository MarcaVehiculoRepository { get; }
        public ICategoriaVehiculoRepository CategoriaVehiculoRepository { get; }
        public IConductorRepository ConductorRepository { get; }
        public IExtraRepository ExtraRepository { get; }
        public ILocalizacionRepository LocalizacionRepository { get; }

        // =========================
        // QUERY REPOSITORIES
        // =========================
        public ClienteQueryRepository ClienteQueryRepository { get; }
        public VehiculoQueryRepository VehiculoQueryRepository { get; }
        public ReservaQueryRepository ReservaQueryRepository { get; }
        public FacturaQueryRepository FacturaQueryRepository { get; }

        public UnitOfWork(RedCarDBContext context)
        {
            _context = context;

            // =========================
            // REPOSITORIES - NÚCLEO
            // =========================
            ClienteRepository = new ClienteRepository(_context);
            VehiculoRepository = new VehiculoRepository(_context);
            ReservaRepository = new ReservaRepository(_context);
            ReservaExtraRepository = new ReservaExtraRepository(_context);
            ReservaConductorRepository = new ReservaConductorRepository(_context);
            FacturaRepository = new FacturaRepository(_context);

            // =========================
            // REPOSITORIES - SEGURIDAD
            // =========================
            UsuarioAppRepository = new UsuarioAppRepository(_context);
            RolRepository = new RolRepository(_context);
            AuditoriaRepository = new AuditoriaRepository(_context);

            // =========================
            // REPOSITORIES - CATÁLOGOS / ADMINISTRACIÓN
            // =========================
            MarcaVehiculoRepository = new MarcaVehiculoRepository(_context);
            CategoriaVehiculoRepository = new CategoriaVehiculoRepository(_context);
            ConductorRepository = new ConductorRepository(_context);
            ExtraRepository = new ExtraRepository(_context);
            LocalizacionRepository = new LocalizacionRepository(_context);

            // =========================
            // QUERY REPOSITORIES
            // =========================
            ClienteQueryRepository = new ClienteQueryRepository(_context);
            VehiculoQueryRepository = new VehiculoQueryRepository(_context);
            ReservaQueryRepository = new ReservaQueryRepository(_context);
            FacturaQueryRepository = new FacturaQueryRepository(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}