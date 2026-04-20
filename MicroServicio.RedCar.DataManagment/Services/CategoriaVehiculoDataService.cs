using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class CategoriaVehiculoDataService : ICategoriaVehiculoDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaVehiculoDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<CategoriaVehiculoDataModel?> ObtenerPorIdAsync(int id_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerPorIdAsync(id_categoria_vehiculo, cancellationToken);

            return entity is null
                ? null
                : CategoriaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<CategoriaVehiculoDataModel?> ObtenerPorGuidAsync(Guid categoria_vehiculo_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerPorGuidAsync(categoria_vehiculo_guid, cancellationToken);

            return entity is null
                ? null
                : CategoriaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<CategoriaVehiculoDataModel?> ObtenerPorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerPorCodigoAsync(codigo_categoria_vehiculo, cancellationToken);

            return entity is null
                ? null
                : CategoriaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<CategoriaVehiculoDataModel?> ObtenerPorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerPorNombreAsync(nombre_categoria_vehiculo, cancellationToken);

            return entity is null
                ? null
                : CategoriaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<CategoriaVehiculoDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.CategoriaVehiculoRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(CategoriaVehiculoDataMapper.ToDataModel)
                .ToList();
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<CategoriaVehiculoDataModel> CrearAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = CategoriaVehiculoDataMapper.ToEntity(model);

            await _unitOfWork.CategoriaVehiculoRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CategoriaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<CategoriaVehiculoDataModel?> ActualizarAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerParaActualizarAsync(model.id_categoria_vehiculo, cancellationToken);

            if (entity is null)
                return null;

            entity.codigo_categoria_vehiculo = model.codigo_categoria_vehiculo;
            entity.nombre_categoria_vehiculo = model.nombre_categoria_vehiculo;
            entity.descripcion_categoria_vehiculo = model.descripcion_categoria_vehiculo;

            entity.estado_categoria_vehiculo = model.estado_categoria_vehiculo;
            entity.es_eliminado = model.es_eliminado;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificado_desde_ip = model.modificado_desde_ip;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.origen_registro = model.origen_registro;

            _unitOfWork.CategoriaVehiculoRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CategoriaVehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_categoria_vehiculo, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerParaActualizarAsync(id_categoria_vehiculo, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_categoria_vehiculo = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            _unitOfWork.CategoriaVehiculoRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CategoriaVehiculoRepository.ExistePorCodigoAsync(codigo_categoria_vehiculo, cancellationToken);
        }

        public async Task<bool> ExistePorNombreAsync(string nombre_categoria_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CategoriaVehiculoRepository.ExistePorNombreAsync(nombre_categoria_vehiculo, cancellationToken);
        }
    }
}