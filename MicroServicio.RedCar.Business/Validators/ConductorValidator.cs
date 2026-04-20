using System;
using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.Conductor;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class ConductorValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearConductorRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación del conductor no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.codigo_conductor))
                errors.Add("El código del conductor es obligatorio.");

            if (request.codigo_conductor != null && request.codigo_conductor.Trim().Length > 20)
                errors.Add("El código del conductor no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.tipo_identificacion))
                errors.Add("El tipo de identificación es obligatorio.");

            if (request.tipo_identificacion != null && request.tipo_identificacion.Trim().Length > 10)
                errors.Add("El tipo de identificación no puede exceder 10 caracteres.");

            if (string.IsNullOrWhiteSpace(request.numero_identificacion))
                errors.Add("El número de identificación es obligatorio.");

            if (request.numero_identificacion != null && request.numero_identificacion.Trim().Length > 20)
                errors.Add("El número de identificación no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.con_nombre1))
                errors.Add("El primer nombre es obligatorio.");

            if (request.con_nombre1 != null && request.con_nombre1.Trim().Length > 80)
                errors.Add("El primer nombre no puede exceder 80 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.con_nombre2) &&
                request.con_nombre2.Trim().Length > 80)
            {
                errors.Add("El segundo nombre no puede exceder 80 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.con_apellido1))
                errors.Add("El primer apellido es obligatorio.");

            if (request.con_apellido1 != null && request.con_apellido1.Trim().Length > 80)
                errors.Add("El primer apellido no puede exceder 80 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.con_apellido2) &&
                request.con_apellido2.Trim().Length > 80)
            {
                errors.Add("El segundo apellido no puede exceder 80 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.numero_licencia))
                errors.Add("El número de licencia es obligatorio.");

            if (request.numero_licencia != null && request.numero_licencia.Trim().Length > 30)
                errors.Add("El número de licencia no puede exceder 30 caracteres.");

            if (request.fecha_vencimiento_licencia == default)
                errors.Add("La fecha de vencimiento de la licencia es obligatoria.");

            if (request.edad_conductor < 18)
                errors.Add("La edad del conductor debe ser mayor o igual a 18 años.");

            if (string.IsNullOrWhiteSpace(request.con_telefono))
                errors.Add("El teléfono del conductor es obligatorio.");

            if (request.con_telefono != null && request.con_telefono.Trim().Length > 20)
                errors.Add("El teléfono del conductor no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.con_correo))
                errors.Add("El correo del conductor es obligatorio.");

            if (request.con_correo != null && request.con_correo.Trim().Length > 120)
                errors.Add("El correo del conductor no puede exceder 120 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.con_correo) && !EsCorreoValido(request.con_correo))
                errors.Add("El correo del conductor no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(request.estado_conductor))
                errors.Add("El estado del conductor es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_conductor) &&
                request.estado_conductor != "ACT" &&
                request.estado_conductor != "INA")
            {
                errors.Add("El estado del conductor debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador es obligatorio.");

            if (request.creado_por_usuario != null && request.creado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario creador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificado_desde_ip) &&
                request.modificado_desde_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_registro))
                errors.Add("El origen del registro es obligatorio.");

            if (request.origen_registro != null && request.origen_registro.Trim().Length > 20)
                errors.Add("El origen del registro no puede exceder 20 caracteres.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarConductorRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización del conductor no puede ser nula.");
                return errors;
            }

            if (request.id_conductor <= 0)
                errors.Add("El id del conductor es inválido.");

            if (string.IsNullOrWhiteSpace(request.codigo_conductor))
                errors.Add("El código del conductor es obligatorio.");

            if (request.codigo_conductor != null && request.codigo_conductor.Trim().Length > 20)
                errors.Add("El código del conductor no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.tipo_identificacion))
                errors.Add("El tipo de identificación es obligatorio.");

            if (request.tipo_identificacion != null && request.tipo_identificacion.Trim().Length > 10)
                errors.Add("El tipo de identificación no puede exceder 10 caracteres.");

            if (string.IsNullOrWhiteSpace(request.numero_identificacion))
                errors.Add("El número de identificación es obligatorio.");

            if (request.numero_identificacion != null && request.numero_identificacion.Trim().Length > 20)
                errors.Add("El número de identificación no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.con_nombre1))
                errors.Add("El primer nombre es obligatorio.");

            if (request.con_nombre1 != null && request.con_nombre1.Trim().Length > 80)
                errors.Add("El primer nombre no puede exceder 80 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.con_nombre2) &&
                request.con_nombre2.Trim().Length > 80)
            {
                errors.Add("El segundo nombre no puede exceder 80 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.con_apellido1))
                errors.Add("El primer apellido es obligatorio.");

            if (request.con_apellido1 != null && request.con_apellido1.Trim().Length > 80)
                errors.Add("El primer apellido no puede exceder 80 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.con_apellido2) &&
                request.con_apellido2.Trim().Length > 80)
            {
                errors.Add("El segundo apellido no puede exceder 80 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.numero_licencia))
                errors.Add("El número de licencia es obligatorio.");

            if (request.numero_licencia != null && request.numero_licencia.Trim().Length > 30)
                errors.Add("El número de licencia no puede exceder 30 caracteres.");

            if (request.fecha_vencimiento_licencia == default)
                errors.Add("La fecha de vencimiento de la licencia es obligatoria.");

            if (request.edad_conductor < 18)
                errors.Add("La edad del conductor debe ser mayor o igual a 18 años.");

            if (string.IsNullOrWhiteSpace(request.con_telefono))
                errors.Add("El teléfono del conductor es obligatorio.");

            if (request.con_telefono != null && request.con_telefono.Trim().Length > 20)
                errors.Add("El teléfono del conductor no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.con_correo))
                errors.Add("El correo del conductor es obligatorio.");

            if (request.con_correo != null && request.con_correo.Trim().Length > 120)
                errors.Add("El correo del conductor no puede exceder 120 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.con_correo) && !EsCorreoValido(request.con_correo))
                errors.Add("El correo del conductor no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(request.estado_conductor))
                errors.Add("El estado del conductor es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_conductor) &&
                request.estado_conductor != "ACT" &&
                request.estado_conductor != "INA")
            {
                errors.Add("El estado del conductor debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null && request.modificado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificado_desde_ip) &&
                request.modificado_desde_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_registro))
                errors.Add("El origen del registro es obligatorio.");

            if (request.origen_registro != null && request.origen_registro.Trim().Length > 20)
                errors.Add("El origen del registro no puede exceder 20 caracteres.");

            if (request.estado_conductor == "INA")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                    errors.Add("El motivo de inhabilitación es obligatorio cuando el estado es INA.");

                if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                    request.motivo_inhabilitacion.Trim().Length > 200)
                {
                    errors.Add("El motivo de inhabilitación no puede exceder 200 caracteres.");
                }
            }

            if (request.estado_conductor == "ACT" && !string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("No debe enviarse motivo de inhabilitación cuando el estado es ACT.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(ConductorFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.codigo_conductor) &&
                request.codigo_conductor.Trim().Length > 20)
            {
                errors.Add("El código del conductor no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.tipo_identificacion) &&
                request.tipo_identificacion.Trim().Length > 10)
            {
                errors.Add("El tipo de identificación no puede exceder 10 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.numero_identificacion) &&
                request.numero_identificacion.Trim().Length > 20)
            {
                errors.Add("El número de identificación no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.con_nombre1) &&
                request.con_nombre1.Trim().Length > 80)
            {
                errors.Add("El primer nombre no puede exceder 80 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.con_nombre2) &&
                request.con_nombre2.Trim().Length > 80)
            {
                errors.Add("El segundo nombre no puede exceder 80 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.con_apellido1) &&
                request.con_apellido1.Trim().Length > 80)
            {
                errors.Add("El primer apellido no puede exceder 80 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.con_apellido2) &&
                request.con_apellido2.Trim().Length > 80)
            {
                errors.Add("El segundo apellido no puede exceder 80 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.numero_licencia) &&
                request.numero_licencia.Trim().Length > 30)
            {
                errors.Add("El número de licencia no puede exceder 30 caracteres.");
            }

            if (request.fecha_vencimiento_licencia_desde.HasValue &&
                request.fecha_vencimiento_licencia_hasta.HasValue &&
                request.fecha_vencimiento_licencia_desde.Value > request.fecha_vencimiento_licencia_hasta.Value)
            {
                errors.Add("La fecha de vencimiento desde no puede ser mayor que la fecha de vencimiento hasta.");
            }

            if (request.edad_conductor.HasValue && request.edad_conductor.Value < 18)
                errors.Add("La edad del conductor no puede ser menor a 18 años.");

            if (!string.IsNullOrWhiteSpace(request.con_telefono) &&
                request.con_telefono.Trim().Length > 20)
            {
                errors.Add("El teléfono del conductor no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.con_correo) &&
                request.con_correo.Trim().Length > 120)
            {
                errors.Add("El correo del conductor no puede exceder 120 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.con_correo) && !EsCorreoValido(request.con_correo))
                errors.Add("El correo ingresado en el filtro no tiene un formato válido.");

            if (!string.IsNullOrWhiteSpace(request.estado_conductor) &&
                request.estado_conductor != "ACT" &&
                request.estado_conductor != "INA")
            {
                errors.Add("El estado del filtro debe ser ACT o INA.");
            }

            if (!string.IsNullOrWhiteSpace(request.origen_registro) &&
                request.origen_registro.Trim().Length > 20)
            {
                errors.Add("El origen del registro no puede exceder 20 caracteres.");
            }

            if (request.page_number <= 0)
                errors.Add("El número de página debe ser mayor que cero.");

            if (request.page_size <= 0)
                errors.Add("El tamaño de página debe ser mayor que cero.");

            if (request.page_size > 100)
                errors.Add("El tamaño de página no puede ser mayor a 100.");

            return errors;
        }

        private static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            correo = correo.Trim();

            return correo.Contains("@") &&
                   correo.Contains(".") &&
                   !correo.Contains(" ");
        }
    }
}