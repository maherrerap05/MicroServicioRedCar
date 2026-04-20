using MicroServicio.RedCar.DataAccess.Queries;
using MicroServicio.RedCar.DataAccess.Repositories.Interfaces;

namespace MicroServicio.RedCar.DataManagement.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // =========================
        // REPOSITORIES - NÚCLEO
        // =========================
        IClienteRepository ClienteRepository { get; }
        IVehiculoRepository VehiculoRepository { get; }
        IReservaRepository ReservaRepository { get; }
        IReservaExtraRepository ReservaExtraRepository { get; }
        IReservaConductorRepository ReservaConductorRepository { get; }
        IFacturaRepository FacturaRepository { get; }

        // =========================
        // REPOSITORIES - SEGURIDAD
        // =========================
        IUsuarioAppRepository UsuarioAppRepository { get; }
        IRolRepository RolRepository { get; }
        IAuditoriaRepository AuditoriaRepository { get; }

        // =========================
        // REPOSITORIES - CATÁLOGOS / ADMINISTRACIÓN
        // =========================
        IMarcaVehiculoRepository MarcaVehiculoRepository { get; }
        ICategoriaVehiculoRepository CategoriaVehiculoRepository { get; }
        IConductorRepository ConductorRepository { get; }
        IExtraRepository ExtraRepository { get; }
        ILocalizacionRepository LocalizacionRepository { get; }

        // =========================
        // QUERY REPOSITORIES
        // =========================
        ClienteQueryRepository ClienteQueryRepository { get; }
        VehiculoQueryRepository VehiculoQueryRepository { get; }
        ReservaQueryRepository ReservaQueryRepository { get; }
        FacturaQueryRepository FacturaQueryRepository { get; }

        // =========================
        // SAVE CHANGES
        // =========================
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}