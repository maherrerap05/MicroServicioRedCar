using MicroServicio.RedCar.Business.DTOs.Reserva;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaDataService _reservaDataService;
        private readonly IVehiculoDataService _vehiculoDataService;

        public ReservaService(
            IReservaDataService reservaDataService,
            IVehiculoDataService vehiculoDataService)
        {
            _reservaDataService = reservaDataService;
            _vehiculoDataService = vehiculoDataService;
        }

        // =========================
        // CREAR
        // =========================
        public async Task<ReservaResponse> CrearAsync(CrearReservaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ReservaValidator.ValidarCreacion(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de creación de la reserva es inválida.", errors);

            var existeCodigo = await _reservaDataService.ExistePorCodigoAsync(request.codigo_reserva, cancellationToken);

            if (existeCodigo)
                throw new ValidationException("Ya existe una reserva con el código indicado.");

            var fechaInicio = request.fecha_recogida.Date + request.hora_recogida;
            var fechaFin = request.fecha_devolucion.Date + request.hora_devolucion;

            var disponible = await _vehiculoDataService.EstaDisponibleAsync(
                request.id_vehiculo,
                fechaInicio,
                fechaFin,
                cancellationToken);

            if (!disponible)
                throw new BusinessException("El vehículo no está disponible en el rango de fechas seleccionado.");

            var dataModel = ReservaBusinessMapper.ToDataModel(request);

            var creado = await _reservaDataService.CrearAsync(
                dataModel,
                null,
                null,
                cancellationToken);

            // Crear extras y conductores ya con ID
            var extras = request.extras
                .Select(x => ReservaBusinessMapper.ToDataModel(x, creado.id_reserva))
                .ToList();

            var conductores = request.conductores
                .Select(x => ReservaBusinessMapper.ToDataModel(x, creado.id_reserva))
                .ToList();

            var actualizado = await _reservaDataService.ActualizarAsync(
                creado,
                extras,
                conductores,
                cancellationToken);

            return ReservaBusinessMapper.ToResponse(actualizado!, conductores, extras);
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public async Task<ReservaResponse> ActualizarAsync(ActualizarReservaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ReservaValidator.ValidarActualizacion(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de actualización es inválida.", errors);

            var existente = await _reservaDataService.ObtenerPorIdAsync(request.id_reserva, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró la reserva.");

            var dataModel = ReservaBusinessMapper.ToDataModel(request);

            // 🔒 preservar datos
            dataModel.guid_reserva = existente.guid_reserva;
            dataModel.fecha_reserva_utc = existente.fecha_reserva_utc;
            dataModel.fecha_registro_utc = existente.fecha_registro_utc;
            dataModel.creado_por_usuario = existente.creado_por_usuario;
            dataModel.row_version = existente.row_version;

            var extras = request.extras
                .Select(x => ReservaBusinessMapper.ToDataModel(x, request.id_reserva))
                .ToList();

            var conductores = request.conductores
                .Select(x => ReservaBusinessMapper.ToDataModel(x, request.id_reserva))
                .ToList();

            var actualizado = await _reservaDataService.ActualizarAsync(
                dataModel,
                extras,
                conductores,
                cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar la reserva.");

            return ReservaBusinessMapper.ToResponse(actualizado, conductores, extras);
        }

        // =========================
        // CONFIRMAR
        // =========================
        public async Task<ReservaResponse> ConfirmarAsync(ConfirmarReservaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ReservaValidator.ValidarConfirmacion(request);

            if (errors.Any())
                throw new ValidationException("Solicitud inválida.", errors);

            var existente = await _reservaDataService.ObtenerPorIdAsync(request.id_reserva, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró la reserva.");

            if (existente.estado_reserva != "PEN")
                throw new BusinessException("Solo se pueden confirmar reservas en estado PEN.");

            var dataModel = ReservaBusinessMapper.ToDataModel(request);

            var actualizado = await _reservaDataService.ActualizarAsync(dataModel, null, null, cancellationToken);

            return ReservaBusinessMapper.ToResponse(actualizado!);
        }

        // =========================
        // CANCELAR
        // =========================
        public async Task<ReservaResponse> CancelarAsync(CancelarReservaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ReservaValidator.ValidarCancelacion(request);

            if (errors.Any())
                throw new ValidationException("Solicitud inválida.", errors);

            var existente = await _reservaDataService.ObtenerPorIdAsync(request.id_reserva, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró la reserva.");

            if (existente.estado_reserva == "CAN")
                throw new BusinessException("La reserva ya está cancelada.");

            var dataModel = ReservaBusinessMapper.ToDataModel(request);

            var actualizado = await _reservaDataService.ActualizarAsync(dataModel, null, null, cancellationToken);

            return ReservaBusinessMapper.ToResponse(actualizado!);
        }

        // =========================
        // ELIMINAR
        // =========================
        public async Task EliminarLogicoAsync(int id_reserva, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var eliminado = await _reservaDataService.EliminarLogicoAsync(id_reserva, usuario, motivo, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró la reserva para eliminar.");
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<ReservaResponse> ObtenerPorIdAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            var reserva = await _reservaDataService.ObtenerPorIdAsync(id_reserva, cancellationToken);

            if (reserva is null)
                throw new NotFoundException("Reserva no encontrada.");

            var conductores = await _reservaDataService.ObtenerConductoresPorReservaAsync(id_reserva, cancellationToken);
            var extras = await _reservaDataService.ObtenerExtrasPorReservaAsync(id_reserva, cancellationToken);

            return ReservaBusinessMapper.ToResponse(reserva, conductores, extras);
        }

        public async Task<ReservaResponse> ObtenerPorGuidAsync(Guid guid_reserva, CancellationToken cancellationToken = default)
        {
            var reserva = await _reservaDataService.ObtenerPorGuidAsync(guid_reserva, cancellationToken);

            if (reserva is null)
                throw new NotFoundException("Reserva no encontrada.");

            var conductores = await _reservaDataService.ObtenerConductoresPorReservaAsync(reserva.id_reserva, cancellationToken);
            var extras = await _reservaDataService.ObtenerExtrasPorReservaAsync(reserva.id_reserva, cancellationToken);

            return ReservaBusinessMapper.ToResponse(reserva, conductores, extras);
        }

        public async Task<ReservaResponse> ObtenerPorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default)
        {
            var reserva = await _reservaDataService.ObtenerPorCodigoAsync(codigo_reserva, cancellationToken);

            if (reserva is null)
                throw new NotFoundException("Reserva no encontrada.");

            var conductores = await _reservaDataService.ObtenerConductoresPorReservaAsync(reserva.id_reserva, cancellationToken);
            var extras = await _reservaDataService.ObtenerExtrasPorReservaAsync(reserva.id_reserva, cancellationToken);

            return ReservaBusinessMapper.ToResponse(reserva, conductores, extras);
        }

        public async Task<IReadOnlyList<ReservaResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var reservas = await _reservaDataService.ObtenerTodosAsync(cancellationToken);

            return reservas.Select(x => ReservaBusinessMapper.ToResponse(x)).ToList();
        }

        public async Task<DataPagedResult<ReservaResponse>> BuscarAsync(ReservaFiltroRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ReservaValidator.ValidarFiltro(request);

            if (errors.Any())
                throw new ValidationException("Filtro inválido.", errors);

            var filtro = new ReservaFiltroDataModel
            {
                codigo_reserva = request.codigo_reserva,
                id_cliente = request.id_cliente,
                id_vehiculo = request.id_vehiculo,
                id_localizacion_recogida = request.id_localizacion_recogida,
                id_localizacion_devolucion = request.id_localizacion_devolucion,
                estado_reserva = request.estado_reserva,
                origen_canal_reserva = request.origen_canal_reserva,
                fecha_inicio_desde = request.fecha_recogida_desde,
                fecha_inicio_hasta = request.fecha_recogida_hasta,
                fecha_fin_desde = request.fecha_devolucion_desde,
                fecha_fin_hasta = request.fecha_devolucion_hasta,
                PageNumber = request.page_number,
                PageSize = request.page_size
            };

            var result = await _reservaDataService.BuscarAsync(filtro, cancellationToken);

            return new DataPagedResult<ReservaResponse>
            {
                Items = result.Items.Select(x => ReservaBusinessMapper.ToResponse(x)).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<IReadOnlyList<ReservaResponse>> ObtenerHistorialPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            var reservas = await _reservaDataService.ObtenerHistorialPorClienteAsync(id_cliente, cancellationToken);

            return reservas.Select(x => ReservaBusinessMapper.ToResponse(x)).ToList();
        }

        public async Task<IReadOnlyList<ReservaResponse>> ObtenerReservasActivasAsync(CancellationToken cancellationToken = default)
        {
            var reservas = await _reservaDataService.ObtenerReservasActivasAsync(cancellationToken);

            return reservas.Select(x => ReservaBusinessMapper.ToResponse(x)).ToList();
        }

        public async Task<IReadOnlyList<ReservaResponse>> ObtenerReservasPorVehiculoAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            var reservas = await _reservaDataService.ObtenerReservasPorVehiculoAsync(id_vehiculo, cancellationToken);

            return reservas.Select(x => ReservaBusinessMapper.ToResponse(x)).ToList();
        }
    }
}