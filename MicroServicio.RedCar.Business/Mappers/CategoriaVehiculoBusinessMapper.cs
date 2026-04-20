using System;
using MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class CategoriaVehiculoBusinessMapper
    {
        public static CategoriaVehiculoDataModel ToDataModel(CrearCategoriaVehiculoRequest request)
        {
            return new CategoriaVehiculoDataModel
            {
                codigo_categoria_vehiculo = request.codigo_categoria_vehiculo,
                nombre_categoria_vehiculo = request.nombre_categoria_vehiculo,
                descripcion_categoria_vehiculo = request.descripcion_categoria_vehiculo,

                estado_categoria_vehiculo = request.estado_categoria_vehiculo,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro
            };
        }

        public static CategoriaVehiculoDataModel ToDataModel(ActualizarCategoriaVehiculoRequest request)
        {
            return new CategoriaVehiculoDataModel
            {
                id_categoria_vehiculo = request.id_categoria_vehiculo,

                codigo_categoria_vehiculo = request.codigo_categoria_vehiculo,
                nombre_categoria_vehiculo = request.nombre_categoria_vehiculo,
                descripcion_categoria_vehiculo = request.descripcion_categoria_vehiculo,

                estado_categoria_vehiculo = request.estado_categoria_vehiculo,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado_categoria_vehiculo == "INA" ? DateTime.UtcNow : null
            };
        }

        public static CategoriaVehiculoResponse ToResponse(CategoriaVehiculoDataModel model)
        {
            return new CategoriaVehiculoResponse
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

                origen_registro = model.origen_registro
            };
        }
    }
}