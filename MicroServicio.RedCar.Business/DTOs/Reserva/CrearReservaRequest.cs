using System;
using System.Collections.Generic;

namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class CrearReservaRequest
    {
        public string codigo_reserva { get; set; } = null!;

        public int id_cliente { get; set; }
        public int id_vehiculo { get; set; }
        public int id_localizacion_recogida { get; set; }
        public int id_localizacion_devolucion { get; set; }

        public DateTime fecha_recogida { get; set; }
        public TimeSpan hora_recogida { get; set; }

        public DateTime fecha_devolucion { get; set; }
        public TimeSpan hora_devolucion { get; set; }

        public byte edad_conductor_principal { get; set; }

        public string? observaciones_reserva { get; set; }
        public string origen_canal_reserva { get; set; } = null!;
        public string estado_reserva { get; set; } = "PEN";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;

        public List<ReservaConductorRequest> conductores { get; set; } = new();
        public List<ReservaExtraRequest> extras { get; set; } = new();
    }
}