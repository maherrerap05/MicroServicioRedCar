using System;

namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class ConfirmarReservaRequest
    {
        public int id_reserva { get; set; }

        public string modificado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;
    }
}