using System;
using MicroServicio.RedCar.Business.DTOs.Vehiculo;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class VehiculoBusinessMapper
    {
        public static VehiculoDataModel ToDataModel(CrearVehiculoRequest request)
        {
            return new VehiculoDataModel
            {
                codigo_interno_vehiculo = request.codigo_interno_vehiculo,
                placa_vehiculo = request.placa_vehiculo,
                modelo_vehiculo = request.modelo_vehiculo,
                anio_fabricacion = request.anio_fabricacion,

                color_vehiculo = request.color_vehiculo,
                tipo_combustible = request.tipo_combustible,
                tipo_transmision = request.tipo_transmision,

                capacidad_pasajeros = request.capacidad_pasajeros,
                capacidad_maletas = request.capacidad_maletas,
                numero_puertas = request.numero_puertas,

                localizacion_actual = request.localizacion_actual,

                precio_base_dia = request.precio_base_dia,
                kilometraje_actual = request.kilometraje_actual,

                observaciones_generales = request.observaciones_generales,
                imagen_referencial_url = request.imagen_referencial_url,

                estado_vehiculo = request.estado_vehiculo,

                id_marca_vehiculo = request.id_marca_vehiculo,
                id_categoria_vehiculo = request.id_categoria_vehiculo,

                aire_acondicionado = request.aire_acondicionado,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro
            };
        }

        public static VehiculoDataModel ToDataModel(ActualizarVehiculoRequest request)
        {
            return new VehiculoDataModel
            {
                id_vehiculo = request.id_vehiculo,

                codigo_interno_vehiculo = request.codigo_interno_vehiculo,
                placa_vehiculo = request.placa_vehiculo,
                modelo_vehiculo = request.modelo_vehiculo,
                anio_fabricacion = request.anio_fabricacion,

                color_vehiculo = request.color_vehiculo,
                tipo_combustible = request.tipo_combustible,
                tipo_transmision = request.tipo_transmision,

                capacidad_pasajeros = request.capacidad_pasajeros,
                capacidad_maletas = request.capacidad_maletas,
                numero_puertas = request.numero_puertas,

                localizacion_actual = request.localizacion_actual,

                precio_base_dia = request.precio_base_dia,
                kilometraje_actual = request.kilometraje_actual,

                observaciones_generales = request.observaciones_generales,
                imagen_referencial_url = request.imagen_referencial_url,

                estado_vehiculo = request.estado_vehiculo,

                id_marca_vehiculo = request.id_marca_vehiculo,
                id_categoria_vehiculo = request.id_categoria_vehiculo,

                aire_acondicionado = request.aire_acondicionado,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado_vehiculo == "INA" ? DateTime.UtcNow : null
            };
        }

        public static VehiculoResponse ToResponse(VehiculoDataModel model)
        {
            return new VehiculoResponse
            {
                id_vehiculo = model.id_vehiculo,
                vehiculo_guid = model.vehiculo_guid,

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

                localizacion_actual = model.localizacion_actual,

                precio_base_dia = model.precio_base_dia,
                kilometraje_actual = model.kilometraje_actual,

                observaciones_generales = model.observaciones_generales,
                imagen_referencial_url = model.imagen_referencial_url,

                estado_vehiculo = model.estado_vehiculo,
                es_eliminado = model.es_eliminado,

                id_marca_vehiculo = model.id_marca_vehiculo,
                id_categoria_vehiculo = model.id_categoria_vehiculo,

                aire_acondicionado = model.aire_acondicionado,

                fecha_registro_utc = model.fecha_registro_utc,
                creado_por_usuario = model.creado_por_usuario,

                modificado_por_usuario = model.modificado_por_usuario,
                fecha_modificacion_utc = model.fecha_modificacion_utc,

                fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc,
                modificado_desde_ip = model.modificado_desde_ip,

                origen_registro = model.origen_registro,
                motivo_inhabilitacion = model.motivo_inhabilitacion
            };
        }
    }
}