using MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface ICategoriaVehiculoService
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<CategoriaVehiculoResponse?> ObtenerPorIdAsync(int id);
        Task<CategoriaVehiculoResponse?> ObtenerPorGuidAsync(Guid guid);
        Task<CategoriaVehiculoResponse?> ObtenerPorCodigoAsync(string codigo);
        Task<IReadOnlyList<CategoriaVehiculoResponse>> ObtenerTodosAsync();

        Task<DataPagedResult<CategoriaVehiculoResponse>> BuscarAsync(CategoriaVehiculoFiltroRequest filtro);

        // =========================
        // COMANDOS
        // =========================
        Task<CategoriaVehiculoResponse> CrearAsync(CrearCategoriaVehiculoRequest request);
        Task<CategoriaVehiculoResponse?> ActualizarAsync(ActualizarCategoriaVehiculoRequest request);
        Task<bool> EliminarAsync(int id, string usuario, string? motivo);
    }
}