using MicroServicio.RedCar.Business.DTOs.MarcaVehiculo;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IMarcaVehiculoService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<MarcaVehiculoResponse?> ObtenerPorIdAsync(int id);
        Task<MarcaVehiculoResponse?> ObtenerPorGuidAsync(Guid guid);
        Task<MarcaVehiculoResponse?> ObtenerPorCodigoAsync(string codigo);
        Task<IReadOnlyList<MarcaVehiculoResponse>> ObtenerTodosAsync();

        Task<DataPagedResult<MarcaVehiculoResponse>> BuscarAsync(MarcaVehiculoFiltroRequest filtro);

        // =========================
        // COMANDOS
        // =========================
        Task<MarcaVehiculoResponse> CrearAsync(CrearMarcaVehiculoRequest request);
        Task<MarcaVehiculoResponse?> ActualizarAsync(ActualizarMarcaVehiculoRequest request);
        Task<bool> EliminarAsync(int id, string usuario, string? motivo, string? ip);
    }
}