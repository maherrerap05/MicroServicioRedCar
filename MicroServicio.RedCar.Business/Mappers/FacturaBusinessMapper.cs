using System;
using MicroServicio.RedCar.Business.DTOs.Factura;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class FacturaBusinessMapper
    {
        public static FacturaDataModel ToDataModel(CrearFacturaRequest request)
        {
            return new FacturaDataModel
            {
                numero_factura = request.numero_factura,

                id_cliente = request.id_cliente,
                id_reserva = request.id_reserva,

                fecha_emision = DateTime.UtcNow,

                observaciones_factura = request.observaciones_factura,
                origen_canal_factura = request.origen_canal_factura,

                estado = request.estado,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static FacturaDataModel ToDataModel(ActualizarFacturaRequest request)
        {
            return new FacturaDataModel
            {
                id_factura = request.id_factura,
                numero_factura = request.numero_factura,

                id_cliente = request.id_cliente,
                id_reserva = request.id_reserva,

                observaciones_factura = request.observaciones_factura,
                origen_canal_factura = request.origen_canal_factura,

                estado = request.estado,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado == "INA" ? DateTime.UtcNow : null
            };
        }

        public static FacturaDataModel ToDataModel(AprobarFacturaRequest request)
        {
            return new FacturaDataModel
            {
                id_factura = request.id_factura,
                estado = "APR",

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static FacturaDataModel ToDataModel(AnularFacturaRequest request)
        {
            return new FacturaDataModel
            {
                id_factura = request.id_factura,
                estado = "INA",

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = DateTime.UtcNow,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static FacturaResponse ToResponse(FacturaDataModel model)
        {
            return new FacturaResponse
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

                servicio_origen = model.servicio_origen
            };
        }
    }
}