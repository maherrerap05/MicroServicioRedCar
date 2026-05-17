using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class LocalizacionDataMapper
    {
        public static LocalizacionDataModel ToDataModel(LocalizacionEntity entity)
        {
            return new LocalizacionDataModel
            {
                id_localizacion = entity.id_localizacion,
                localizacion_guid = entity.localizacion_guid,

                codigo_localizacion = entity.codigo_localizacion,
                nombre_localizacion = entity.nombre_localizacion,
                direccion_localizacion = entity.direccion_localizacion,
                telefono_contacto = entity.telefono_contacto,
                correo_contacto = entity.correo_contacto,
                horario_atencion = entity.horario_atencion,

                zona_horaria = entity.zona_horaria,

                id_ciudad = entity.id_ciudad,

                estado_localizacion = entity.estado_localizacion,
                es_eliminado = entity.es_eliminado,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificado_desde_ip = entity.modificado_desde_ip,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,
                origen_registro = entity.origen_registro
            };
        }

        public static LocalizacionEntity ToEntity(LocalizacionDataModel model)
        {
            return new LocalizacionEntity
            {
                id_localizacion = model.id_localizacion,
                localizacion_guid = model.localizacion_guid == Guid.Empty ? Guid.NewGuid() : model.localizacion_guid,

                codigo_localizacion = model.codigo_localizacion,
                nombre_localizacion = model.nombre_localizacion,
                direccion_localizacion = model.direccion_localizacion,
                telefono_contacto = model.telefono_contacto,
                correo_contacto = model.correo_contacto,
                horario_atencion = model.horario_atencion,

                zona_horaria = model.zona_horaria,

                id_ciudad = model.id_ciudad,

                estado_localizacion = model.estado_localizacion,
                es_eliminado = model.es_eliminado,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,
                origen_registro = model.origen_registro
            };
        }
    }
}