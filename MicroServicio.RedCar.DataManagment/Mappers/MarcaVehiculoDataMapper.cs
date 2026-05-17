using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class MarcaVehiculoDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static MarcaVehiculoDataModel ToDataModel(MarcaVehiculoEntity entity)
        {
            return new MarcaVehiculoDataModel
            {
                id_marca_vehiculo = entity.id_marca_vehiculo,
                marca_vehiculo_guid = entity.marca_vehiculo_guid,

                codigo_marca_vehiculo = entity.codigo_marca_vehiculo,
                nombre_marca_vehiculo = entity.nombre_marca_vehiculo,
                descripcion_marca_vehiculo = entity.descripcion_marca_vehiculo,

                estado_marca_vehiculo = entity.estado_marca_vehiculo,
                es_eliminado = entity.es_eliminado,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificado_desde_ip = entity.modificado_desde_ip,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,
                origen_registro = entity.origen_registro
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static MarcaVehiculoEntity ToEntity(MarcaVehiculoDataModel model)
        {
            return new MarcaVehiculoEntity
            {
                id_marca_vehiculo = model.id_marca_vehiculo,
                marca_vehiculo_guid = model.marca_vehiculo_guid == Guid.Empty ? Guid.NewGuid() : model.marca_vehiculo_guid,

                codigo_marca_vehiculo = model.codigo_marca_vehiculo,
                nombre_marca_vehiculo = model.nombre_marca_vehiculo,
                descripcion_marca_vehiculo = model.descripcion_marca_vehiculo,

                estado_marca_vehiculo = model.estado_marca_vehiculo,
                es_eliminado = model.es_eliminado,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,
                origen_registro = model.origen_registro
            };
        }
    }
}