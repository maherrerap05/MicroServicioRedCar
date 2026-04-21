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
        private readonly IExtraDataService _extraDataService;

        // IVA fijo del 15% según normativa vigente
        private const decimal TasaIva = 0.15m;

        public ReservaService(
            IReservaDataService reservaDataService,
            IVehiculoDataService vehiculoDataService,
            IExtraDataService extraDataService)
        {
            _reservaDataService = reservaDataService;
            _vehiculoDataService = vehiculoDataService;
            _extraDataService = extraDataService;
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

            // ── Validar disponibilidad del vehículo ──────────────────────────
            var disponible = await _vehiculoDataService.EstaDisponibleAsync(
                request.id_vehiculo,
                fechaInicio,
                fechaFin,
                cancellationToken);

            if (!disponible)
                throw new BusinessException("El vehículo no está disponible en el rango de fechas seleccionado.");

            // ── Obtener precio base del vehículo ─────────────────────────────
            var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(
                request.id_vehiculo,
                cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException($"No se encontró el vehículo con id {request.id_vehiculo}.");

            // ── Conductores ──────────────────────────────────────────────────
            var conductores = request.conductores
                .Select(x => ReservaBusinessMapper.ToDataModel(x, 0))
                .ToList();

            // ── Extras: consulta precio real de cada extra ───────────────────
            var extras = new List<ReservaExtraDataModel>();

            foreach (var extraRequest in request.extras)
            {
                var extraData = await _extraDataService.ObtenerPorIdAsync(
                    extraRequest.id_extra,
                    cancellationToken);

                if (extraData is null)
                    throw new NotFoundException(
                        $"No se encontró el extra con id {extraRequest.id_extra}.");

                extras.Add(ReservaBusinessMapper.ToDataModel(
                    extraRequest,
                    0,
                    extraData.valor_fijo));
            }

            // ── Cálculo de totales ───────────────────────────────────────────
            var cantidadDias = (int)Math.Ceiling((fechaFin - fechaInicio).TotalDays);
            var subtotalVehiculo = vehiculo.precio_base_dia * cantidadDias;
            var subtotalExtras = extras.Sum(x => x.subtotal_extra);
            var subtotalReserva = subtotalVehiculo + subtotalExtras;
            var valorIva = Math.Round(subtotalReserva * TasaIva, 2);
            var totalReserva = subtotalReserva + valorIva;

            var dataModel = ReservaBusinessMapper.ToDataModel(request);
            dataModel.subtotal_reserva = subtotalReserva;
            dataModel.valor_iva = valorIva;
            dataModel.total_reserva = totalReserva;

            // ── Crear reserva + conductores + extras en una sola operación ───
            var creado = await _reservaDataService.CrearAsync(
                dataModel,
                extras,
                conductores,
                cancellationToken);

            return ReservaBusinessMapper.ToResponse(creado, conductores, extras);
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

            // ── Obtener precio base del vehículo ─────────────────────────────
            var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(
                request.id_vehiculo,
                cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException($"No se encontró el vehículo con id {request.id_vehiculo}.");

            // ── Conductores ──────────────────────────────────────────────────
            var conductores = request.conductores
                .Select(x => ReservaBusinessMapper.ToDataModel(x, request.id_reserva))
                .ToList();

            // ── Extras: consulta precio real de cada extra ───────────────────
            var extras = new List<ReservaExtraDataModel>();

            foreach (var extraRequest in request.extras)
            {
                var extraData = await _extraDataService.ObtenerPorIdAsync(
                    extraRequest.id_extra,
                    cancellationToken);

                if (extraData is null)
                    throw new NotFoundException(
                        $"No se encontró el extra con id {extraRequest.id_extra}.");

                extras.Add(ReservaBusinessMapper.ToDataModel(
                    extraRequest,
                    request.id_reserva,
                    extraData.valor_fijo));
            }

            // ── Cálculo de totales ───────────────────────────────────────────
            var fechaInicio = request.fecha_recogida.Date + request.hora_recogida;
            var fechaFin = request.fecha_devolucion.Date + request.hora_devolucion;
            var cantidadDias = (int)Math.Ceiling((fechaFin - fechaInicio).TotalDays);
            var subtotalVehiculo = vehiculo.precio_base_dia * cantidadDias;
            var subtotalExtras = extras.Sum(x => x.subtotal_extra);
            var subtotalReserva = subtotalVehiculo + subtotalExtras;
            var valorIva = Math.Round(subtotalReserva * TasaIva, 2);
            var totalReserva = subtotalReserva + valorIva;

            var dataModel = ReservaBusinessMapper.ToDataModel(request);
            dataModel.subtotal_reserva = subtotalReserva;
            dataModel.valor_iva = valorIva;
            dataModel.total_reserva = totalReserva;

            // 🔒 Preservar datos inmutables de la reserva original
            dataModel.guid_reserva = existente.guid_reserva;
            dataModel.fecha_reserva_utc = existente.fecha_reserva_utc;
            dataModel.fecha_registro_utc = existente.fecha_registro_utc;
            dataModel.creado_por_usuario = existente.creado_por_usuario;
            dataModel.row_version = existente.row_version;

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

            // 🔒 Preservar todos los campos inmutables de la reserva original
            dataModel.codigo_reserva = existente.codigo_reserva;
            dataModel.id_cliente = existente.id_cliente;
            dataModel.id_vehiculo = existente.id_vehiculo;
            dataModel.id_localizacion_recogida = existente.id_localizacion_recogida;
            dataModel.id_localizacion_devolucion = existente.id_localizacion_devolucion;
            dataModel.fecha_reserva_utc = existente.fecha_reserva_utc;
            dataModel.fecha_recogida = existente.fecha_recogida;
            dataModel.hora_recogida = existente.hora_recogida;
            dataModel.fecha_devolucion = existente.fecha_devolucion;
            dataModel.hora_devolucion = existente.hora_devolucion;
            dataModel.fecha_hora_recogida = existente.fecha_hora_recogida;
            dataModel.fecha_hora_devolucion = existente.fecha_hora_devolucion;
            dataModel.fecha_inicio = existente.fecha_inicio;
            dataModel.fecha_fin = existente.fecha_fin;
            dataModel.cantidad_dias_reserva = existente.cantidad_dias_reserva;
            dataModel.subtotal_reserva = existente.subtotal_reserva;
            dataModel.valor_iva = existente.valor_iva;
            dataModel.total_reserva = existente.total_reserva;
            dataModel.observaciones_reserva = existente.observaciones_reserva;
            dataModel.origen_canal_reserva = existente.origen_canal_reserva;
            dataModel.fecha_registro_utc = existente.fecha_registro_utc;
            dataModel.creado_por_usuario = existente.creado_por_usuario;
            dataModel.guid_reserva = existente.guid_reserva;
            dataModel.row_version = existente.row_version;
            dataModel.servicio_origen = existente.servicio_origen;

            var actualizado = await _reservaDataService.ActualizarAsync(dataModel, null, null, cancellationToken);

            // ── Actualizar localización del vehículo ─────────────────────────────
            // Al confirmar se asume que el viaje se realizó y el vehículo
            // se encuentra ahora en la localización de devolución acordada.
            await _vehiculoDataService.ActualizarLocalizacionAsync(
                existente.id_vehiculo,
                existente.id_localizacion_devolucion,
                request.modificado_por_usuario,
                cancellationToken);

            // ── Aprobar conductores y extras ─────────────────────────────────────
            // Al confirmar la reserva se cambia el estado de todos los conductores
            // y extras activos a APR para reflejar la aprobación formal.
            await _reservaDataService.AprobarConductoresYExtrasAsync(
                request.id_reserva,
                request.modificado_por_usuario,
                cancellationToken);

            // ── Consultar conductores y extras para el response ───────────────────
            var conductores = await _reservaDataService.ObtenerConductoresPorReservaAsync(
                request.id_reserva, cancellationToken);
            var extras = await _reservaDataService.ObtenerExtrasPorReservaAsync(
                request.id_reserva, cancellationToken);

            return ReservaBusinessMapper.ToResponse(actualizado!, conductores, extras);
        }

        // =========================
        // ELIMINAR
        // =========================
        public async Task EliminarLogicoAsync(int id_reserva, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            // El motivo es obligatorio porque al eliminar se asigna estado_reserva = 'CAN',
            // y el constraint CHK_RESERVAS_CANCELACION_MOTIVO_COHERENTE exige que
            // motivo_cancelacion no sea nulo cuando el estado es CAN.
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ValidationException("El motivo es obligatorio para eliminar una reserva.");

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