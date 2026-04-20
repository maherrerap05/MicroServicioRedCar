using System;
using MicroServicio.RedCar.Business.DTOs.Cliente;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class ClienteBusinessMapper
    {
        public static ClienteDataModel ToDataModel(CrearClienteRequest request)
        {
            return new ClienteDataModel
            {
                tipo_identificacion = request.tipo_identificacion,
                numero_identificacion = request.numero_identificacion,
                razon_social = request.razon_social,

                nombres = request.nombres,
                apellidos = request.apellidos,

                correo = request.correo,
                telefono = request.telefono,
                direccion = request.direccion,

                estado = request.estado,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen
            };
        }

        public static ClienteDataModel ToDataModel(ActualizarClienteRequest request)
        {
            return new ClienteDataModel
            {
                id_cliente = request.id_cliente,

                tipo_identificacion = request.tipo_identificacion,
                numero_identificacion = request.numero_identificacion,
                razon_social = request.razon_social,

                nombres = request.nombres,
                apellidos = request.apellidos,

                correo = request.correo,
                telefono = request.telefono,
                direccion = request.direccion,

                estado = request.estado,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificacion_ip = request.modificacion_ip,
                servicio_origen = request.servicio_origen,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado == "INA" ? DateTime.UtcNow : null
            };
        }

        public static ClienteResponse ToResponse(ClienteDataModel model)
        {
            return new ClienteResponse
            {
                id_cliente = model.id_cliente,
                cliente_guid = model.cliente_guid,

                tipo_identificacion = model.tipo_identificacion,
                numero_identificacion = model.numero_identificacion,
                razon_social = model.razon_social,

                nombres = model.nombres,
                apellidos = model.apellidos,

                correo = model.correo,
                telefono = model.telefono,
                direccion = model.direccion,

                estado = model.estado,
                es_eliminado = model.es_eliminado,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                modificacion_ip = model.modificacion_ip,

                servicio_origen = model.servicio_origen,
                motivo_inhabilitacion = model.motivo_inhabilitacion
            };
        }
    }
}