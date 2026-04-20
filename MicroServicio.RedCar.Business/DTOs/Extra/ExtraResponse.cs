using System;

namespace MicroServicio.RedCar.Business.DTOs.Extra
{
    public class ExtraResponse
    {
        public int id_extra { get; set; }
        public Guid extra_guid { get; set; }

        public string codigo_extra { get; set; } = null!;
        public string nombre_extra { get; set; } = null!;
        public string descripcion_extra { get; set; } = null!;

        public decimal valor_fijo { get; set; }

        public string estado_extra { get; set; } = null!;
        public bool es_eliminado { get; set; }

        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificado_desde_ip { get; set; }

        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        public string origen_registro { get; set; } = null!;
    }
}