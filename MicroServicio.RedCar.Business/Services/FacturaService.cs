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

        public FacturaService(IFacturaDataService facturaDataService)
        {
            _facturaDataService = facturaDataService;
        }

        // =========================
        // CREAR
        // =========================
        public async Task<FacturaResponse> CrearAsync(CrearFacturaRequest request, CancellationToken cancellationToken = default)
        {
            var errors = FacturaValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ValidationException("La solicitud de creación de factura es inválida.", errors);

            var model = FacturaBusinessMapper.ToDataModel(request);

            var creado = await _facturaDataService.CrearAsync(model, cancellationToken);

            return FacturaBusinessMapper.ToResponse(creado);
        }

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

            var model = FacturaBusinessMapper.ToDataModel(request);

            // PRESERVAR AUDITORÍA
            model.guid_factura = existente.guid_factura;
            model.fecha_registro_utc = existente.fecha_registro_utc;
            model.creado_por_usuario = existente.creado_por_usuario;

            var actualizado = await _facturaDataService.ActualizarAsync(model, cancellationToken);

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

            var existente = await _facturaDataService.ObtenerPorIdAsync(request.id_factura, cancellationToken);

            if (existente is null)
                throw new NotFoundException("Factura no encontrada.");

            if (existente.estado == "APR")
                throw new BusinessException("La factura ya está aprobada.");

            var model = FacturaBusinessMapper.ToDataModel(request);

            var actualizado = await _facturaDataService.ActualizarAsync(model, cancellationToken);

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

            var existente = await _facturaDataService.ObtenerPorIdAsync(request.id_factura, cancellationToken);

            if (existente is null)
                throw new NotFoundException("Factura no encontrada.");

            var model = FacturaBusinessMapper.ToDataModel(request);

            var actualizado = await _facturaDataService.ActualizarAsync(model, cancellationToken);

            return FacturaBusinessMapper.ToResponse(actualizado!);
        }

        // =========================
        // ELIMINAR LÓGICO
        // =========================
        public async Task EliminarLogicoAsync(int id_factura, string usuario, CancellationToken cancellationToken = default)
        {
            var eliminado = await _facturaDataService.EliminarLogicoAsync(id_factura, usuario, null, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró la factura para eliminar.");
        }
    }
}