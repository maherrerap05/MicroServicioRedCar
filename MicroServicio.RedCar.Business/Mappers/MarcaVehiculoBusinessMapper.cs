using System;
using MicroServicio.RedCar.Business.DTOs.MarcaVehiculo;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class MarcaVehiculoBusinessMapper
    {
        public static MarcaVehiculoDataModel ToDataModel(CrearMarcaVehiculoRequest request)
        {
            return new MarcaVehiculoDataModel
            {
                codigo_marca_vehiculo = request.codigo_marca_vehiculo,
                nombre_marca_vehiculo = request.nombre_marca_vehiculo,
                descripcion_marca_vehiculo = request.descripcion_marca_vehiculo,

                estado_marca_vehiculo = request.estado_marca_vehiculo,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro
            };
        }

        public static MarcaVehiculoDataModel ToDataModel(ActualizarMarcaVehiculoRequest request)
        {
            return new MarcaVehiculoDataModel
            {
                id_marca_vehiculo = request.id_marca_vehiculo,

                codigo_marca_vehiculo = request.codigo_marca_vehiculo,
                nombre_marca_vehiculo = request.nombre_marca_vehiculo,
                descripcion_marca_vehiculo = request.descripcion_marca_vehiculo,

                estado_marca_vehiculo = request.estado_marca_vehiculo,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado_marca_vehiculo == "INA" ? DateTime.UtcNow : null
            };
        }

        public static MarcaVehiculoResponse ToResponse(MarcaVehiculoDataModel model)
        {
            return new MarcaVehiculoResponse
            {
                id_marca_vehiculo = model.id_marca_vehiculo,
                marca_vehiculo_guid = model.marca_vehiculo_guid,

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