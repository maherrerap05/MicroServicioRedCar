using MicroServicio.RedCar.Business.DTOs.Conductor;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Interfaces
{
    public interface IConductorService
    {
        Task<ConductorResponse> CrearAsync(CrearConductorRequest request, CancellationToken cancellationToken = default);

        Task<ConductorResponse> ActualizarAsync(ActualizarConductorRequest request, CancellationToken cancellationToken = default);

        Task<ConductorResponse> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default);

        Task<ConductorResponse> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default);

        Task<ConductorResponse> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);

        Task<ConductorResponse> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<ConductorResponse> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ConductorResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<DataPagedResult<ConductorResponse>> BuscarAsync(ConductorFiltroRequest request, CancellationToken cancellationToken = default);

        Task EliminarLogicoAsync(int id_conductor, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default);
    }
}