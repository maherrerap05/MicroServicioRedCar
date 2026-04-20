using System;

namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class ReservaConductorResponse
    {
        public int id_reserva_conductor { get; set; }
        public Guid reserva_conductor_guid { get; set; }

        public int id_conductor { get; set; }

        public string tipo_conductor { get; set; } = null!;
        public bool es_principal { get; set; }

        public DateTime fecha_asignacion_utc { get; set; }

        public string estado_reserva_conductor { get; set; } = null!;
    }
}