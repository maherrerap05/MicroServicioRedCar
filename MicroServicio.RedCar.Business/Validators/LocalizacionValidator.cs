using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.Localizacion;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class LocalizacionValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearLocalizacionRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación de la localización no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.codigo_localizacion))
                errors.Add("El código de la localización es obligatorio.");

            if (request.codigo_localizacion != null && request.codigo_localizacion.Trim().Length > 20)
                errors.Add("El código de la localización no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombre_localizacion))
                errors.Add("El nombre de la localización es obligatorio.");

            if (request.nombre_localizacion != null && request.nombre_localizacion.Trim().Length > 100)
                errors.Add("El nombre de la localización no puede exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(request.direccion_localizacion))
                errors.Add("La dirección de la localización es obligatoria.");

            if (request.direccion_localizacion != null && request.direccion_localizacion.Trim().Length > 200)
                errors.Add("La dirección de la localización no puede exceder 200 caracteres.");

            if (string.IsNullOrWhiteSpace(request.telefono_contacto))
                errors.Add("El teléfono de contacto es obligatorio.");

            if (request.telefono_contacto != null && request.telefono_contacto.Trim().Length > 20)
                errors.Add("El teléfono de contacto no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.correo_contacto))
                errors.Add("El correo de contacto es obligatorio.");

            if (request.correo_contacto != null && request.correo_contacto.Trim().Length > 120)
                errors.Add("El correo de contacto no puede exceder 120 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.correo_contacto) && !EsCorreoValido(request.correo_contacto))
                errors.Add("El correo de contacto no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(request.horario_atencion))
                errors.Add("El horario de atención es obligatorio.");

            if (request.horario_atencion != null && request.horario_atencion.Trim().Length > 120)
                errors.Add("El horario de atención no puede exceder 120 caracteres.");

            if (string.IsNullOrWhiteSpace(request.zona_horaria))
                errors.Add("La zona horaria es obligatoria.");

            if (request.zona_horaria != null && request.zona_horaria.Trim().Length > 50)
                errors.Add("La zona horaria no puede exceder 50 caracteres.");

            if (request.id_ciudad <= 0)
                errors.Add("La ciudad es obligatoria.");

            if (string.IsNullOrWhiteSpace(request.estado_localizacion))
                errors.Add("El estado de la localización es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_localizacion) &&
                request.estado_localizacion != "ACT" &&
                request.estado_localizacion != "INA")
            {
                errors.Add("El estado de la localización debe ser ACT o INA.");
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

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarLocalizacionRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización de la localización no puede ser nula.");
                return errors;
            }

            if (request.id_localizacion <= 0)
                errors.Add("El id de la localización es inválido.");

            if (string.IsNullOrWhiteSpace(request.codigo_localizacion))
                errors.Add("El código de la localización es obligatorio.");

            if (request.codigo_localizacion != null && request.codigo_localizacion.Trim().Length > 20)
                errors.Add("El código de la localización no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombre_localizacion))
                errors.Add("El nombre de la localización es obligatorio.");

            if (request.nombre_localizacion != null && request.nombre_localizacion.Trim().Length > 100)
                errors.Add("El nombre de la localización no puede exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(request.direccion_localizacion))
                errors.Add("La dirección de la localización es obligatoria.");

            if (request.direccion_localizacion != null && request.direccion_localizacion.Trim().Length > 200)
                errors.Add("La dirección de la localización no puede exceder 200 caracteres.");

            if (string.IsNullOrWhiteSpace(request.telefono_contacto))
                errors.Add("El teléfono de contacto es obligatorio.");

            if (request.telefono_contacto != null && request.telefono_contacto.Trim().Length > 20)
                errors.Add("El teléfono de contacto no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.correo_contacto))
                errors.Add("El correo de contacto es obligatorio.");

            if (request.correo_contacto != null && request.correo_contacto.Trim().Length > 120)
                errors.Add("El correo de contacto no puede exceder 120 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.correo_contacto) && !EsCorreoValido(request.correo_contacto))
                errors.Add("El correo de contacto no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(request.horario_atencion))
                errors.Add("El horario de atención es obligatorio.");

            if (request.horario_atencion != null && request.horario_atencion.Trim().Length > 120)
                errors.Add("El horario de atención no puede exceder 120 caracteres.");

            if (string.IsNullOrWhiteSpace(request.zona_horaria))
                errors.Add("La zona horaria es obligatoria.");

            if (request.zona_horaria != null && request.zona_horaria.Trim().Length > 50)
                errors.Add("La zona horaria no puede exceder 50 caracteres.");

            if (request.id_ciudad <= 0)
                errors.Add("La ciudad es obligatoria.");

            if (string.IsNullOrWhiteSpace(request.estado_localizacion))
                errors.Add("El estado de la localización es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_localizacion) &&
                request.estado_localizacion != "ACT" &&
                request.estado_localizacion != "INA")
            {
                errors.Add("El estado de la localización debe ser ACT o INA.");
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

            if (request.estado_localizacion == "INA")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                    errors.Add("El motivo de inhabilitación es obligatorio cuando el estado es INA.");

                if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                    request.motivo_inhabilitacion.Trim().Length > 200)
                {
                    errors.Add("El motivo de inhabilitación no puede exceder 200 caracteres.");
                }
            }

            if (request.estado_localizacion == "ACT" && !string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("No debe enviarse motivo de inhabilitación cuando el estado es ACT.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(LocalizacionFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.codigo_localizacion) &&
                request.codigo_localizacion.Trim().Length > 20)
            {
                errors.Add("El código de la localización no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.nombre_localizacion) &&
                request.nombre_localizacion.Trim().Length > 100)
            {
                errors.Add("El nombre de la localización no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.direccion_localizacion) &&
                request.direccion_localizacion.Trim().Length > 200)
            {
                errors.Add("La dirección de la localización no puede exceder 200 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.telefono_contacto) &&
                request.telefono_contacto.Trim().Length > 20)
            {
                errors.Add("El teléfono de contacto no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.correo_contacto) &&
                request.correo_contacto.Trim().Length > 120)
            {
                errors.Add("El correo de contacto no puede exceder 120 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.correo_contacto) && !EsCorreoValido(request.correo_contacto))
                errors.Add("El correo ingresado en el filtro no tiene un formato válido.");

            if (!string.IsNullOrWhiteSpace(request.horario_atencion) &&
                request.horario_atencion.Trim().Length > 120)
            {
                errors.Add("El horario de atención no puede exceder 120 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.zona_horaria) &&
                request.zona_horaria.Trim().Length > 50)
            {
                errors.Add("La zona horaria no puede exceder 50 caracteres.");
            }

            if (request.id_ciudad.HasValue && request.id_ciudad.Value <= 0)
                errors.Add("La ciudad debe ser mayor que cero.");

            if (!string.IsNullOrWhiteSpace(request.estado_localizacion) &&
                request.estado_localizacion != "ACT" &&
                request.estado_localizacion != "INA")
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