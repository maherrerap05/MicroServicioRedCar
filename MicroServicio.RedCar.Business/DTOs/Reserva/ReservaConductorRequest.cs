namespace MicroServicio.RedCar.Business.DTOs.Reserva
{
    public class ReservaConductorRequest
    {
        public int id_conductor { get; set; }

        public string tipo_conductor { get; set; } = null!;
        public bool es_principal { get; set; }

        public string estado_reserva_conductor { get; set; } = "ACT";

        public string? creado_por_usuario { get; set; }
        public string? modificado_por_usuario { get; set; }  // ← AGREGAR
        public string? modificado_desde_ip { get; set; }
        public string? origen_registro { get; set; }
    }
}