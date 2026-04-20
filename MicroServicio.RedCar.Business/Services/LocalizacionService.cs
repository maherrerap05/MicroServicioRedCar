using MicroServicio.RedCar.Business.DTOs.Localizacion;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class LocalizacionService : ILocalizacionService
    {
        private readonly ILocalizacionDataService _dataService;

        public LocalizacionService(ILocalizacionDataService dataService)
        {
            _dataService = dataService;
        }

        // =========================
        // CREAR
        // =========================
        public async Task<LocalizacionResponse> CrearAsync(CrearLocalizacionRequest request, CancellationToken cancellationToken = default)
        {
            var errors = LocalizacionValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ValidationException("Solicitud inválida.", errors);

            if (await _dataService.ExistePorCodigoAsync(request.codigo_localizacion, cancellationToken))
                throw new ValidationException("Ya existe una localización con ese código.");

            var model = LocalizacionBusinessMapper.ToDataModel(request);

            var creado = await _dataService.CrearAsync(model, cancellationToken);

            return LocalizacionBusinessMapper.ToResponse(creado);
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public async Task<LocalizacionResponse> ActualizarAsync(ActualizarLocalizacionRequest request, CancellationToken cancellationToken = default)
        {
            var errors = LocalizacionValidator.ValidarActualizacion(request);
            if (errors.Any())
                throw new ValidationException("Solicitud inválida.", errors);

            var existente = await _dataService.ObtenerPorIdAsync(request.id_localizacion, cancellationToken);

            if (existente is null)
                throw new NotFoundException("Localización no encontrada.");

            var duplicado = await _dataService.ObtenerPorCodigoAsync(request.codigo_localizacion, cancellationToken);
            if (duplicado != null && duplicado.id_localizacion != request.id_localizacion)
                throw new ValidationException("Ya existe otra localización con ese código.");

            var model = LocalizacionBusinessMapper.ToDataModel(request);

            // PRESERVAR AUDITORÍA
            model.localizacion_guid = existente.localizacion_guid;
            model.fecha_registro_utc = existente.fecha_registro_utc;
            model.creado_por_usuario = existente.creado_por_usuario;

            var actualizado = await _dataService.ActualizarAsync(model, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar.");

            return LocalizacionBusinessMapper.ToResponse(actualizado);
        }

        // =========================
        // OBTENER
        // =========================
        public async Task<LocalizacionResponse> ObtenerPorIdAsync(int id_localizacion, CancellationToken cancellationToken = default)
        {
            var data = await _dataService.ObtenerPorIdAsync(id_localizacion, cancellationToken);

            if (data is null)
                throw new NotFoundException("Localización no encontrada.");

            return LocalizacionBusinessMapper.ToResponse(data);
        }

        public async Task<LocalizacionResponse> ObtenerPorGuidAsync(Guid localizacion_guid, CancellationToken cancellationToken = default)
        {
            var data = await _dataService.ObtenerPorGuidAsync(localizacion_guid, cancellationToken);

            if (data is null)
                throw new NotFoundException("Localización no encontrada.");

            return LocalizacionBusinessMapper.ToResponse(data);
        }

        public async Task<LocalizacionResponse> ObtenerPorCodigoAsync(string codigo_localizacion, CancellationToken cancellationToken = default)
        {
            var data = await _dataService.ObtenerPorCodigoAsync(codigo_localizacion, cancellationToken);

            if (data is null)
                throw new NotFoundException("Localización no encontrada.");

            return LocalizacionBusinessMapper.ToResponse(data);
        }

        public async Task<IReadOnlyList<LocalizacionResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var lista = await _dataService.ObtenerTodosAsync(cancellationToken);

            return lista.Select(LocalizacionBusinessMapper.ToResponse).ToList();
        }

        // =========================
        // BUSCAR (EN MEMORIA)
        // =========================
        public async Task<DataPagedResult<LocalizacionResponse>> BuscarAsync(LocalizacionFiltroRequest request, CancellationToken cancellationToken = default)
        {
            var errors = LocalizacionValidator.ValidarFiltro(request);
            if (errors.Any())
                throw new ValidationException("Filtro inválido.", errors);

            var lista = await _dataService.ObtenerTodosAsync(cancellationToken);

            var query = lista.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.codigo_localizacion))
                query = query.Where(x => x.codigo_localizacion.Contains(request.codigo_localizacion));

            if (!string.IsNullOrWhiteSpace(request.nombre_localizacion))
                query = query.Where(x => x.nombre_localizacion.Contains(request.nombre_localizacion));

            if (!string.IsNullOrWhiteSpace(request.zona_horaria))
                query = query.Where(x => x.zona_horaria.Contains(request.zona_horaria));

            if (!string.IsNullOrWhiteSpace(request.estado_localizacion))
                query = query.Where(x => x.estado_localizacion == request.estado_localizacion);

            if (request.id_ciudad.HasValue)
                query = query.Where(x => x.id_ciudad == request.id_ciudad.Value);

            var total = query.Count();

            var items = query
                .Skip((request.page_number - 1) * request.page_size)
                .Take(request.page_size)
                .ToList();

            return new DataPagedResult<LocalizacionResponse>
            {
                Items = items.Select(LocalizacionBusinessMapper.ToResponse).ToList(),
                PageNumber = request.page_number,
                PageSize = request.page_size,
                TotalRecords = total
            };
        }

        // =========================
        // ELIMINAR
        // =========================
        public async Task EliminarLogicoAsync(int id_localizacion, string usuario, CancellationToken cancellationToken = default)
        {
            var eliminado = await _dataService.EliminarLogicoAsync(id_localizacion, usuario, null, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró la localización.");
        }
    }
}