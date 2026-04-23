using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicio.RedCar.Business.DTOs.Reserva;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class ReservaBusinessMapper
    {
        public static ReservaDataModel ToDataModel(CrearReservaRequest request)
        {
            var fechaHoraRecogida = request.fecha_recogida.Date + request.hora_recogida;
            var fechaHoraDevolucion = request.fecha_devolucion.Date + request.hora_devolucion;

            return new ReservaDataModel
            {
                codigo_reserva = request.codigo_reserva,

                id_cliente = request.id_cliente,
                id_vehiculo = request.id_vehiculo,
                id_localizacion_recogida = request.id_localizacion_recogida,
                id_localizacion_devolucion = request.id_localizacion_devolucion,

                fecha_reserva_utc = DateTime.UtcNow,

                fecha_recogida = request.fecha_recogida,
                hora_recogida = request.hora_recogida,

                fecha_devolucion = request.fecha_devolucion,
                hora_devolucion = request.hora_devolucion,

                fecha_hora_recogida = fechaHoraRecogida,
                fecha_hora_devolucion = fechaHoraDevolucion,

                fecha_inicio = fechaHoraRecogida,
                fecha_fin = fechaHoraDevolucion,

                cantidad_dias_reserva = (int)Math.Ceiling((fechaHoraDevolucion - fechaHoraRecogida).TotalDays),

                observaciones_reserva = request.observaciones_reserva,
                origen_canal_reserva = request.origen_canal_reserva,
                estado_reserva = request.estado_reserva,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static ReservaDataModel ToDataModel(ActualizarReservaRequest request)
        {
            var fechaHoraRecogida = request.fecha_recogida.Date + request.hora_recogida;
            var fechaHoraDevolucion = request.fecha_devolucion.Date + request.hora_devolucion;

            return new ReservaDataModel
            {
                id_reserva = request.id_reserva,
                codigo_reserva = request.codigo_reserva,

                id_cliente = request.id_cliente,
                id_vehiculo = request.id_vehiculo,
                id_localizacion_recogida = request.id_localizacion_recogida,
                id_localizacion_devolucion = request.id_localizacion_devolucion,

                fecha_recogida = request.fecha_recogida,
                hora_recogida = request.hora_recogida,

                fecha_devolucion = request.fecha_devolucion,
                hora_devolucion = request.hora_devolucion,

                fecha_hora_recogida = fechaHoraRecogida,
                fecha_hora_devolucion = fechaHoraDevolucion,

                fecha_inicio = fechaHoraRecogida,
                fecha_fin = fechaHoraDevolucion,

                cantidad_dias_reserva = (int)Math.Ceiling((fechaHoraDevolucion - fechaHoraRecogida).TotalDays),

                observaciones_reserva = request.observaciones_reserva,
                origen_canal_reserva = request.origen_canal_reserva,
                estado_reserva = request.estado_reserva,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen,

                motivo_cancelacion = request.motivo_cancelacion,
                fecha_cancelacion_utc = request.estado_reserva == "CAN" ? DateTime.UtcNow : null,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.motivo_inhabilitacion != null ? DateTime.UtcNow : null
            };
        }

        public static ReservaDataModel ToDataModel(ConfirmarReservaRequest request)
        {
            return new ReservaDataModel
            {
                id_reserva = request.id_reserva,
                estado_reserva = "CON",
                fecha_confirmacion_utc = DateTime.UtcNow,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static ReservaDataModel ToDataModel(CancelarReservaRequest request)
        {
            return new ReservaDataModel
            {
                id_reserva = request.id_reserva,
                estado_reserva = "CAN",
                motivo_cancelacion = request.motivo_cancelacion,
                fecha_cancelacion_utc = DateTime.UtcNow,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static ReservaConductorDataModel ToDataModel(ReservaConductorRequest request, int idReserva)
        {
            // Solo se asigna fecha_modificacion_utc si viene modificado_por_usuario,
            // para respetar el constraint CHK_RES_X_CON_MODIFICACION_USUARIO_COHERENTE
            var tieneModificacion = !string.IsNullOrWhiteSpace(request.modificado_por_usuario);

            return new ReservaConductorDataModel
            {
                id_reserva = idReserva,
                id_conductor = request.id_conductor,

                tipo_conductor = request.tipo_conductor,
                es_principal = request.es_principal,
                fecha_asignacion_utc = DateTime.UtcNow,

                estado_reserva_conductor = request.estado_reserva_conductor,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario!,

                modificado_por_usuario = tieneModificacion ? request.modificado_por_usuario : null,  // ← AGREGAR
                fecha_modificacion_utc = tieneModificacion ? DateTime.UtcNow : null,                 // ← AGREGAR

                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro!
            };
        }

        public static ReservaExtraDataModel ToDataModel(
            ReservaExtraRequest request,
            int idReserva,
            decimal valorUnitario)
        {
            var subtotal = valorUnitario * request.cantidad;
            var tieneModificacion = !string.IsNullOrWhiteSpace(request.modificado_por_usuario);

            return new ReservaExtraDataModel
            {
                id_reserva = idReserva,
                id_extra = request.id_extra,
                cantidad = request.cantidad,
                valor_unitario_extra = valorUnitario,
                subtotal_extra = subtotal,
                estado_reserva_extra = request.estado_reserva_extra,
                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario!,

                modificado_por_usuario = tieneModificacion ? request.modificado_por_usuario : null,  // ← AGREGAR
                fecha_modificacion_utc = tieneModificacion ? DateTime.UtcNow : null,                 // ← AGREGAR

                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro!
            };
        }

        public static ReservaResponse ToResponse(
            ReservaDataModel model,
            IEnumerable<ReservaConductorDataModel>? conductores = null,
            IEnumerable<ReservaExtraDataModel>? extras = null)
        {
            return new ReservaResponse
            {
                id_reserva = model.id_reserva,
                guid_reserva = model.guid_reserva,
                codigo_reserva = model.codigo_reserva,

                id_cliente = model.id_cliente,
                id_vehiculo = model.id_vehiculo,
                id_localizacion_recogida = model.id_localizacion_recogida,
                id_localizacion_devolucion = model.id_localizacion_devolucion,

                fecha_reserva_utc = model.fecha_reserva_utc,

                fecha_recogida = model.fecha_recogida,
                hora_recogida = model.hora_recogida,

                fecha_devolucion = model.fecha_devolucion,
                hora_devolucion = model.hora_devolucion,

                fecha_hora_recogida = model.fecha_hora_recogida,
                fecha_hora_devolucion = model.fecha_hora_devolucion,

                fecha_inicio = model.fecha_inicio,
                fecha_fin = model.fecha_fin,

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

                conductores = conductores != null
                    ? conductores.Select(ToResponse).ToList()
                    : new List<ReservaConductorResponse>(),

                extras = extras != null
                    ? extras.Select(ToResponse).ToList()
                    : new List<ReservaExtraResponse>()
            };
        }

        public static ReservaConductorResponse ToResponse(ReservaConductorDataModel model)
        {
            return new ReservaConductorResponse
            {
                id_reserva_conductor = model.id_reserva_conductor,
                reserva_conductor_guid = model.reserva_conductor_guid,

                id_conductor = model.id_conductor,

                tipo_conductor = model.tipo_conductor,
                es_principal = model.es_principal,

                fecha_asignacion_utc = model.fecha_asignacion_utc,

                estado_reserva_conductor = model.estado_reserva_conductor
            };
        }

        public static ReservaExtraResponse ToResponse(ReservaExtraDataModel model)
        {
            return new ReservaExtraResponse
            {
                id_reserva_extra = model.id_reserva_extra,
                reserva_extra_guid = model.reserva_extra_guid,

                id_extra = model.id_extra,

                cantidad = model.cantidad,
                valor_unitario_extra = model.valor_unitario_extra,
                subtotal_extra = model.subtotal_extra,

                estado_reserva_extra = model.estado_reserva_extra
            };
        }
    }
}