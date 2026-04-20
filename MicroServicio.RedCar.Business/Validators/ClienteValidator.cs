using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.Cliente;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class ClienteValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearClienteRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación del cliente no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.tipo_identificacion))
                errors.Add("El tipo de identificación es obligatorio.");

            if (request.tipo_identificacion != null && request.tipo_identificacion.Trim().Length > 20)
                errors.Add("El tipo de identificación no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.numero_identificacion))
                errors.Add("El número de identificación es obligatorio.");

            if (request.numero_identificacion != null && request.numero_identificacion.Trim().Length > 30)
                errors.Add("El número de identificación no puede exceder 30 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.razon_social) && request.razon_social.Trim().Length > 200)
                errors.Add("La razón social no puede exceder 200 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombres))
                errors.Add("Los nombres son obligatorios.");

            if (request.nombres != null && request.nombres.Trim().Length > 160)
                errors.Add("Los nombres no pueden exceder 160 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.apellidos) && request.apellidos.Trim().Length > 160)
                errors.Add("Los apellidos no pueden exceder 160 caracteres.");

            if (string.IsNullOrWhiteSpace(request.correo))
                errors.Add("El correo es obligatorio.");

            if (request.correo != null && request.correo.Trim().Length > 150)
                errors.Add("El correo no puede exceder 150 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.correo) && !EsCorreoValido(request.correo))
                errors.Add("El correo no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(request.telefono))
                errors.Add("El teléfono es obligatorio.");

            if (request.telefono != null && request.telefono.Trim().Length > 30)
                errors.Add("El teléfono no puede exceder 30 caracteres.");

            if (string.IsNullOrWhiteSpace(request.direccion))
                errors.Add("La dirección es obligatoria.");

            if (request.direccion != null && request.direccion.Trim().Length > 250)
                errors.Add("La dirección no puede exceder 250 caracteres.");

            if (string.IsNullOrWhiteSpace(request.estado))
                errors.Add("El estado es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado) &&
                request.estado != "ACT" &&
                request.estado != "INA")
            {
                errors.Add("El estado del cliente debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador es obligatorio.");

            if (request.creado_por_usuario != null && request.creado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario creador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) && request.modificacion_ip.Trim().Length > 45)
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null && request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarClienteRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización del cliente no puede ser nula.");
                return errors;
            }

            if (request.id_cliente <= 0)
                errors.Add("El id del cliente es inválido.");

            if (string.IsNullOrWhiteSpace(request.tipo_identificacion))
                errors.Add("El tipo de identificación es obligatorio.");

            if (request.tipo_identificacion != null && request.tipo_identificacion.Trim().Length > 20)
                errors.Add("El tipo de identificación no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.numero_identificacion))
                errors.Add("El número de identificación es obligatorio.");

            if (request.numero_identificacion != null && request.numero_identificacion.Trim().Length > 30)
                errors.Add("El número de identificación no puede exceder 30 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.razon_social) && request.razon_social.Trim().Length > 200)
                errors.Add("La razón social no puede exceder 200 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombres))
                errors.Add("Los nombres son obligatorios.");

            if (request.nombres != null && request.nombres.Trim().Length > 160)
                errors.Add("Los nombres no pueden exceder 160 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.apellidos) && request.apellidos.Trim().Length > 160)
                errors.Add("Los apellidos no pueden exceder 160 caracteres.");

            if (string.IsNullOrWhiteSpace(request.correo))
                errors.Add("El correo es obligatorio.");

            if (request.correo != null && request.correo.Trim().Length > 150)
                errors.Add("El correo no puede exceder 150 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.correo) && !EsCorreoValido(request.correo))
                errors.Add("El correo no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(request.telefono))
                errors.Add("El teléfono es obligatorio.");

            if (request.telefono != null && request.telefono.Trim().Length > 30)
                errors.Add("El teléfono no puede exceder 30 caracteres.");

            if (string.IsNullOrWhiteSpace(request.direccion))
                errors.Add("La dirección es obligatoria.");

            if (request.direccion != null && request.direccion.Trim().Length > 250)
                errors.Add("La dirección no puede exceder 250 caracteres.");

            if (string.IsNullOrWhiteSpace(request.estado))
                errors.Add("El estado es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado) &&
                request.estado != "ACT" &&
                request.estado != "INA")
            {
                errors.Add("El estado del cliente debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null && request.modificado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) && request.modificacion_ip.Trim().Length > 45)
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null && request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            if (request.estado == "INA")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                    errors.Add("El motivo de inhabilitación es obligatorio cuando el estado es INA.");

                if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                    request.motivo_inhabilitacion.Trim().Length > 250)
                {
                    errors.Add("El motivo de inhabilitación no puede exceder 250 caracteres.");
                }
            }

            if (request.estado == "ACT" && !string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("No debe enviarse motivo de inhabilitación cuando el estado es ACT.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(ClienteFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.tipo_identificacion) &&
                request.tipo_identificacion.Trim().Length > 20)
            {
                errors.Add("El tipo de identificación no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.numero_identificacion) &&
                request.numero_identificacion.Trim().Length > 30)
            {
                errors.Add("El número de identificación no puede exceder 30 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.razon_social) &&
                request.razon_social.Trim().Length > 200)
            {
                errors.Add("La razón social no puede exceder 200 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.nombres) &&
                request.nombres.Trim().Length > 160)
            {
                errors.Add("Los nombres no pueden exceder 160 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.apellidos) &&
                request.apellidos.Trim().Length > 160)
            {
                errors.Add("Los apellidos no pueden exceder 160 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.correo) &&
                request.correo.Trim().Length > 150)
            {
                errors.Add("El correo no puede exceder 150 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.correo) && !EsCorreoValido(request.correo))
                errors.Add("El correo ingresado en el filtro no tiene un formato válido.");

            if (!string.IsNullOrWhiteSpace(request.telefono) &&
                request.telefono.Trim().Length > 30)
            {
                errors.Add("El teléfono no puede exceder 30 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.direccion) &&
                request.direccion.Trim().Length > 250)
            {
                errors.Add("La dirección no puede exceder 250 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.estado) &&
                request.estado != "ACT" &&
                request.estado != "INA")
            {
                errors.Add("El estado del filtro debe ser ACT o INA.");
            }

            if (!string.IsNullOrWhiteSpace(request.servicio_origen) &&
                request.servicio_origen.Trim().Length > 50)
            {
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");
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