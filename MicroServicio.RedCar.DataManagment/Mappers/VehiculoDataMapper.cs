using MicroServicio.RedCar.DataAccess.Entities;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Mappers
{
    public static class VehiculoDataMapper
    {
        // =========================
        // ENTITY → DATA MODEL
        // =========================
        public static VehiculoDataModel ToDataModel(VehiculoEntity entity)
        {
            return new VehiculoDataModel
            {
                id_vehiculo = entity.id_vehiculo,
                vehiculo_guid = entity.vehiculo_guid,

                codigo_interno_vehiculo = entity.codigo_interno_vehiculo,
                placa_vehiculo = entity.placa_vehiculo,

                modelo_vehiculo = entity.modelo_vehiculo,
                anio_fabricacion = entity.anio_fabricacion,

                color_vehiculo = entity.color_vehiculo,
                tipo_combustible = entity.tipo_combustible,
                tipo_transmision = entity.tipo_transmision,

                capacidad_pasajeros = entity.capacidad_pasajeros,
                capacidad_maletas = entity.capacidad_maletas,
                numero_puertas = entity.numero_puertas,

                aire_acondicionado = entity.aire_acondicionado,

                localizacion_actual = entity.localizacion_actual,
                precio_base_dia = entity.precio_base_dia,
                kilometraje_actual = entity.kilometraje_actual,

                observaciones_generales = entity.observaciones_generales,
                imagen_referencial_url = entity.imagen_referencial_url,

                id_marca_vehiculo = entity.id_marca_vehiculo,
                id_categoria_vehiculo = entity.id_categoria_vehiculo,

                estado_vehiculo = entity.estado_vehiculo,
                es_eliminado = entity.es_eliminado,

                origen_registro = entity.origen_registro,

                fecha_registro_utc = entity.fecha_registro_utc,
                creado_por_usuario = entity.creado_por_usuario,

                modificado_por_usuario = entity.modificado_por_usuario,
                fecha_modificacion_utc = entity.fecha_modificacion_utc,
                modificado_desde_ip = entity.modificado_desde_ip,

                fecha_inhabilitacion_utc = entity.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = entity.motivo_inhabilitacion,

                
            };
        }

        // =========================
        // DATA MODEL → ENTITY
        // =========================
        public static VehiculoEntity ToEntity(VehiculoDataModel model)
        {
            return new VehiculoEntity
            {
                id_vehiculo = model.id_vehiculo,
                vehiculo_guid = model.vehiculo_guid == Guid.Empty ? Guid.NewGuid() : model.vehiculo_guid,

                codigo_interno_vehiculo = model.codigo_interno_vehiculo,
                placa_vehiculo = model.placa_vehiculo,

                modelo_vehiculo = model.modelo_vehiculo,
                anio_fabricacion = model.anio_fabricacion,

                color_vehiculo = model.color_vehiculo,
                tipo_combustible = model.tipo_combustible,
                tipo_transmision = model.tipo_transmision,

                capacidad_pasajeros = model.capacidad_pasajeros,
                capacidad_maletas = model.capacidad_maletas,
                numero_puertas = model.numero_puertas,

                aire_acondicionado = model.aire_acondicionado,

                localizacion_actual = model.localizacion_actual,
                precio_base_dia = model.precio_base_dia,
                kilometraje_actual = model.kilometraje_actual,

                observaciones_generales = model.observaciones_generales,
                imagen_referencial_url = model.imagen_referencial_url,

                id_marca_vehiculo = model.id_marca_vehiculo,
                id_categoria_vehiculo = model.id_categoria_vehiculo,

                estado_vehiculo = model.estado_vehiculo,
                es_eliminado = model.es_eliminado,

                origen_registro = model.origen_registro,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                motivo_inhabilitacion = model.motivo_inhabilitacion,

                
            };
        }
    }
}