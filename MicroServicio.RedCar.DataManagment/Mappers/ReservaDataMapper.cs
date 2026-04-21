using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class ReservaDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static ReservaDataModel ToDataModel(ReservaEntity entity)
        {
            return new ReservaDataModel
            {
                id_reserva = entity.id_reserva,

                guid_reserva = entity.guid_reserva,
                codigo_reserva = entity.codigo_reserva,

                id_cliente = entity.id_cliente,
                id_vehiculo = entity.id_vehiculo,
                id_localizacion_recogida = entity.id_localizacion_recogida,
                id_localizacion_devolucion = entity.id_localizacion_devolucion,

                fecha_reserva_utc = entity.fecha_reserva_utc,
                fecha_inicio = entity.fecha_inicio,
                fecha_fin = entity.fecha_fin,

                fecha_recogida = entity.fecha_recogida,
                hora_recogida = entity.hora_recogida,

                fecha_devolucion = entity.fecha_devolucion,
                hora_devolucion = entity.hora_devolucion,

                fecha_hora_recogida = entity.fecha_hora_recogida,
                fecha_hora_devolucion = entity.fecha_hora_devolucion,

                cantidad_dias_reserva = entity.cantidad_dias_reserva,

                subtotal_reserva = entity.subtotal_reserva,
                valor_iva = entity.valor_iva,
                total_reserva = entity.total_reserva,

                observaciones_reserva = entity.observaciones_reserva,
                origen_canal_reserva = entity.origen_canal_reserva,

                estado_reserva = entity.estado_reserva,

                fecha_confirmacion_utc = entity.fecha_confirmacion_utc,
                fecha_cancelacion_utc = entity.fecha_cancelacion_utc,
                motivo_cancelacion = entity.motivo_cancelacion,

                es_eliminado = entity.es_eliminado,
                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificacion_ip = entity.modificacion_ip,

                servicio_origen = entity.servicio_origen,
                row_version = entity.row_version
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static ReservaEntity ToEntity(ReservaDataModel model)
        {
            return new ReservaEntity
            {
                id_reserva = model.id_reserva,

                guid_reserva = model.guid_reserva,
                codigo_reserva = model.codigo_reserva,

                id_cliente = model.id_cliente,
                id_vehiculo = model.id_vehiculo,
                id_localizacion_recogida = model.id_localizacion_recogida,
                id_localizacion_devolucion = model.id_localizacion_devolucion,

                fecha_reserva_utc = model.fecha_reserva_utc,
                fecha_inicio = model.fecha_inicio,
                fecha_fin = model.fecha_fin,

                fecha_recogida = model.fecha_recogida,
                hora_recogida = model.hora_recogida,

                fecha_devolucion = model.fecha_devolucion,
                hora_devolucion = model.hora_devolucion,

                fecha_hora_recogida = model.fecha_hora_recogida,
                fecha_hora_devolucion = model.fecha_hora_devolucion,

                cantidad_dias_reserva = model.cantidad_dias_reserva,

                subtotal_reserva = model.subtotal_reserva,
                valor_iva = model.valor_iva,
                total_reserva = model.total_reserva,

                observaciones_reserva = model.observaciones_reserva,
                origen_canal_reserva = model.origen_canal_reserva,

                estado_reserva = model.estado_reserva,

                fecha_confirmacion_utc = model.fecha_confirmacion_utc,
                fecha_cancelacion_utc = model.fecha_cancelacion_utc,
                motivo_cancelacion = model.motivo_cancelacion,

                es_eliminado = model.es_eliminado,
                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificacion_ip = model.modificacion_ip,

                servicio_origen = model.servicio_origen,
                row_version = model.row_version
            };
        }
    }
}