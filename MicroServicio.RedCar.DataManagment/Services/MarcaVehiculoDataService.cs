using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class MarcaVehiculoDataService : IMarcaVehiculoDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarcaVehiculoDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<MarcaVehiculoDataModel?> ObtenerPorIdAsync(int id_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.MarcaVehiculoRepository.ObtenerPorIdAsync(id_marca_vehiculo, cancellationToken);

            return entity is null
                ? null
                : MarcaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<MarcaVehiculoDataModel?> ObtenerPorGuidAsync(Guid marca_vehiculo_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.MarcaVehiculoRepository.ObtenerPorGuidAsync(marca_vehiculo_guid, cancellationToken);

            return entity is null
                ? null
                : MarcaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<MarcaVehiculoDataModel?> ObtenerPorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.MarcaVehiculoRepository.ObtenerPorCodigoAsync(codigo_marca_vehiculo, cancellationToken);

            return entity is null
                ? null
                : MarcaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<MarcaVehiculoDataModel?> ObtenerPorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.MarcaVehiculoRepository.ObtenerPorNombreAsync(nombre_marca_vehiculo, cancellationToken);

            return entity is null
                ? null
                : MarcaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<MarcaVehiculoDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.MarcaVehiculoRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(MarcaVehiculoDataMapper.ToDataModel)
                .ToList();
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<MarcaVehiculoDataModel> CrearAsync(MarcaVehiculoDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = MarcaVehiculoDataMapper.ToEntity(model);

            await _unitOfWork.MarcaVehiculoRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MarcaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<MarcaVehiculoDataModel?> ActualizarAsync(MarcaVehiculoDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.MarcaVehiculoRepository.ObtenerParaActualizarAsync(model.id_marca_vehiculo, cancellationToken);

            if (entity is null)
                return null;

            entity.codigo_marca_vehiculo = model.codigo_marca_vehiculo;
            entity.nombre_marca_vehiculo = model.nombre_marca_vehiculo;
            entity.descripcion_marca_vehiculo = model.descripcion_marca_vehiculo;

            entity.estado_marca_vehiculo = model.estado_marca_vehiculo;
            entity.es_eliminado = model.es_eliminado;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificado_desde_ip = model.modificado_desde_ip;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.origen_registro = model.origen_registro;

            _unitOfWork.MarcaVehiculoRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MarcaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_marca_vehiculo, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.MarcaVehiculoRepository.ObtenerParaActualizarAsync(id_marca_vehiculo, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_marca_vehiculo = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;
            entity.modificado_desde_ip = ip;          // ← nuevo

            _unitOfWork.MarcaVehiculoRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.MarcaVehiculoRepository.ExistePorCodigoAsync(codigo_marca_vehiculo, cancellationToken);
        }

        public async Task<bool> ExistePorNombreAsync(string nombre_marca_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.MarcaVehiculoRepository.ExistePorNombreAsync(nombre_marca_vehiculo, cancellationToken);
        }
    }
}