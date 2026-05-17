using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class ReservaConductorDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static ReservaConductorDataModel ToDataModel(ReservaConductorEntity entity)
        {
            return new ReservaConductorDataModel
            {
                id_reserva_conductor = entity.id_reserva_conductor,

                reserva_conductor_guid = entity.reserva_conductor_guid,

                id_reserva = entity.id_reserva,
                id_conductor = entity.id_conductor,

                tipo_conductor = entity.tipo_conductor,
                es_principal = entity.es_principal,
                fecha_asignacion_utc = entity.fecha_asignacion_utc,

                estado_reserva_conductor = entity.estado_reserva_conductor,
                es_eliminado = entity.es_eliminado,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificado_desde_ip = entity.modificado_desde_ip,

                origen_registro = entity.origen_registro,
                
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static ReservaConductorEntity ToEntity(ReservaConductorDataModel model)
        {
            return new ReservaConductorEntity
            {
                id_reserva_conductor = model.id_reserva_conductor,

                reserva_conductor_guid = model.reserva_conductor_guid == Guid.Empty ? Guid.NewGuid() : model.reserva_conductor_guid,

                id_reserva = model.id_reserva,
                id_conductor = model.id_conductor,

                tipo_conductor = model.tipo_conductor,
                es_principal = model.es_principal,
                fecha_asignacion_utc = model.fecha_asignacion_utc,

                estado_reserva_conductor = model.estado_reserva_conductor,
                es_eliminado = model.es_eliminado,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                origen_registro = model.origen_registro,
                
            };
        }
    }
}