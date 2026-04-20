using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.Extra;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class ExtraValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearExtraRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación del extra no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.codigo_extra))
                errors.Add("El código del extra es obligatorio.");

            if (request.codigo_extra != null && request.codigo_extra.Trim().Length > 20)
                errors.Add("El código del extra no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombre_extra))
                errors.Add("El nombre del extra es obligatorio.");

            if (request.nombre_extra != null && request.nombre_extra.Trim().Length > 100)
                errors.Add("El nombre del extra no puede exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(request.descripcion_extra))
                errors.Add("La descripción del extra es obligatoria.");

            if (request.descripcion_extra != null && request.descripcion_extra.Trim().Length > 250)
                errors.Add("La descripción del extra no puede exceder 250 caracteres.");

            if (request.valor_fijo <= 0)
                errors.Add("El valor fijo debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(request.estado_extra))
                errors.Add("El estado del extra es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_extra) &&
                request.estado_extra != "ACT" &&
                request.estado_extra != "INA")
            {
                errors.Add("El estado del extra debe ser ACT o INA.");
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

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarExtraRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización del extra no puede ser nula.");
                return errors;
            }

            if (request.id_extra <= 0)
                errors.Add("El id del extra es inválido.");

            if (string.IsNullOrWhiteSpace(request.codigo_extra))
                errors.Add("El código del extra es obligatorio.");

            if (request.codigo_extra != null && request.codigo_extra.Trim().Length > 20)
                errors.Add("El código del extra no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombre_extra))
                errors.Add("El nombre del extra es obligatorio.");

            if (request.nombre_extra != null && request.nombre_extra.Trim().Length > 100)
                errors.Add("El nombre del extra no puede exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(request.descripcion_extra))
                errors.Add("La descripción del extra es obligatoria.");

            if (request.descripcion_extra != null && request.descripcion_extra.Trim().Length > 250)
                errors.Add("La descripción del extra no puede exceder 250 caracteres.");

            if (request.valor_fijo <= 0)
                errors.Add("El valor fijo debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(request.estado_extra))
                errors.Add("El estado del extra es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_extra) &&
                request.estado_extra != "ACT" &&
                request.estado_extra != "INA")
            {
                errors.Add("El estado del extra debe ser ACT o INA.");
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

            if (request.estado_extra == "INA")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                    errors.Add("El motivo de inhabilitación es obligatorio cuando el estado es INA.");

                if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                    request.motivo_inhabilitacion.Trim().Length > 200)
                {
                    errors.Add("El motivo de inhabilitación no puede exceder 200 caracteres.");
                }
            }

            if (request.estado_extra == "ACT" && !string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("No debe enviarse motivo de inhabilitación cuando el estado es ACT.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(ExtraFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.codigo_extra) &&
                request.codigo_extra.Trim().Length > 20)
            {
                errors.Add("El código del extra no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.nombre_extra) &&
                request.nombre_extra.Trim().Length > 100)
            {
                errors.Add("El nombre del extra no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.descripcion_extra) &&
                request.descripcion_extra.Trim().Length > 250)
            {
                errors.Add("La descripción del extra no puede exceder 250 caracteres.");
            }

            if (request.valor_fijo_desde.HasValue && request.valor_fijo_desde.Value <= 0)
                errors.Add("El valor fijo desde debe ser mayor que cero.");

            if (request.valor_fijo_hasta.HasValue && request.valor_fijo_hasta.Value <= 0)
                errors.Add("El valor fijo hasta debe ser mayor que cero.");

            if (request.valor_fijo_desde.HasValue && request.valor_fijo_hasta.HasValue &&
                request.valor_fijo_desde.Value > request.valor_fijo_hasta.Value)
            {
                errors.Add("El valor fijo desde no puede ser mayor que el valor fijo hasta.");
            }

            if (!string.IsNullOrWhiteSpace(request.estado_extra) &&
                request.estado_extra != "ACT" &&
                request.estado_extra != "INA")
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
    }
}