using MicroServicio.RedCar.DataAcces.Common;
using MicroServicio.RedCar.DataAccess.Entities;

namespace MicroServicio.RedCar.DataAccess.Repositories.Interfaces
{
    public interface IConductorRepository
    {
        // =========================
        // CONSULTAS
        // =========================
        Task<IReadOnlyList<ConductorEntity>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerParaActualizarAsync(int id_conductor, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<ConductorEntity?> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        Task<PagedResult<ConductorEntity>> BuscarAsync(
            string? codigo_conductor,
            string? tipo_identificacion,
            string? numero_identificacion,
            string? con_nombre1,
            string? con_nombre2,
            string? con_apellido1,
            string? con_apellido2,
            string? numero_licencia,
            DateTime? fecha_vencimiento_licencia_desde,
            DateTime? fecha_vencimiento_licencia_hasta,
            byte? edad_conductor,
            string? con_telefono,
            string? con_correo,
            string? estado_conductor,
            string? origen_registro,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        // =========================
        // COMANDOS
        // =========================
        Task AgregarAsync(ConductorEntity conductor, CancellationToken cancellationToken = default);

        void Actualizar(ConductorEntity conductor);

        // =========================
        // VALIDACIONES
        // =========================
        Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default);

        Task<bool> ExistePorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default);

        Task<bool> ExistePorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default);
    }
}