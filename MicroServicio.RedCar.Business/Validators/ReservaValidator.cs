using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicio.RedCar.Business.DTOs.Reserva;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class ReservaValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearReservaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación de la reserva no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.codigo_reserva))
                errors.Add("El código de la reserva es obligatorio.");

            if (request.codigo_reserva != null && request.codigo_reserva.Trim().Length > 40)
                errors.Add("El código de la reserva no puede exceder 40 caracteres.");

            if (request.id_cliente <= 0)
                errors.Add("El cliente es obligatorio.");

            if (request.id_vehiculo <= 0)
                errors.Add("El vehículo es obligatorio.");

            if (request.id_localizacion_recogida <= 0)
                errors.Add("La localización de recogida es obligatoria.");

            if (request.id_localizacion_devolucion <= 0)
                errors.Add("La localización de devolución es obligatoria.");

            if (request.fecha_recogida == default)
                errors.Add("La fecha de recogida es obligatoria.");

            if (request.hora_recogida == default)
                errors.Add("La hora de recogida es obligatoria.");

            if (request.fecha_devolucion == default)
                errors.Add("La fecha de devolución es obligatoria.");

            if (request.hora_devolucion == default)
                errors.Add("La hora de devolución es obligatoria.");

            if (request.fecha_recogida != default &&
                request.fecha_devolucion != default)
            {
                var fechaHoraRecogida = request.fecha_recogida.Date + request.hora_recogida;
                var fechaHoraDevolucion = request.fecha_devolucion.Date + request.hora_devolucion;

                if (fechaHoraDevolucion <= fechaHoraRecogida)
                    errors.Add("La fecha y hora de devolución deben ser mayores que la fecha y hora de recogida.");
            }

            if (request.edad_conductor_principal < 18)
                errors.Add("La edad del conductor principal no puede ser menor a 18 años.");

            if (request.edad_conductor_principal > 100)
                errors.Add("La edad del conductor principal no puede ser mayor a 100 años.");

            if (!string.IsNullOrWhiteSpace(request.observaciones_reserva) &&
                request.observaciones_reserva.Trim().Length > 300)
            {
                errors.Add("Las observaciones de la reserva no pueden exceder 300 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_canal_reserva))
                errors.Add("El origen del canal de la reserva es obligatorio.");

            if (request.origen_canal_reserva != null &&
                request.origen_canal_reserva.Trim().Length > 50)
            {
                errors.Add("El origen del canal de la reserva no puede exceder 50 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_reserva))
                errors.Add("El estado de la reserva es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_reserva) &&
                request.estado_reserva != "PEN" &&
                request.estado_reserva != "CON" &&
                request.estado_reserva != "CAN" &&
                request.estado_reserva != "EXP" &&
                request.estado_reserva != "FIN" &&
                request.estado_reserva != "EMI")
            {
                errors.Add("El estado de la reserva debe ser PEN, CON, CAN, EXP, FIN o EMI.");
            }

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador es obligatorio.");

            if (request.creado_por_usuario != null &&
                request.creado_por_usuario.Trim().Length > 100)
            {
                errors.Add("El usuario creador no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null &&
                request.servicio_origen.Trim().Length > 50)
            {
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");
            }

            if (request.conductores == null)
                errors.Add("La lista de conductores no puede ser nula.");

            if (request.conductores != null)
            {
                if (request.conductores.Count == 0)
                    errors.Add("Debe existir al menos un conductor en la reserva.");

                var cantidadPrincipales = request.conductores.Count(x => x.es_principal);

                if (cantidadPrincipales == 0)
                    errors.Add("Debe existir un conductor principal en la reserva.");

                if (cantidadPrincipales > 1)
                    errors.Add("Solo puede existir un conductor principal en la reserva.");

                var conductoresDuplicados = request.conductores
                    .GroupBy(x => x.id_conductor)
                    .Where(x => x.Key > 0 && x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();

                if (conductoresDuplicados.Any())
                    errors.Add("No se pueden repetir conductores dentro de la misma reserva.");

                foreach (var conductor in request.conductores)
                {
                    var erroresConductor = ValidarConductor(conductor);
                    errors.AddRange(erroresConductor);
                }
            }

            if (request.extras != null)
            {
                var extrasDuplicados = request.extras
                    .GroupBy(x => x.id_extra)
                    .Where(x => x.Key > 0 && x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();

                if (extrasDuplicados.Any())
                    errors.Add("No se pueden repetir extras dentro de la misma reserva.");

                foreach (var extra in request.extras)
                {
                    var erroresExtra = ValidarExtra(extra);
                    errors.AddRange(erroresExtra);
                }
            }

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarReservaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización de la reserva no puede ser nula.");
                return errors;
            }

            if (request.id_reserva <= 0)
                errors.Add("El id de la reserva es inválido.");

            if (string.IsNullOrWhiteSpace(request.codigo_reserva))
                errors.Add("El código de la reserva es obligatorio.");

            if (request.codigo_reserva != null && request.codigo_reserva.Trim().Length > 40)
                errors.Add("El código de la reserva no puede exceder 40 caracteres.");

            if (request.id_cliente <= 0)
                errors.Add("El cliente es obligatorio.");

            if (request.id_vehiculo <= 0)
                errors.Add("El vehículo es obligatorio.");

            if (request.id_localizacion_recogida <= 0)
                errors.Add("La localización de recogida es obligatoria.");

            if (request.id_localizacion_devolucion <= 0)
                errors.Add("La localización de devolución es obligatoria.");

            if (request.fecha_recogida == default)
                errors.Add("La fecha de recogida es obligatoria.");

            if (request.hora_recogida == default)
                errors.Add("La hora de recogida es obligatoria.");

            if (request.fecha_devolucion == default)
                errors.Add("La fecha de devolución es obligatoria.");

            if (request.hora_devolucion == default)
                errors.Add("La hora de devolución es obligatoria.");

            if (request.fecha_recogida != default &&
                request.fecha_devolucion != default)
            {
                var fechaHoraRecogida = request.fecha_recogida.Date + request.hora_recogida;
                var fechaHoraDevolucion = request.fecha_devolucion.Date + request.hora_devolucion;

                if (fechaHoraDevolucion <= fechaHoraRecogida)
                    errors.Add("La fecha y hora de devolución deben ser mayores que la fecha y hora de recogida.");
            }

            if (request.edad_conductor_principal < 18)
                errors.Add("La edad del conductor principal no puede ser menor a 18 años.");

            if (request.edad_conductor_principal > 100)
                errors.Add("La edad del conductor principal no puede ser mayor a 100 años.");

            if (!string.IsNullOrWhiteSpace(request.observaciones_reserva) &&
                request.observaciones_reserva.Trim().Length > 300)
            {
                errors.Add("Las observaciones de la reserva no pueden exceder 300 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_canal_reserva))
                errors.Add("El origen del canal de la reserva es obligatorio.");

            if (request.origen_canal_reserva != null &&
                request.origen_canal_reserva.Trim().Length > 50)
            {
                errors.Add("El origen del canal de la reserva no puede exceder 50 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_reserva))
                errors.Add("El estado de la reserva es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_reserva) &&
                request.estado_reserva != "PEN" &&
                request.estado_reserva != "CON" &&
                request.estado_reserva != "CAN" &&
                request.estado_reserva != "EXP" &&
                request.estado_reserva != "FIN" &&
                request.estado_reserva != "EMI")
            {
                errors.Add("El estado de la reserva debe ser PEN, CON, CAN, EXP, FIN o EMI.");
            }

            if (request.estado_reserva == "CAN")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_cancelacion))
                    errors.Add("El motivo de cancelación es obligatorio cuando el estado es CAN.");

                if (!string.IsNullOrWhiteSpace(request.motivo_cancelacion) &&
                    request.motivo_cancelacion.Trim().Length > 250)
                {
                    errors.Add("El motivo de cancelación no puede exceder 250 caracteres.");
                }
            }

            if (request.estado_reserva != "CAN" &&
                !string.IsNullOrWhiteSpace(request.motivo_cancelacion))
            {
                errors.Add("No debe enviarse motivo de cancelación si el estado de la reserva no es CAN.");
            }

            if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                request.motivo_inhabilitacion.Trim().Length > 250)
            {
                errors.Add("El motivo de inhabilitación no puede exceder 250 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null &&
                request.modificado_por_usuario.Trim().Length > 100)
            {
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null &&
                request.servicio_origen.Trim().Length > 50)
            {
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");
            }

            if (request.conductores == null)
                errors.Add("La lista de conductores no puede ser nula.");

            if (request.conductores != null)
            {
                if (request.conductores.Count == 0)
                    errors.Add("Debe existir al menos un conductor en la reserva.");

                var cantidadPrincipales = request.conductores.Count(x => x.es_principal);

                if (cantidadPrincipales == 0)
                    errors.Add("Debe existir un conductor principal en la reserva.");

                if (cantidadPrincipales > 1)
                    errors.Add("Solo puede existir un conductor principal en la reserva.");

                var conductoresDuplicados = request.conductores
                    .GroupBy(x => x.id_conductor)
                    .Where(x => x.Key > 0 && x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();

                if (conductoresDuplicados.Any())
                    errors.Add("No se pueden repetir conductores dentro de la misma reserva.");

                foreach (var conductor in request.conductores)
                {
                    var erroresConductor = ValidarConductor(conductor);
                    errors.AddRange(erroresConductor);
                }
            }

            if (request.extras != null)
            {
                var extrasDuplicados = request.extras
                    .GroupBy(x => x.id_extra)
                    .Where(x => x.Key > 0 && x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();

                if (extrasDuplicados.Any())
                    errors.Add("No se pueden repetir extras dentro de la misma reserva.");

                foreach (var extra in request.extras)
                {
                    var erroresExtra = ValidarExtra(extra);
                    errors.AddRange(erroresExtra);
                }
            }

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(ReservaFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.codigo_reserva) &&
                request.codigo_reserva.Trim().Length > 40)
            {
                errors.Add("El código de la reserva no puede exceder 40 caracteres.");
            }

            if (request.id_cliente.HasValue && request.id_cliente.Value <= 0)
                errors.Add("El id del cliente debe ser mayor que cero.");

            if (request.id_vehiculo.HasValue && request.id_vehiculo.Value <= 0)
                errors.Add("El id del vehículo debe ser mayor que cero.");

            if (request.id_localizacion_recogida.HasValue && request.id_localizacion_recogida.Value <= 0)
                errors.Add("El id de la localización de recogida debe ser mayor que cero.");

            if (request.id_localizacion_devolucion.HasValue && request.id_localizacion_devolucion.Value <= 0)
                errors.Add("El id de la localización de devolución debe ser mayor que cero.");

            if (request.fecha_recogida_desde.HasValue &&
                request.fecha_recogida_hasta.HasValue &&
                request.fecha_recogida_desde.Value > request.fecha_recogida_hasta.Value)
            {
                errors.Add("La fecha de recogida desde no puede ser mayor que la fecha de recogida hasta.");
            }

            if (request.fecha_devolucion_desde.HasValue &&
                request.fecha_devolucion_hasta.HasValue &&
                request.fecha_devolucion_desde.Value > request.fecha_devolucion_hasta.Value)
            {
                errors.Add("La fecha de devolución desde no puede ser mayor que la fecha de devolución hasta.");
            }

            if (request.fecha_reserva_utc_desde.HasValue &&
                request.fecha_reserva_utc_hasta.HasValue &&
                request.fecha_reserva_utc_desde.Value > request.fecha_reserva_utc_hasta.Value)
            {
                errors.Add("La fecha de reserva desde no puede ser mayor que la fecha de reserva hasta.");
            }

            if (!string.IsNullOrWhiteSpace(request.origen_canal_reserva) &&
                request.origen_canal_reserva.Trim().Length > 50)
            {
                errors.Add("El origen del canal de la reserva no puede exceder 50 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.estado_reserva) &&
                request.estado_reserva != "PEN" &&
                request.estado_reserva != "CON" &&
                request.estado_reserva != "CAN" &&
                request.estado_reserva != "EXP" &&
                request.estado_reserva != "FIN" &&
                request.estado_reserva != "EMI")
            {
                errors.Add("El estado de la reserva debe ser PEN, CON, CAN, EXP, FIN o EMI.");
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

        public static IReadOnlyCollection<string> ValidarConfirmacion(ConfirmarReservaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de confirmación no puede ser nula.");
                return errors;
            }

            if (request.id_reserva <= 0)
                errors.Add("El id de la reserva es inválido.");

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null &&
                request.modificado_por_usuario.Trim().Length > 100)
            {
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null &&
                request.servicio_origen.Trim().Length > 50)
            {
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");
            }

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarCancelacion(CancelarReservaRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de cancelación no puede ser nula.");
                return errors;
            }

            if (request.id_reserva <= 0)
                errors.Add("El id de la reserva es inválido.");

            if (string.IsNullOrWhiteSpace(request.motivo_cancelacion))
                errors.Add("El motivo de cancelación es obligatorio.");

            if (request.motivo_cancelacion != null &&
                request.motivo_cancelacion.Trim().Length > 250)
            {
                errors.Add("El motivo de cancelación no puede exceder 250 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null &&
                request.modificado_por_usuario.Trim().Length > 100)
            {
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modificacion_ip) &&
                request.modificacion_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.servicio_origen))
                errors.Add("El servicio de origen es obligatorio.");

            if (request.servicio_origen != null &&
                request.servicio_origen.Trim().Length > 50)
            {
                errors.Add("El servicio de origen no puede exceder 50 caracteres.");
            }

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarExtra(ReservaExtraRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("El detalle del extra no puede ser nulo.");
                return errors;
            }

            if (request.id_extra <= 0)
                errors.Add("El extra es obligatorio.");

            if (request.cantidad < 1)
                errors.Add("La cantidad del extra debe ser mayor o igual a 1.");

            if (string.IsNullOrWhiteSpace(request.estado_reserva_extra))
                errors.Add("El estado del extra de reserva es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_reserva_extra) &&
                request.estado_reserva_extra != "ACT" &&
                request.estado_reserva_extra != "INA")
            {
                errors.Add("El estado del extra de reserva debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador del extra es obligatorio.");

            if (request.creado_por_usuario != null &&
                request.creado_por_usuario.Trim().Length > 100)
            {
                errors.Add("El usuario creador del extra no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modificado_desde_ip) &&
                request.modificado_desde_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación del extra no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_registro))
                errors.Add("El origen del registro del extra es obligatorio.");

            if (request.origen_registro != null &&
                request.origen_registro.Trim().Length > 20)
            {
                errors.Add("El origen del registro del extra no puede exceder 20 caracteres.");
            }

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarConductor(ReservaConductorRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("El detalle del conductor no puede ser nulo.");
                return errors;
            }

            if (request.id_conductor <= 0)
                errors.Add("El conductor es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.tipo_conductor))
                errors.Add("El tipo de conductor es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.tipo_conductor) &&
                request.tipo_conductor != "PRI" &&
                request.tipo_conductor != "ADI")
            {
                errors.Add("El tipo de conductor debe ser PRI o ADI.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_reserva_conductor))
                errors.Add("El estado del conductor de reserva es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_reserva_conductor) &&
                request.estado_reserva_conductor != "ACT" &&
                request.estado_reserva_conductor != "INA")
            {
                errors.Add("El estado del conductor de reserva debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador del conductor es obligatorio.");

            if (request.creado_por_usuario != null &&
                request.creado_por_usuario.Trim().Length > 100)
            {
                errors.Add("El usuario creador del conductor no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modificado_desde_ip) &&
                request.modificado_desde_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación del conductor no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_registro))
                errors.Add("El origen del registro del conductor es obligatorio.");

            if (request.origen_registro != null &&
                request.origen_registro.Trim().Length > 20)
            {
                errors.Add("El origen del registro del conductor no puede exceder 20 caracteres.");
            }

            if (request.es_principal && request.tipo_conductor == "ADI")
                errors.Add("Un conductor adicional no puede marcarse como principal.");

            return errors;
        }
    }
}