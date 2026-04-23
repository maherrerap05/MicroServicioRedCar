using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class ExtraDataService : IExtraDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExtraDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<ExtraDataModel?> ObtenerPorIdAsync(int id_extra, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ExtraRepository.ObtenerPorIdAsync(id_extra, cancellationToken);

            return entity is null
                ? null
                : ExtraDataMapper.ToDataModel(entity);
        }

        public async Task<ExtraDataModel?> ObtenerPorGuidAsync(Guid extra_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ExtraRepository.ObtenerPorGuidAsync(extra_guid, cancellationToken);

            return entity is null
                ? null
                : ExtraDataMapper.ToDataModel(entity);
        }

        public async Task<ExtraDataModel?> ObtenerPorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ExtraRepository.ObtenerPorCodigoAsync(codigo_extra, cancellationToken);

            return entity is null
                ? null
                : ExtraDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<ExtraDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ExtraRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(ExtraDataMapper.ToDataModel)
                .ToList();
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<ExtraDataModel> CrearAsync(ExtraDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = ExtraDataMapper.ToEntity(model);

            await _unitOfWork.ExtraRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ExtraDataMapper.ToDataModel(entity);
        }

        public async Task<ExtraDataModel?> ActualizarAsync(ExtraDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ExtraRepository.ObtenerParaActualizarAsync(model.id_extra, cancellationToken);

            if (entity is null)
                return null;

            entity.codigo_extra = model.codigo_extra;
            entity.nombre_extra = model.nombre_extra;
            entity.descripcion_extra = model.descripcion_extra;

            entity.valor_fijo = model.valor_fijo;

            entity.estado_extra = model.estado_extra;
            entity.es_eliminado = model.es_eliminado;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificado_desde_ip = model.modificado_desde_ip;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.origen_registro = model.origen_registro;

            _unitOfWork.ExtraRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ExtraDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_extra, string usuario, string? motivo, string? ip, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ExtraRepository.ObtenerParaActualizarAsync(id_extra, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_extra = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;
            entity.modificado_desde_ip = ip;          // ← nuevo

            _unitOfWork.ExtraRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_extra, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ExtraRepository.ExistePorCodigoAsync(codigo_extra, cancellationToken);
        }
    }
}