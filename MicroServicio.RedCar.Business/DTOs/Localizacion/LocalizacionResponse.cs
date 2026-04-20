using System;

namespace MicroServicio.RedCar.Business.DTOs.Localizacion
{
    public class LocalizacionResponse
    {
        public int id_localizacion { get; set; }
        public Guid localizacion_guid { get; set; }

        public string codigo_localizacion { get; set; } = null!;
        public string nombre_localizacion { get; set; } = null!;
        public string direccion_localizacion { get; set; } = null!;

        public string telefono_contacto { get; set; } = null!;
        public string correo_contacto { get; set; } = null!;
        public string horario_atencion { get; set; } = null!;

        public string zona_horaria { get; set; } = null!;
        public int id_ciudad { get; set; }

        public string estado_localizacion { get; set; } = null!;
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