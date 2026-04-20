using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class UsuarioAppDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static UsuarioAppDataModel ToDataModel(UsuarioAppEntity entity)
        {
            return new UsuarioAppDataModel
            {
                id_usuario = entity.id_usuario,
                usuario_guid = entity.usuario_guid,

                username = entity.username,
                correo = entity.correo,

                password_hash = entity.password_hash,
                password_salt = entity.password_salt,

                estado_usuario = entity.estado_usuario,
                es_eliminado = entity.es_eliminado,
                activo = entity.activo,

                id_cliente = entity.id_cliente,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,

                row_version = entity.row_version
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static UsuarioAppEntity ToEntity(UsuarioAppDataModel model)
        {
            return new UsuarioAppEntity
            {
                id_usuario = model.id_usuario,
                usuario_guid = model.usuario_guid,

                username = model.username,
                correo = model.correo,

                password_hash = model.password_hash,
                password_salt = model.password_salt,

                estado_usuario = model.estado_usuario,
                es_eliminado = model.es_eliminado,
                activo = model.activo,

                id_cliente = model.id_cliente,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,

                row_version = model.row_version
            };
        }
    }
}