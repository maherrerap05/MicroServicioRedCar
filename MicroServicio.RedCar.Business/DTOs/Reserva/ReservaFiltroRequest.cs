using System;

namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class ReservaFiltroRequest
    {
        public string? codigo_reserva { get; set; }

        public int? id_cliente { get; set; }
        public int? id_vehiculo { get; set; }
        public int? id_localizacion_recogida { get; set; }
        public int? id_localizacion_devolucion { get; set; }

        public DateTime? fecha_recogida_desde { get; set; }
        public DateTime? fecha_recogida_hasta { get; set; }

        public DateTime? fecha_devolucion_desde { get; set; }
        public DateTime? fecha_devolucion_hasta { get; set; }

        public DateTime? fecha_reserva_utc_desde { get; set; }
        public DateTime? fecha_reserva_utc_hasta { get; set; }

        public string? origen_canal_reserva { get; set; }
        public string? estado_reserva { get; set; }
        public string? servicio_origen { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}