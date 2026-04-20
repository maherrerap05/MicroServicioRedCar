using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class UsuarioAppDataService : IUsuarioAppDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioAppDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<UsuarioAppDataModel?> ObtenerPorIdAsync(int id_usuario, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.UsuarioAppRepository.ObtenerPorIdAsync(id_usuario, cancellationToken);

            return entity is null
                ? null
                : UsuarioAppDataMapper.ToDataModel(entity);
        }

        public async Task<UsuarioAppDataModel?> ObtenerPorGuidAsync(Guid usuario_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.UsuarioAppRepository.ObtenerPorGuidAsync(usuario_guid, cancellationToken);

            return entity is null
                ? null
                : UsuarioAppDataMapper.ToDataModel(entity);
        }

        public async Task<UsuarioAppDataModel?> ObtenerPorUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.UsuarioAppRepository.ObtenerPorUserNameAsync(userName, cancellationToken);

            return entity is null
                ? null
                : UsuarioAppDataMapper.ToDataModel(entity);
        }

        public async Task<UsuarioAppDataModel?> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.UsuarioAppRepository.ObtenerPorCorreoAsync(correo, cancellationToken);

            return entity is null
                ? null
                : UsuarioAppDataMapper.ToDataModel(entity);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<UsuarioAppDataModel> CrearAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = UsuarioAppDataMapper.ToEntity(model);

            await _unitOfWork.UsuarioAppRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return UsuarioAppDataMapper.ToDataModel(entity);
        }

        public async Task<UsuarioAppDataModel?> ActualizarAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.UsuarioAppRepository.ObtenerParaActualizarAsync(model.id_usuario, cancellationToken);

            if (entity is null)
                return null;

            entity.username = model.username;
            entity.correo = model.correo;

            entity.password_hash = model.password_hash;
            entity.password_salt = model.password_salt;

            entity.estado_usuario = model.estado_usuario;
            entity.es_eliminado = model.es_eliminado;
            entity.activo = model.activo;

            entity.id_cliente = model.id_cliente;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;

            _unitOfWork.UsuarioAppRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return UsuarioAppDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(int id_usuario, string usuario, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.UsuarioAppRepository.ObtenerParaActualizarAsync(id_usuario, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_usuario = "INA";
            entity.es_eliminado = true;
            entity.activo = false;
            entity.modificado_por_usuario = usuario;
            entity.fecha_modificacion_utc = DateTime.UtcNow;

            _unitOfWork.UsuarioAppRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UsuarioAppRepository.ExistePorUserNameAsync(userName, cancellationToken);
        }

        public async Task<bool> ExistePorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.UsuarioAppRepository.ExistePorCorreoAsync(correo, cancellationToken);
        }
    }
}