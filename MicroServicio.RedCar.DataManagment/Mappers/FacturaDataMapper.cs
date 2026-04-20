using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class FacturaDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static FacturaDataModel ToDataModel(FacturaEntity entity)
        {
            return new FacturaDataModel
            {
                id_factura = entity.id_factura,

                guid_factura = entity.guid_factura,
                numero_factura = entity.numero_factura,

                id_cliente = entity.id_cliente,
                id_reserva = entity.id_reserva,

                fecha_emision = entity.fecha_emision,

                subtotal = entity.subtotal,
                valor_iva = entity.valor_iva,
                total = entity.total,

                observaciones_factura = entity.observaciones_factura,
                origen_canal_factura = entity.origen_canal_factura,

                estado = entity.estado,
                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                es_eliminado = entity.es_eliminado,
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
        public static FacturaEntity ToEntity(FacturaDataModel model)
        {
            return new FacturaEntity
            {
                id_factura = model.id_factura,

                guid_factura = model.guid_factura,
                numero_factura = model.numero_factura,

                id_cliente = model.id_cliente,
                id_reserva = model.id_reserva,

                fecha_emision = model.fecha_emision,

                subtotal = model.subtotal,
                valor_iva = model.valor_iva,
                total = model.total,

                observaciones_factura = model.observaciones_factura,
                origen_canal_factura = model.origen_canal_factura,

                estado = model.estado,
                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                es_eliminado = model.es_eliminado,
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