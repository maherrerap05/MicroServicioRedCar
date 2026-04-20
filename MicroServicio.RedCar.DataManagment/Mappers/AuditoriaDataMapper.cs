using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class AuditoriaDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static AuditoriaDataModel ToDataModel(AuditoriaEntity entity)
        {
            return new AuditoriaDataModel
            {
                id_auditoria = entity.id_auditoria,
                auditoria_guid = entity.auditoria_guid,

                tabla_afectada = entity.tabla_afectada,
                operacion = entity.operacion,

                id_registro_afectado = entity.id_registro_afectado,

                datos_anteriores = entity.datos_anteriores,
                datos_nuevos = entity.datos_nuevos,

                usuario_ejecutor = entity.usuario_ejecutor,
                ip_origen = entity.ip_origen,

                fecha_evento_utc = entity.fecha_evento_utc,

                activo = entity.activo,

                row_version = entity.row_version
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static AuditoriaEntity ToEntity(AuditoriaDataModel model)
        {
            return new AuditoriaEntity
            {
                id_auditoria = model.id_auditoria,
                auditoria_guid = model.auditoria_guid,

                tabla_afectada = model.tabla_afectada,
                operacion = model.operacion,

                id_registro_afectado = model.id_registro_afectado,

                datos_anteriores = model.datos_anteriores,
                datos_nuevos = model.datos_nuevos,

                usuario_ejecutor = model.usuario_ejecutor,
                ip_origen = model.ip_origen,

                fecha_evento_utc = model.fecha_evento_utc,

                activo = model.activo,

                row_version = model.row_version
            };
        }
    }
}