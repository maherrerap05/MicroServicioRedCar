using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.DataAccess.Configurations;
using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Context
{
    public class RedCarDBContext : DbContext
    {
        public RedCarDBContext(DbContextOptions<RedCarDBContext> options)
            : base(options)
        {
        }

        // =========================
        // ENTIDADES PRINCIPALES
        // =========================
        public DbSet<ClienteEntity> Clientes => Set<ClienteEntity>();
        public DbSet<VehiculoEntity> Vehiculos => Set<VehiculoEntity>();
        public DbSet<LocalizacionEntity> Localizaciones => Set<LocalizacionEntity>();
        public DbSet<ExtraEntity> Extras => Set<ExtraEntity>();
        public DbSet<ConductorEntity> Conductores => Set<ConductorEntity>();
        public DbSet<ReservaEntity> Reservas => Set<ReservaEntity>();
        public DbSet<FacturaEntity> Facturas => Set<FacturaEntity>();
        public DbSet<ReservaExtraEntity> ReservasExtras => Set<ReservaExtraEntity>();
        public DbSet<ReservaConductorEntity> ReservasConductores => Set<ReservaConductorEntity>();

        // =========================
        // CATÁLOGOS NUEVOS
        // =========================
        public DbSet<CategoriaVehiculoEntity> CategoriasVehiculo => Set<CategoriaVehiculoEntity>();
        public DbSet<MarcaVehiculoEntity> MarcasVehiculo => Set<MarcaVehiculoEntity>();
        public DbSet<CiudadEntity> Ciudades => Set<CiudadEntity>();
        public DbSet<PaisEntity> Paises => Set<PaisEntity>();

        // =========================
        // SEGURIDAD Y AUDITORÍA
        // =========================
        public DbSet<UsuarioAppEntity> UsuariosApp => Set<UsuarioAppEntity>();
        public DbSet<RolEntity> Roles => Set<RolEntity>();
        public DbSet<UsuarioRolEntity> UsuariosRoles => Set<UsuarioRolEntity>();
        public DbSet<AuditoriaEntity> Auditoria => Set<AuditoriaEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // CONFIGURACIONES PRINCIPALES
            // =========================
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new VehiculoConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizacionConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraConfiguration());
            modelBuilder.ApplyConfiguration(new ConductorConfiguration());
            modelBuilder.ApplyConfiguration(new ReservaConfiguration());
            modelBuilder.ApplyConfiguration(new FacturaConfiguration());
            modelBuilder.ApplyConfiguration(new ReservaExtraConfiguration());
            modelBuilder.ApplyConfiguration(new ReservaConductorConfiguration());

            // =========================
            // CONFIGURACIONES CATÁLOGOS
            // =========================
            modelBuilder.ApplyConfiguration(new CategoriaVehiculoConfiguration());
            modelBuilder.ApplyConfiguration(new MarcaVehiculoConfiguration());
            modelBuilder.ApplyConfiguration(new CiudadConfiguration());
            modelBuilder.ApplyConfiguration(new PaisConfiguration());

            // =========================
            // CONFIGURACIONES SEGURIDAD Y AUDITORÍA
            // =========================
            modelBuilder.ApplyConfiguration(new UsuarioAppConfiguration());
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioRolConfiguration());
            modelBuilder.ApplyConfiguration(new AuditoriaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}