using System;

namespace MicroServicio.RedCar.Business.DTOs.Extra
{
    public class CrearExtraRequest
    {
        public string codigo_extra { get; set; } = null!;
        public string nombre_extra { get; set; } = null!;
        public string descripcion_extra { get; set; } = null!;

        public decimal valor_fijo { get; set; }

        public string estado_extra { get; set; } = "ACT";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificado_desde_ip { get; set; }
        public string origen_registro { get; set; } = null!;
    }
}