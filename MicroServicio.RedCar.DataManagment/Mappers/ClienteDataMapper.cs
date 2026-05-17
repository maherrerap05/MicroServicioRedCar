using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class ClienteDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static ClienteDataModel ToDataModel(ClienteEntity entity)
        {
            return new ClienteDataModel
            {
                id_cliente = entity.id_cliente,
                cliente_guid = entity.cliente_guid,

                tipo_identificacion = entity.tipo_identificacion,
                numero_identificacion = entity.numero_identificacion,
                razon_social = entity.razon_social,

                nombres = entity.nombres,
                apellidos = entity.apellidos,

                correo = entity.correo,
                telefono = entity.telefono,
                direccion = entity.direccion,

                estado = entity.estado,
                es_eliminado = entity.es_eliminado,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificacion_ip = entity.modificacion_ip,

                servicio_origen = entity.servicio_origen,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,

                
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static ClienteEntity ToEntity(ClienteDataModel model)
        {
            return new ClienteEntity
            {
                id_cliente = model.id_cliente,
                cliente_guid = model.cliente_guid == Guid.Empty ? Guid.NewGuid() : model.cliente_guid,

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
                modificacion_ip = model.modificacion_ip,

                servicio_origen = model.servicio_origen,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,

                
            };
        }
    }
}