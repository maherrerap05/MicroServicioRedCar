using MicroServicio.RedCar.Business.DTOs.Factura;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class FacturaValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearFacturaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación de la factura no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.numero_factura))
                errors.Add("El número de factura es obligatorio.");

            if (request.numero_factura != null && request.numero_factura.Trim().Length > 40)
                errors.Add("El número de factura no puede exceder 40 caracteres.");

            if (request.id_reserva <= 0)
                errors.Add("La reserva es obligatoria.");

            if (!string.IsNullOrWhiteSpace(request.observaciones_factura) &&
                request.observaciones_factura.Trim().Length > 300)
                errors.Add("Las observaciones de la factura no pueden exceder 300 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.origen_canal_factura) &&
                request.origen_canal_factura.Trim().Length > 50)
                errors.Add("El origen del canal de la factura no puede exceder 50 caracteres.");

            if (string.IsNullOrWhiteSpace(request.estado))
                errors.Add("El estado de la factura es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado) &&
                request.estado != "ABI" &&
                request.estado != "APR" &&
                request.estado != "INA")
                errors.Add("El estado de la factura debe ser ABI, APR o INA.");

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador es obligatorio.");

            if (request.creado_por_usuario != null && request.creado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario creador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null && request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            if (request.estado == "INA")
                errors.Add("Para inhabilitar una factura debe usarse el proceso de anulación y no la creación.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarFacturaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización de la factura no puede ser nula.");
                return errors;
            }

            if (request.id_factura <= 0)
                errors.Add("El id de la factura es inválido.");

            if (!string.IsNullOrWhiteSpace(request.observaciones_factura) &&
                request.observaciones_factura.Trim().Length > 300)
                errors.Add("Las observaciones de la factura no pueden exceder 300 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.origen_canal_factura) &&
                request.origen_canal_factura.Trim().Length > 50)
                errors.Add("El origen del canal de la factura no puede exceder 50 caracteres.");

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null && request.modificado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null && request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(FacturaFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.numero_factura) &&
                request.numero_factura.Trim().Length > 40)
                errors.Add("El número de factura no puede exceder 40 caracteres.");

            if (request.id_cliente.HasValue && request.id_cliente.Value <= 0)
                errors.Add("El cliente debe ser mayor que cero.");

            if (request.id_reserva.HasValue && request.id_reserva.Value <= 0)
                errors.Add("La reserva debe ser mayor que cero.");

            if (request.fecha_emision_desde.HasValue && request.fecha_emision_hasta.HasValue &&
                request.fecha_emision_desde.Value > request.fecha_emision_hasta.Value)
                errors.Add("La fecha de emisión desde no puede ser mayor que la fecha de emisión hasta.");

            if (!string.IsNullOrWhiteSpace(request.origen_canal_factura) &&
                request.origen_canal_factura.Trim().Length > 50)
                errors.Add("El origen del canal de la factura no puede exceder 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.estado) &&
                request.estado != "ABI" &&
                request.estado != "APR" &&
                request.estado != "INA")
                errors.Add("El estado del filtro debe ser ABI, APR o INA.");

            if (!string.IsNullOrWhiteSpace(request.servicio_origen) &&
                request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            if (request.page_number <= 0)
                errors.Add("El número de página debe ser mayor que cero.");

            if (request.page_size <= 0)
                errors.Add("El tamaño de página debe ser mayor que cero.");

            if (request.page_size > 100)
                errors.Add("El tamaño de página no puede ser mayor a 100.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarAprobacion(AprobarFacturaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de aprobación de la factura no puede ser nula.");
                return errors;
            }

            if (request.id_factura <= 0)
                errors.Add("El id de la factura es inválido.");

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null && request.modificado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null && request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarAnulacion(AnularFacturaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de anulación de la factura no puede ser nula.");
                return errors;
            }

            if (request.id_factura <= 0)
                errors.Add("El id de la factura es inválido.");

            if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("El motivo de inhabilitación es obligatorio.");

            if (request.motivo_inhabilitacion != null && request.motivo_inhabilitacion.Trim().Length > 250)
                errors.Add("El motivo de inhabilitación no puede exceder 250 caracteres.");

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null && request.modificado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null && request.servicio_origen.Trim().Length > 50)
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");

            return errors;
        }
    }
}