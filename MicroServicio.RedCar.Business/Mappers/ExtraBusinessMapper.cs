using System;
using MicroServicio.RedCar.Business.DTOs.Extra;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class ExtraBusinessMapper
    {
        public static ExtraDataModel ToDataModel(CrearExtraRequest request)
        {
            return new ExtraDataModel
            {
                codigo_extra = request.codigo_extra,
                nombre_extra = request.nombre_extra,
                descripcion_extra = request.descripcion_extra,

                valor_fijo = request.valor_fijo,

                estado_extra = request.estado_extra,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro
            };
        }

        public static ExtraDataModel ToDataModel(ActualizarExtraRequest request)
        {
            return new ExtraDataModel
            {
                id_extra = request.id_extra,

                codigo_extra = request.codigo_extra,
                nombre_extra = request.nombre_extra,
                descripcion_extra = request.descripcion_extra,

                valor_fijo = request.valor_fijo,

                estado_extra = request.estado_extra,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado_extra == "INA" ? DateTime.UtcNow : null
            };
        }

        public static ExtraResponse ToResponse(ExtraDataModel model)
        {
            return new ExtraResponse
            {
                id_extra = model.id_extra,
                extra_guid = model.extra_guid,

                codigo_extra = model.codigo_extra,
                nombre_extra = model.nombre_extra,
                descripcion_extra = model.descripcion_extra,

                valor_fijo = model.valor_fijo,

                estado_extra = model.estado_extra,
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