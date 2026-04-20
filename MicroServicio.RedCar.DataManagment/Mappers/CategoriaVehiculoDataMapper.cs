using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class CategoriaVehiculoDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static CategoriaVehiculoDataModel ToDataModel(CategoriaVehiculoEntity entity)
        {
            return new CategoriaVehiculoDataModel
            {
                id_categoria_vehiculo = entity.id_categoria_vehiculo,
                categoria_vehiculo_guid = entity.categoria_vehiculo_guid,

                codigo_categoria_vehiculo = entity.codigo_categoria_vehiculo,
                nombre_categoria_vehiculo = entity.nombre_categoria_vehiculo,
                descripcion_categoria_vehiculo = entity.descripcion_categoria_vehiculo,

                estado_categoria_vehiculo = entity.estado_categoria_vehiculo,
                es_eliminado = entity.es_eliminado,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificado_desde_ip = entity.modificado_desde_ip,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,

                row_version = entity.row_version,
                origen_registro = entity.origen_registro
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static CategoriaVehiculoEntity ToEntity(CategoriaVehiculoDataModel model)
        {
            return new CategoriaVehiculoEntity
            {
                id_categoria_vehiculo = model.id_categoria_vehiculo,
                categoria_vehiculo_guid = model.categoria_vehiculo_guid,

                codigo_categoria_vehiculo = model.codigo_categoria_vehiculo,
                nombre_categoria_vehiculo = model.nombre_categoria_vehiculo,
                descripcion_categoria_vehiculo = model.descripcion_categoria_vehiculo,

                estado_categoria_vehiculo = model.estado_categoria_vehiculo,
                es_eliminado = model.es_eliminado,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,

                row_version = model.row_version,
                origen_registro = model.origen_registro
            };
        }
    }
}