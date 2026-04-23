using MicroServicio.RedCar.Business.DTOs.Conductor;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class ConductorService : IConductorService
    {
        private readonly IConductorDataService _conductorDataService;

        public ConductorService(IConductorDataService conductorDataService)
        {
            _conductorDataService = conductorDataService;
        }

        // =========================
        // CREAR
        // =========================
        public async Task<ConductorResponse> CrearAsync(CrearConductorRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ConductorValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de creación de conductor es inválida.", errors);

            if (await _conductorDataService.ExistePorCodigoAsync(request.codigo_conductor, cancellationToken))
                throw new ValidationException("Ya existe un conductor con ese código.");

            if (await _conductorDataService.ExistePorIdentificacionAsync(request.numero_identificacion, cancellationToken))
                throw new ValidationException("Ya existe un conductor con esa identificación.");

            if (await _conductorDataService.ExistePorLicenciaAsync(request.numero_licencia, cancellationToken))
                throw new ValidationException("Ya existe un conductor con esa licencia.");

            var model = ConductorBusinessMapper.ToDataModel(request);
            var creado = await _conductorDataService.CrearAsync(model, cancellationToken);

            return ConductorBusinessMapper.ToResponse(creado);
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public async Task<ConductorResponse> ActualizarAsync(ActualizarConductorRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ConductorValidator.ValidarActualizacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de actualización de conductor es inválida.", errors);

            var existente = await _conductorDataService.ObtenerPorIdAsync(request.id_conductor!.Value, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró el conductor.");

            var porCodigo = await _conductorDataService.ObtenerPorCodigoAsync(request.codigo_conductor!, cancellationToken);
            if (porCodigo != null && porCodigo.id_conductor != request.id_conductor!.Value)
                throw new ValidationException("Ya existe otro conductor con ese código.");

            var porIdentificacion = await _conductorDataService.ObtenerPorIdentificacionAsync(request.numero_identificacion!, cancellationToken);
            if (porIdentificacion != null && porIdentificacion.id_conductor != request.id_conductor!.Value)
                throw new ValidationException("Ya existe otro conductor con esa identificación.");

            var porLicencia = await _conductorDataService.ObtenerPorLicenciaAsync(request.numero_licencia!, cancellationToken);
            if (porLicencia != null && porLicencia.id_conductor != request.id_conductor!.Value)
                throw new ValidationException("Ya existe otro conductor con esa licencia.");

            var model = ConductorBusinessMapper.ToDataModel(request);

            model.conductor_guid = existente.conductor_guid;
            model.fecha_registro_utc = existente.fecha_registro_utc;
            model.creado_por_usuario = existente.creado_por_usuario;

            var actualizado = await _conductorDataService.ActualizarAsync(model, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar el conductor.");

            return ConductorBusinessMapper.ToResponse(actualizado);
        }

        // =========================
        // BUSCAR
        // =========================
        public async Task<DataPagedResult<ConductorResponse>> BuscarAsync(ConductorFiltroRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ConductorValidator.ValidarFiltro(request);
            if (errors.Any())
                throw new ValidationException("Los parámetros de búsqueda son inválidos.", errors);

            var filtro = ConductorBusinessMapper.ToFiltroDataModel(request);
            var resultado = await _conductorDataService.BuscarAsync(filtro, cancellationToken);

            return new DataPagedResult<ConductorResponse>
            {
                Items = resultado.Items
                    .Select(ConductorBusinessMapper.ToResponse)
                    .ToList(),
                TotalRecords = resultado.TotalRecords,
                PageNumber = resultado.PageNumber,
                PageSize = resultado.PageSize
            };
        }

        // =========================
        // OBTENER
        // =========================
        public async Task<ConductorResponse> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default)
        {
            var conductor = await _conductorDataService.ObtenerPorIdAsync(id_conductor, cancellationToken);

            if (conductor is null)
                throw new NotFoundException("Conductor no encontrado.");

            return ConductorBusinessMapper.ToResponse(conductor);
        }

        public async Task<ConductorResponse> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default)
        {
            var conductor = await _conductorDataService.ObtenerPorGuidAsync(conductor_guid, cancellationToken);

            if (conductor is null)
                throw new NotFoundException("Conductor no encontrado.");

            return ConductorBusinessMapper.ToResponse(conductor);
        }

        public async Task<ConductorResponse> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default)
        {
            var conductor = await _conductorDataService.ObtenerPorCodigoAsync(codigo_conductor, cancellationToken);

            if (conductor is null)
                throw new NotFoundException("Conductor no encontrado.");

            return ConductorBusinessMapper.ToResponse(conductor);
        }

        public async Task<ConductorResponse> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            var conductor = await _conductorDataService.ObtenerPorIdentificacionAsync(numero_identificacion, cancellationToken);

            if (conductor is null)
                throw new NotFoundException("Conductor no encontrado.");

            return ConductorBusinessMapper.ToResponse(conductor);
        }

        public async Task<ConductorResponse> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default)
        {
            var conductor = await _conductorDataService.ObtenerPorLicenciaAsync(numero_licencia, cancellationToken);

            if (conductor is null)
                throw new NotFoundException("Conductor no encontrado.");

            return ConductorBusinessMapper.ToResponse(conductor);
        }

        public async Task<IReadOnlyList<ConductorResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var conductores = await _conductorDataService.ObtenerTodosAsync(cancellationToken);

            return conductores.Select(x => ConductorBusinessMapper.ToResponse(x)).ToList();
        }

        // =========================
        // ELIMINAR LÓGICO
        // =========================
        public async Task EliminarLogicoAsync(int id_conductor, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ValidationException("El motivo es obligatorio para inhabilitar un conductor.");

            var eliminado = await _conductorDataService.EliminarLogicoAsync(id_conductor, usuario, motivo, ip, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró el conductor para eliminar.");
        }
    }
}