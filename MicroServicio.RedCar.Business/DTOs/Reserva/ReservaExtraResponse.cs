using System;

namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class ReservaExtraResponse
    {
        public int id_reserva_extra { get; set; }
        public Guid reserva_extra_guid { get; set; }

        public int id_extra { get; set; }

        public int cantidad { get; set; }
        public decimal valor_unitario_extra { get; set; }
        public decimal subtotal_extra { get; set; }

        public string estado_reserva_extra { get; set; } = null!;
    }
}