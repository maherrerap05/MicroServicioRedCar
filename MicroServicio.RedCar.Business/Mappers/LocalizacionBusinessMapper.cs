using System;
using MicroServicio.RedCar.Business.DTOs.Localizacion;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class LocalizacionBusinessMapper
    {
        public static LocalizacionDataModel ToDataModel(CrearLocalizacionRequest request)
        {
            return new LocalizacionDataModel
            {
                codigo_localizacion = request.codigo_localizacion,
                nombre_localizacion = request.nombre_localizacion,
                direccion_localizacion = request.direccion_localizacion,

                telefono_contacto = request.telefono_contacto,
                correo_contacto = request.correo_contacto,
                horario_atencion = request.horario_atencion,

                zona_horaria = request.zona_horaria,
                id_ciudad = request.id_ciudad,

                estado_localizacion = request.estado_localizacion,

                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro
            };
        }

        public static LocalizacionDataModel ToDataModel(ActualizarLocalizacionRequest request)
        {
            return new LocalizacionDataModel
            {
                id_localizacion = request.id_localizacion,

                codigo_localizacion = request.codigo_localizacion,
                nombre_localizacion = request.nombre_localizacion,
                direccion_localizacion = request.direccion_localizacion,

                telefono_contacto = request.telefono_contacto,
                correo_contacto = request.correo_contacto,
                horario_atencion = request.horario_atencion,

                zona_horaria = request.zona_horaria,
                id_ciudad = request.id_ciudad,

                estado_localizacion = request.estado_localizacion,

                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro,

                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado_localizacion == "INA" ? DateTime.UtcNow : null
            };
        }

        public static LocalizacionResponse ToResponse(LocalizacionDataModel model)
        {
            return new LocalizacionResponse
            {
                id_localizacion = model.id_localizacion,
                localizacion_guid = model.localizacion_guid,

                codigo_localizacion = model.codigo_localizacion,
                nombre_localizacion = model.nombre_localizacion,
                direccion_localizacion = model.direccion_localizacion,

                telefono_contacto = model.telefono_contacto,
                correo_contacto = model.correo_contacto,
                horario_atencion = model.horario_atencion,

                zona_horaria = model.zona_horaria,
                id_ciudad = model.id_ciudad,

                estado_localizacion = model.estado_localizacion,
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