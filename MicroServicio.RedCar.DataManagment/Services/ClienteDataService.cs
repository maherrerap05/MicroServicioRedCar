using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class ClienteDataService : IClienteDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<ClienteDataModel?> ObtenerPorIdAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ClienteRepository.ObtenerPorIdAsync(id_cliente, cancellationToken);

            return entity is null
                ? null
                : ClienteDataMapper.ToDataModel(entity);
        }

        public async Task<ClienteDataModel?> ObtenerPorGuidAsync(Guid cliente_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ClienteRepository.ObtenerPorGuidAsync(cliente_guid, cancellationToken);

            return entity is null
                ? null
                : ClienteDataMapper.ToDataModel(entity);
        }

        public async Task<ClienteDataModel?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ClienteRepository.ObtenerPorIdentificacionAsync(numero_identificacion, cancellationToken);

            return entity is null
                ? null
                : ClienteDataMapper.ToDataModel(entity);
        }

        public async Task<ClienteDataModel?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ClienteRepository.ObtenerPorCorreoAsync(correo, cancellationToken);

            return entity is null
                ? null
                : ClienteDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<ClienteDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ClienteRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(ClienteDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<DataPagedResult<ClienteDataModel>> BuscarAsync(ClienteFiltroDataModel filtro, CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.ClienteQueryRepository.BuscarAsync(
                filtro.PageNumber,
                filtro.PageSize,
                cancellationToken);

            return new DataPagedResult<ClienteDataModel>
            {
                Items = result.Items
                    .Select(ClienteDataMapper.ToDataModel)
                    .ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<ClienteDataModel> CrearAsync(ClienteDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = ClienteDataMapper.ToEntity(model);

            await _unitOfWork.ClienteRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ClienteDataMapper.ToDataModel(entity);
        }

        public async Task<ClienteDataModel?> ActualizarAsync(ClienteDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ClienteRepository.ObtenerParaActualizarAsync(model.id_cliente, cancellationToken);

            if (entity is null)
                return null;

            // =========================
            // DATOS PRINCIPALES
            // =========================
            entity.tipo_identificacion = model.tipo_identificacion;
            entity.numero_identificacion = model.numero_identificacion;
            entity.razon_social = model.razon_social;

            entity.nombres = model.nombres;
            entity.apellidos = model.apellidos;

            entity.correo = model.correo;
            entity.telefono = model.telefono;
            entity.direccion = model.direccion;

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            entity.estado = model.estado;
            entity.es_eliminado = model.es_eliminado;

            // =========================
            // AUDITORÍA / ORIGEN
            // =========================
            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificacion_ip = model.modificacion_ip;

            entity.servicio_origen = model.servicio_origen;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            _unitOfWork.ClienteRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ClienteDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_cliente, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ClienteRepository.ObtenerParaActualizarAsync(id_cliente, cancellationToken);

            if (entity is null)
                return false;

            entity.estado = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            _unitOfWork.ClienteRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ClienteRepository.ExistePorIdentificacionAsync(numero_identificacion, cancellationToken);
        }

        public async Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ClienteRepository.ExistePorCorreoAsync(correo, cancellationToken);
        }
    }
}