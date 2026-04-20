using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class ExtraDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static ExtraDataModel ToDataModel(ExtraEntity entity)
        {
            return new ExtraDataModel
            {
                id_extra = entity.id_extra,
                extra_guid = entity.extra_guid,

                codigo_extra = entity.codigo_extra,
                nombre_extra = entity.nombre_extra,
                descripcion_extra = entity.descripcion_extra,

                valor_fijo = entity.valor_fijo,

                estado_extra = entity.estado_extra,
                es_eliminado = entity.es_eliminado,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificado_desde_ip = entity.modificado_desde_ip,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,

                row_version = entity.row_version,
                origen_registro = entity.origen_registro
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static ExtraEntity ToEntity(ExtraDataModel model)
        {
            return new ExtraEntity
            {
                id_extra = model.id_extra,
                extra_guid = model.extra_guid,

                codigo_extra = model.codigo_extra,
                nombre_extra = model.nombre_extra,
                descripcion_extra = model.descripcion_extra,

                valor_fijo = model.valor_fijo,

                estado_extra = model.estado_extra,
                es_eliminado = model.es_eliminado,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,

                row_version = model.row_version,
                origen_registro = model.origen_registro
            };
        }
    }
}