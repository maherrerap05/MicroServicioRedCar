using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class ReservaExtraDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static ReservaExtraDataModel ToDataModel(ReservaExtraEntity entity)
        {
            return new ReservaExtraDataModel
            {
                id_reserva_extra = entity.id_reserva_extra,

                reserva_extra_guid = entity.reserva_extra_guid,

                id_reserva = entity.id_reserva,
                id_extra = entity.id_extra,

                cantidad = entity.cantidad,

                valor_unitario_extra = entity.valor_unitario_extra,
                subtotal_extra = entity.subtotal_extra,

                estado_reserva_extra = entity.estado_reserva_extra,
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
        public static ReservaExtraEntity ToEntity(ReservaExtraDataModel model)
        {
            return new ReservaExtraEntity
            {
                id_reserva_extra = model.id_reserva_extra,

                reserva_extra_guid = model.reserva_extra_guid == Guid.Empty ? Guid.NewGuid() : model.reserva_extra_guid,

                id_reserva = model.id_reserva,
                id_extra = model.id_extra,

                cantidad = model.cantidad,

                valor_unitario_extra = model.valor_unitario_extra,
                subtotal_extra = model.subtotal_extra,

                estado_reserva_extra = model.estado_reserva_extra,
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