using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class ConductorDataService : IConductorDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConductorDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<ConductorDataModel?> ObtenerPorIdAsync(int id_conductor, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerPorIdAsync(id_conductor, cancellationToken);

            return entity is null
                ? null
                : ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<ConductorDataModel?> ObtenerPorGuidAsync(Guid conductor_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerPorGuidAsync(conductor_guid, cancellationToken);

            return entity is null
                ? null
                : ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<ConductorDataModel?> ObtenerPorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerPorCodigoAsync(codigo_conductor, cancellationToken);

            return entity is null
                ? null
                : ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<ConductorDataModel?> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerPorIdentificacionAsync(numero_identificacion, cancellationToken);

            return entity is null
                ? null
                : ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<ConductorDataModel?> ObtenerPorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerPorLicenciaAsync(numero_licencia, cancellationToken);

            return entity is null
                ? null
                : ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<ConductorDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ConductorRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(ConductorDataMapper.ToDataModel)
                .ToList();
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<ConductorDataModel> CrearAsync(ConductorDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = ConductorDataMapper.ToEntity(model);

            await _unitOfWork.ConductorRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<ConductorDataModel?> ActualizarAsync(ConductorDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerParaActualizarAsync(model.id_conductor, cancellationToken);

            if (entity is null)
                return null;

            entity.codigo_conductor = model.codigo_conductor;

            entity.tipo_identificacion = model.tipo_identificacion;
            entity.numero_identificacion = model.numero_identificacion;

            entity.con_nombre1 = model.con_nombre1;
            entity.con_nombre2 = model.con_nombre2;

            entity.con_apellido1 = model.con_apellido1;
            entity.con_apellido2 = model.con_apellido2;

            entity.numero_licencia = model.numero_licencia;
            entity.fecha_vencimiento_licencia = model.fecha_vencimiento_licencia;

            entity.edad_conductor = model.edad_conductor;

            entity.con_telefono = model.con_telefono;
            entity.con_correo = model.con_correo;

            entity.estado_conductor = model.estado_conductor;
            entity.es_eliminado = model.es_eliminado;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificado_desde_ip = model.modificado_desde_ip;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.origen_registro = model.origen_registro;

            _unitOfWork.ConductorRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ConductorDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_conductor, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ConductorRepository.ObtenerParaActualizarAsync(id_conductor, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_conductor = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            _unitOfWork.ConductorRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ConductorRepository.ExistePorIdentificacionAsync(numero_identificacion, cancellationToken);
        }

        public async Task<bool> ExistePorLicenciaAsync(string numero_licencia, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ConductorRepository.ExistePorLicenciaAsync(numero_licencia, cancellationToken);
        }

        public async Task<bool> ExistePorCodigoAsync(string codigo_conductor, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ConductorRepository.ExistePorCodigoAsync(codigo_conductor, cancellationToken);
        }
    }
}