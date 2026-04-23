namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class ReservaExtraRequest
    {
        public int id_extra { get; set; }

        public int cantidad { get; set; } = 1;

        public string estado_reserva_extra { get; set; } = "ACT";

        public string? creado_por_usuario { get; set; }
        public string? modificado_por_usuario { get; set; }  // ← AGREGAR
        public string? modificado_desde_ip { get; set; }
        public string? origen_registro { get; set; }
    }
}