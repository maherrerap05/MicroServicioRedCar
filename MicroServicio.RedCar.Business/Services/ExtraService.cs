using MicroServicio.RedCar.Business.DTOs.Extra;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class ExtraService : IExtraService
    {
        private readonly IExtraDataService _extraDataService;

        public ExtraService(IExtraDataService extraDataService)
        {
            _extraDataService = extraDataService;
        }

        // =========================
        // CREAR
        // =========================
        public async Task<ExtraResponse> CrearAsync(CrearExtraRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ExtraValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de creación del extra es inválida.", errors);

            if (await _extraDataService.ExistePorCodigoAsync(request.codigo_extra, cancellationToken))
                throw new ValidationException("Ya existe un extra con ese código.");

            var model = ExtraBusinessMapper.ToDataModel(request);

            var creado = await _extraDataService.CrearAsync(model, cancellationToken);

            return ExtraBusinessMapper.ToResponse(creado);
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public async Task<ExtraResponse> ActualizarAsync(ActualizarExtraRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ExtraValidator.ValidarActualizacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de actualización del extra es inválida.", errors);

            var existente = await _extraDataService.ObtenerPorIdAsync(request.id_extra, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró el extra.");

            var duplicado = await _extraDataService.ObtenerPorCodigoAsync(request.codigo_extra, cancellationToken);
            if (duplicado != null && duplicado.id_extra != request.id_extra)
                throw new ValidationException("Ya existe otro extra con ese código.");

            var model = ExtraBusinessMapper.ToDataModel(request);

            // PRESERVAR AUDITORÍA
            model.extra_guid = existente.extra_guid;
            model.fecha_registro_utc = existente.fecha_registro_utc;
            model.creado_por_usuario = existente.creado_por_usuario;

            var actualizado = await _extraDataService.ActualizarAsync(model, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar el extra.");

            return ExtraBusinessMapper.ToResponse(actualizado);
        }

        // =========================
        // OBTENER
        // =========================
        public async Task<ExtraResponse> ObtenerPorIdAsync(int id_extra, CancellationToken cancellationToken = default)
        {
            var extra = await _extraDataService.ObtenerPorIdAsync(id_extra, cancellationToken);

            if (extra is null)
                throw new NotFoundException("Extra no encontrado.");

            return ExtraBusinessMapper.ToResponse(extra);
        }

        public async Task<ExtraResponse> ObtenerPorGuidAsync(Guid extra_guid, CancellationToken cancellationToken = default)
        {
            var extra = await _extraDataService.ObtenerPorGuidAsync(extra_guid, cancellationToken);

            if (extra is null)
                throw new NotFoundException("Extra no encontrado.");

            return ExtraBusinessMapper.ToResponse(extra);
        }

        public async Task<ExtraResponse> ObtenerPorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default)
        {
            var extra = await _extraDataService.ObtenerPorCodigoAsync(codigo_extra, cancellationToken);

            if (extra is null)
                throw new NotFoundException("Extra no encontrado.");

            return ExtraBusinessMapper.ToResponse(extra);
        }

        public async Task<IReadOnlyList<ExtraResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var extras = await _extraDataService.ObtenerTodosAsync(cancellationToken);

            return extras.Select(x => ExtraBusinessMapper.ToResponse(x)).ToList();
        }

        // =========================
        // BUSCAR
        // =========================
        public async Task<DataPagedResult<ExtraResponse>> BuscarAsync(ExtraFiltroRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ExtraValidator.ValidarFiltro(request);
            if (errors.Any())
                throw new ValidationException("Filtro inválido.", errors);

            var extras = await _extraDataService.ObtenerTodosAsync(cancellationToken);

            // =========================
            // FILTROS EN MEMORIA
            // =========================
            var query = extras.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.codigo_extra))
                query = query.Where(x => x.codigo_extra.Contains(request.codigo_extra));

            if (!string.IsNullOrWhiteSpace(request.nombre_extra))
                query = query.Where(x => x.nombre_extra.Contains(request.nombre_extra));

            if (!string.IsNullOrWhiteSpace(request.estado_extra))
                query = query.Where(x => x.estado_extra == request.estado_extra);

            if (request.valor_fijo_desde.HasValue)
                query = query.Where(x => x.valor_fijo >= request.valor_fijo_desde.Value);

            if (request.valor_fijo_hasta.HasValue)
                query = query.Where(x => x.valor_fijo <= request.valor_fijo_hasta.Value);

            var total = query.Count();

            var items = query
                .Skip((request.page_number - 1) * request.page_size)
                .Take(request.page_size)
                .ToList();

            return new DataPagedResult<ExtraResponse>
            {
                Items = items.Select(ExtraBusinessMapper.ToResponse).ToList(),
                PageNumber = request.page_number,
                PageSize = request.page_size,
                TotalRecords = total
            };
        }

        // =========================
        // ELIMINAR LÓGICO
        // =========================
        public async Task EliminarLogicoAsync(int id_extra, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ValidationException("El motivo es obligatorio para inhabilitar un extra.");

            var eliminado = await _extraDataService.EliminarLogicoAsync(id_extra, usuario, motivo, ip, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró el extra para eliminar.");
        }
    }
}