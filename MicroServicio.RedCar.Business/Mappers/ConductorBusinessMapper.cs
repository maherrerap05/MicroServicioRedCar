using System;
using MicroServicio.RedCar.Business.DTOs.Conductor;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Mappers
{
    public static class ConductorBusinessMapper
    {
        public static ConductorDataModel ToDataModel(CrearConductorRequest request)
        {
            return new ConductorDataModel
            {
                codigo_conductor = request.codigo_conductor,
                tipo_identificacion = request.tipo_identificacion,
                numero_identificacion = request.numero_identificacion,
                con_nombre1 = request.con_nombre1,
                con_nombre2 = request.con_nombre2,
                con_apellido1 = request.con_apellido1,
                con_apellido2 = request.con_apellido2,
                numero_licencia = request.numero_licencia,
                fecha_vencimiento_licencia = request.fecha_vencimiento_licencia,
                edad_conductor = request.edad_conductor,
                con_telefono = request.con_telefono,
                con_correo = request.con_correo,
                estado_conductor = request.estado_conductor,
                fecha_registro_utc = DateTime.UtcNow,
                creado_por_usuario = request.creado_por_usuario,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro
            };
        }

        public static ConductorDataModel ToDataModel(ActualizarConductorRequest request)
        {
            return new ConductorDataModel
            {
                id_conductor = request.id_conductor!.Value,
                codigo_conductor = request.codigo_conductor!,
                tipo_identificacion = request.tipo_identificacion!,
                numero_identificacion = request.numero_identificacion!,
                con_nombre1 = request.con_nombre1!,
                con_nombre2 = request.con_nombre2,
                con_apellido1 = request.con_apellido1!,
                con_apellido2 = request.con_apellido2,
                numero_licencia = request.numero_licencia!,
                fecha_vencimiento_licencia = request.fecha_vencimiento_licencia!.Value,
                edad_conductor = request.edad_conductor!.Value,
                con_telefono = request.con_telefono!,
                con_correo = request.con_correo!,
                estado_conductor = request.estado_conductor!,
                modificado_por_usuario = request.modificado_por_usuario,
                fecha_modificacion_utc = DateTime.UtcNow,
                modificado_desde_ip = request.modificado_desde_ip,
                origen_registro = request.origen_registro!,
                motivo_inhabilitacion = request.motivo_inhabilitacion,
                fecha_inhabilitacion_utc = request.estado_conductor == "INA" ? DateTime.UtcNow : null
            };
        }

        public static ConductorFiltroDataModel ToFiltroDataModel(ConductorFiltroRequest request)
        {
            return new ConductorFiltroDataModel
            {
                codigo_conductor = request.codigo_conductor,
                tipo_identificacion = request.tipo_identificacion,
                numero_identificacion = request.numero_identificacion,
                con_nombre1 = request.con_nombre1,
                con_nombre2 = request.con_nombre2,
                con_apellido1 = request.con_apellido1,
                con_apellido2 = request.con_apellido2,
                numero_licencia = request.numero_licencia,
                fecha_vencimiento_licencia_desde = request.fecha_vencimiento_licencia_desde,
                fecha_vencimiento_licencia_hasta = request.fecha_vencimiento_licencia_hasta,
                edad_conductor = request.edad_conductor,
                con_telefono = request.con_telefono,
                con_correo = request.con_correo,
                estado_conductor = request.estado_conductor,
                origen_registro = request.origen_registro,
                PageNumber = request.page_number,
                PageSize = request.page_size
            };
        }

        public static ConductorResponse ToResponse(ConductorDataModel model)
        {
            return new ConductorResponse
            {
                id_conductor = model.id_conductor,
                conductor_guid = model.conductor_guid,
                codigo_conductor = model.codigo_conductor,
                tipo_identificacion = model.tipo_identificacion,
                numero_identificacion = model.numero_identificacion,
                con_nombre1 = model.con_nombre1,
                con_nombre2 = model.con_nombre2,
                con_apellido1 = model.con_apellido1,
                con_apellido2 = model.con_apellido2,
                numero_licencia = model.numero_licencia,
                fecha_vencimiento_licencia = model.fecha_vencimiento_licencia,
                edad_conductor = model.edad_conductor,
                con_telefono = model.con_telefono,
                con_correo = model.con_correo,
                estado_conductor = model.estado_conductor,
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