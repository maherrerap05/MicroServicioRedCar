using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class LocalizacionDataService : ILocalizacionDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocalizacionDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LocalizacionDataModel?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.LocalizacionRepository.ObtenerPorIdAsync(id, cancellationToken);
            return entity is null ? null : LocalizacionDataMapper.ToDataModel(entity);
        }

        public async Task<LocalizacionDataModel?> ObtenerPorGuidAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.LocalizacionRepository.ObtenerPorGuidAsync(guid, cancellationToken);
            return entity is null ? null : LocalizacionDataMapper.ToDataModel(entity);
        }

        public async Task<LocalizacionDataModel?> ObtenerPorCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.LocalizacionRepository.ObtenerPorCodigoAsync(codigo, cancellationToken);
            return entity is null ? null : LocalizacionDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<LocalizacionDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.LocalizacionRepository.ObtenerTodosAsync(cancellationToken);
            return entities.Select(LocalizacionDataMapper.ToDataModel).ToList();
        }

        public async Task<LocalizacionDataModel> CrearAsync(LocalizacionDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = LocalizacionDataMapper.ToEntity(model);

            await _unitOfWork.LocalizacionRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return LocalizacionDataMapper.ToDataModel(entity);
        }

        public async Task<LocalizacionDataModel?> ActualizarAsync(LocalizacionDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.LocalizacionRepository.ObtenerParaActualizarAsync(model.id_localizacion, cancellationToken);

            if (entity is null)
                return null;

            entity.codigo_localizacion = model.codigo_localizacion;
            entity.nombre_localizacion = model.nombre_localizacion;
            entity.direccion_localizacion = model.direccion_localizacion;
            entity.telefono_contacto = model.telefono_contacto;
            entity.correo_contacto = model.correo_contacto;
            entity.horario_atencion = model.horario_atencion;
            entity.zona_horaria = model.zona_horaria;

            entity.id_ciudad = model.id_ciudad;

            entity.estado_localizacion = model.estado_localizacion;
            entity.es_eliminado = model.es_eliminado;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificado_desde_ip = model.modificado_desde_ip;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.origen_registro = model.origen_registro;

            _unitOfWork.LocalizacionRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return LocalizacionDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.LocalizacionRepository.ObtenerParaActualizarAsync(id, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_localizacion = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            _unitOfWork.LocalizacionRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.LocalizacionRepository.ExistePorCodigoAsync(codigo, cancellationToken);
        }
    }
}