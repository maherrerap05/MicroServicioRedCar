using MicroServicio.RedCar.Business.DTOs.Factura;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaDataService _facturaDataService;
        private readonly IReservaDataService _reservaDataService;

        public FacturaService(IFacturaDataService facturaDataService, IReservaDataService reservaDataService)
        {
            _facturaDataService = facturaDataService;
            _reservaDataService = reservaDataService;
        }

        // =========================
        // CREAR
        // =========================
        public async Task<FacturaResponse> CrearAsync(CrearFacturaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = FacturaValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de creación de factura es inválida.", errors);

            // 1. Verificar que la reserva existe
            var reserva = await _reservaDataService.ObtenerPorIdAsync(request.id_reserva, cancellationToken);
            if (reserva is null)
                throw new NotFoundException("No se encontró la reserva indicada.");

            // 2. Solo se puede facturar reservas confirmadas
            if (reserva.estado_reserva != "CON")
                throw new BusinessException(
                    reserva.estado_reserva == "CAN"
                        ? "No se puede generar una factura para una reserva cancelada."
                        : "Solo se pueden facturar reservas confirmadas.");

            // 3. Verificar que la reserva no tenga ya una factura
            var facturaExistente = await _facturaDataService.ObtenerFacturaPorReservaAsync(request.id_reserva, cancellationToken);
            if (facturaExistente is not null)
                throw new BusinessException("Ya existe una factura asociada a esta reserva.");

            // 4. Mapear y poblar campos derivados de la reserva
            var model = FacturaBusinessMapper.ToDataModel(request);

            model.id_cliente = reserva.id_cliente;
            model.subtotal = reserva.subtotal_reserva;
            model.valor_iva = reserva.valor_iva;
            model.total = reserva.total_reserva;

            var creado = await _facturaDataService.CrearAsync(model, cancellationToken);

            return FacturaBusinessMapper.ToResponse(creado);
        }

        // =========================
        // ACTUALIZAR
        // =========================
        // =========================
        // ACTUALIZAR
        // =========================
        public async Task<FacturaResponse> ActualizarAsync(ActualizarFacturaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = FacturaValidator.ValidarActualizacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de actualización de factura es inválida.", errors);

            var existente = await _facturaDataService.ObtenerPorIdAsync(request.id_factura, cancellationToken);
            if (existente is null)
                throw new NotFoundException("No se encontró la factura.");

            existente.observaciones_factura = request.observaciones_factura;
            existente.origen_canal_factura = request.origen_canal_factura;
            existente.modificado_por_usuario = request.modificado_por_usuario;
            existente.fecha_modificacion_utc = DateTime.UtcNow;
            existente.modificacion_ip = request.modificacion_ip;
            existente.servicio_origen = request.servicio_origen;
            // motivo_inhabilitacion NO se toca — lo gestiona el flujo de eliminación

            var actualizado = await _facturaDataService.ActualizarAsync(existente, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar la factura.");

            return FacturaBusinessMapper.ToResponse(actualizado);
        }

        // =========================
        // OBTENER
        // =========================
        public async Task<FacturaResponse> ObtenerPorIdAsync(int id_factura, CancellationToken cancellationToken = default)
        {
            var factura = await _facturaDataService.ObtenerPorIdAsync(id_factura, cancellationToken);

            if (factura is null)
                throw new NotFoundException("Factura no encontrada.");

            return FacturaBusinessMapper.ToResponse(factura);
        }

        public async Task<FacturaResponse> ObtenerPorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default)
        {
            var factura = await _facturaDataService.ObtenerPorGuidAsync(guid_factura, cancellationToken);

            if (factura is null)
                throw new NotFoundException("Factura no encontrada.");

            return FacturaBusinessMapper.ToResponse(factura);
        }

        public async Task<IReadOnlyList<FacturaResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var facturas = await _facturaDataService.ObtenerTodosAsync(cancellationToken);

            return facturas.Select(x => FacturaBusinessMapper.ToResponse(x)).ToList();
        }

        public async Task<IReadOnlyList<FacturaResponse>> ObtenerPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            var facturas = await _facturaDataService.ObtenerPorClienteAsync(id_cliente, cancellationToken);

            return facturas.Select(x => FacturaBusinessMapper.ToResponse(x)).ToList();
        }

        public async Task<IReadOnlyList<FacturaResponse>> ObtenerFacturasActivasAsync(CancellationToken cancellationToken = default)
        {
            var facturas = await _facturaDataService.ObtenerFacturasActivasAsync(cancellationToken);

            return facturas.Select(x => FacturaBusinessMapper.ToResponse(x)).ToList();
        }

        public async Task<FacturaResponse> ObtenerPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            var factura = await _facturaDataService.ObtenerFacturaPorReservaAsync(id_reserva, cancellationToken);

            if (factura is null)
                throw new NotFoundException("No existe factura para la reserva.");

            return FacturaBusinessMapper.ToResponse(factura);
        }

        public async Task<IReadOnlyList<FacturaResponse>> ObtenerPorEstadoAsync(string estado, CancellationToken cancellationToken = default)
        {
            var facturas = await _facturaDataService.ObtenerPorEstadoAsync(estado, cancellationToken);

            return facturas.Select(x => FacturaBusinessMapper.ToResponse(x)).ToList();
        }

        // =========================
        // BUSCAR (PAGINADO)
        // =========================
        public async Task<DataPagedResult<FacturaResponse>> BuscarAsync(FacturaFiltroRequest request, CancellationToken cancellationToken = default)
        {
            var errors = FacturaValidator.ValidarFiltro(request);
            if (errors.Any())
                throw new ValidationException("Filtro inválido.", errors);

            var filtro = new FacturaFiltroDataModel
            {
                numero_factura = request.numero_factura,
                id_cliente = request.id_cliente,
                id_reserva = request.id_reserva,
                estado = request.estado,
                origen_canal_factura = request.origen_canal_factura,
                fecha_emision_desde = request.fecha_emision_desde,
                fecha_emision_hasta = request.fecha_emision_hasta,
                PageNumber = request.page_number,
                PageSize = request.page_size
            };

            var result = await _facturaDataService.BuscarAsync(filtro, cancellationToken);

            return new DataPagedResult<FacturaResponse>
            {
                Items = result.Items.Select(x => FacturaBusinessMapper.ToResponse(x)).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        // =========================
        // APROBAR
        // =========================
        public async Task<FacturaResponse> AprobarAsync(AprobarFacturaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = FacturaValidator.ValidarAprobacion(request);
            if (errors.Any())
                throw new ValidationException("Solicitud de aprobación inválida.", errors);

            // 1. Recuperar la factura existente
            var existente = await _facturaDataService.ObtenerPorIdAsync(request.id_factura, cancellationToken);

            if (existente is null)
                throw new NotFoundException("Factura no encontrada.");

            if (existente.estado == "APR")
                throw new BusinessException("La factura ya está aprobada.");

            if (existente.estado == "INA")
                throw new BusinessException("Una factura anulada no puede aprobarse.");

            // 2. Mutar solo los campos propios de la aprobación sobre el model existente
            existente.estado = "APR";
            existente.modificado_por_usuario = request.modificado_por_usuario;
            existente.fecha_modificacion_utc = DateTime.UtcNow;
            existente.modificacion_ip = request.modificacion_ip;
            existente.servicio_origen = request.servicio_origen;

            var actualizado = await _facturaDataService.ActualizarAsync(existente, cancellationToken);

            return FacturaBusinessMapper.ToResponse(actualizado!);
        }

        // =========================
        // ANULAR
        // =========================
        public async Task<FacturaResponse> AnularAsync(AnularFacturaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = FacturaValidator.ValidarAnulacion(request);
            if (errors.Any())
                throw new ValidationException("Solicitud de anulación inválida.", errors);

            // 1. Recuperar la factura existente
            var existente = await _facturaDataService.ObtenerPorIdAsync(request.id_factura, cancellationToken);

            if (existente is null)
                throw new NotFoundException("Factura no encontrada.");

            if (existente.estado == "INA")
                throw new BusinessException("La factura ya está anulada.");

            if (existente.estado == "APR")
                throw new BusinessException("Una factura aprobada no puede anularse.");

            // 2. Mutar solo los campos propios de la anulación sobre el model existente
            existente.estado = "INA";
            existente.es_eliminado = true;              // ← línea que faltaba
            existente.motivo_inhabilitacion = request.motivo_inhabilitacion;
            existente.fecha_inhabilitacion_utc = DateTime.UtcNow;
            existente.modificado_por_usuario = request.modificado_por_usuario;
            existente.fecha_modificacion_utc = DateTime.UtcNow;
            existente.modificacion_ip = request.modificacion_ip;
            existente.servicio_origen = request.servicio_origen;

            var actualizado = await _facturaDataService.ActualizarAsync(existente, cancellationToken);

            return FacturaBusinessMapper.ToResponse(actualizado!);
        }

        // =========================
        // ELIMINAR LÓGICO
        // =========================
        public async Task EliminarLogicoAsync(int id_factura, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ValidationException("El motivo es obligatorio para inhabilitar una factura.");

            var eliminado = await _facturaDataService.EliminarLogicoAsync(id_factura, usuario, motivo, ip, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró la factura para eliminar.");
        }
    }
}