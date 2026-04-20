using System;

namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class CancelarReservaRequest
    {
        public int id_reserva { get; set; }

        public string motivo_cancelacion { get; set; } = null!;

        public string modificado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;
    }
}