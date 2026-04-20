using Microsoft.EntityFrameworkCore;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Services;
using MicroServicio.RedCar.DataAccess.Context;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Services;

namespace MicroServicio.RedCar.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RedCarDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RedCarDb")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // =========================
        // DATA MANAGEMENT
        // =========================
        services.AddScoped<IAuditoriaDataService, AuditoriaDataService>();
        services.AddScoped<ICategoriaVehiculoDataService, CategoriaVehiculoDataService>();
        services.AddScoped<IClienteDataService, ClienteDataService>();
        services.AddScoped<IConductorDataService, ConductorDataService>();
        services.AddScoped<IExtraDataService, ExtraDataService>();
        services.AddScoped<IFacturaDataService, FacturaDataService>();
        services.AddScoped<ILocalizacionDataService, LocalizacionDataService>();
        services.AddScoped<IMarcaVehiculoDataService, MarcaVehiculoDataService>();
        services.AddScoped<IReservaDataService, ReservaDataService>();
        services.AddScoped<IUsuarioAppDataService, UsuarioAppDataService>();
        services.AddScoped<IVehiculoDataService, VehiculoDataService>();

        // =========================
        // BUSINESS
        // =========================
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoriaVehiculoService, CategoriaVehiculoService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IConductorService, ConductorService>();
        services.AddScoped<IExtraService, ExtraService>();
        services.AddScoped<IFacturaService, FacturaService>();
        services.AddScoped<ILocalizacionService, LocalizacionService>();
        services.AddScoped<IMarcaVehiculoService, MarcaVehiculoService>();
        services.AddScoped<IReservaService, ReservaService>();
        services.AddScoped<IVehiculoService, VehiculoService>();

        return services;
    }
}