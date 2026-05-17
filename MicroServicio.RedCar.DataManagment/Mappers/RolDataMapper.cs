using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class RolDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static RolDataModel ToDataModel(RolEntity entity)
        {
            return new RolDataModel
            {
                id_rol = entity.id_rol,
                rol_guid = entity.rol_guid,

                nombre_rol = entity.nombre_rol,
                descripcion_rol = entity.descripcion_rol,

                estado_rol = entity.estado_rol,
                es_eliminado = entity.es_eliminado,
                activo = entity.activo,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,

                
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static RolEntity ToEntity(RolDataModel model)
        {
            return new RolEntity
            {
                id_rol = model.id_rol,
                rol_guid = model.rol_guid == Guid.Empty ? Guid.NewGuid() : model.rol_guid,

                nombre_rol = model.nombre_rol,
                descripcion_rol = model.descripcion_rol,

                estado_rol = model.estado_rol,
                es_eliminado = model.es_eliminado,
                activo = model.activo,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,

                
            };
        }
    }
}