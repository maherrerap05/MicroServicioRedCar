using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class FacturaDataService : IFacturaDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacturaDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<FacturaDataModel?> ObtenerPorIdAsync(int id_factura, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FacturaRepository.ObtenerPorIdAsync(id_factura, cancellationToken);

            return entity is null
                ? null
                : FacturaDataMapper.ToDataModel(entity);
        }

        public async Task<FacturaDataModel?> ObtenerPorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FacturaRepository.ObtenerPorGuidAsync(guid_factura, cancellationToken);

            return entity is null
                ? null
                : FacturaDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<FacturaDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.FacturaRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(FacturaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<DataPagedResult<FacturaDataModel>> BuscarAsync(FacturaFiltroDataModel filtro, CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.FacturaQueryRepository.BuscarAsync(
                filtro.numero_factura,
                filtro.id_cliente,
                filtro.id_reserva,
                filtro.estado,
                filtro.origen_canal_factura,
                filtro.fecha_emision_desde,
                filtro.fecha_emision_hasta,
                filtro.total_min,
                filtro.total_max,
                filtro.PageNumber,
                filtro.PageSize,
                cancellationToken);

            return new DataPagedResult<FacturaDataModel>
            {
                Items = result.Items
                    .Select(FacturaDataMapper.ToDataModel)
                    .ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<IReadOnlyList<FacturaDataModel>> ObtenerPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.FacturaQueryRepository.ObtenerPorClienteAsync(id_cliente, cancellationToken);

            return entities
                .Select(FacturaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<IReadOnlyList<FacturaDataModel>> ObtenerFacturasActivasAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.FacturaQueryRepository.ObtenerFacturasActivasAsync(cancellationToken);

            return entities
                .Select(FacturaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<FacturaDataModel?> ObtenerFacturaPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FacturaQueryRepository.ObtenerFacturaPorReservaAsync(id_reserva, cancellationToken);

            return entity is null
                ? null
                : FacturaDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<FacturaDataModel>> ObtenerPorEstadoAsync(string estado, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.FacturaQueryRepository.ObtenerPorEstadoAsync(estado, cancellationToken);

            return entities
                .Select(FacturaDataMapper.ToDataModel)
                .ToList();
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<FacturaDataModel> CrearAsync(FacturaDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = FacturaDataMapper.ToEntity(model);

            await _unitOfWork.FacturaRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return FacturaDataMapper.ToDataModel(entity);
        }

        public async Task<FacturaDataModel?> ActualizarAsync(FacturaDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FacturaRepository.ObtenerParaActualizarAsync(model.id_factura, cancellationToken);

            if (entity is null)
                return null;

            entity.id_cliente = model.id_cliente;
            entity.id_reserva = model.id_reserva;

            entity.numero_factura = model.numero_factura;
            entity.fecha_emision = model.fecha_emision;

            entity.subtotal = model.subtotal;
            entity.valor_iva = model.valor_iva;
            entity.total = model.total;

            entity.observaciones_factura = model.observaciones_factura;
            entity.origen_canal_factura = model.origen_canal_factura;

            entity.estado = model.estado;
            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.es_eliminado = model.es_eliminado;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificacion_ip = model.modificacion_ip;

            entity.servicio_origen = model.servicio_origen;

            _unitOfWork.FacturaRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return FacturaDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_factura, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.FacturaRepository.ObtenerParaActualizarAsync(id_factura, cancellationToken);

            if (entity is null)
                return false;

            entity.estado = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            _unitOfWork.FacturaRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorGuidAsync(Guid guid_factura, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.FacturaRepository.ExistePorGuidAsync(guid_factura, cancellationToken);
        }
    }
}