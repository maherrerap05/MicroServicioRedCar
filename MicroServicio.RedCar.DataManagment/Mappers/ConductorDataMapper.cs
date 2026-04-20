using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class ConductorDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static ConductorDataModel ToDataModel(ConductorEntity entity)
        {
            return new ConductorDataModel
            {
                id_conductor = entity.id_conductor,
                conductor_guid = entity.conductor_guid,

                codigo_conductor = entity.codigo_conductor,

                tipo_identificacion = entity.tipo_identificacion,
                numero_identificacion = entity.numero_identificacion,

                con_nombre1 = entity.con_nombre1,
                con_nombre2 = entity.con_nombre2,

                con_apellido1 = entity.con_apellido1,
                con_apellido2 = entity.con_apellido2,

                numero_licencia = entity.numero_licencia,
                fecha_vencimiento_licencia = entity.fecha_vencimiento_licencia,

                edad_conductor = entity.edad_conductor,

                con_telefono = entity.con_telefono,
                con_correo = entity.con_correo,

                estado_conductor = entity.estado_conductor,
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
        public static ConductorEntity ToEntity(ConductorDataModel model)
        {
            return new ConductorEntity
            {
                id_conductor = model.id_conductor,
                conductor_guid = model.conductor_guid,

                codigo_conductor = model.codigo_conductor,

                tipo_identificacion = model.tipo_identificacion,
                numero_identificacion = model.numero_identificacion,

                con_nombre1 = model.con_nombre1,
                con_nombre2 = model.con_nombre2,

                con_apellido1 = model.con_apellido1,
                con_apellido2 = model.con_apellido2,

                numero_licencia = model.numero_licencia,
                fecha_vencimiento_licencia = model.fecha_vencimiento_licencia,

                edad_conductor = model.edad_conductor,

                con_telefono = model.con_telefono,
                con_correo = model.con_correo,

                estado_conductor = model.estado_conductor,
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