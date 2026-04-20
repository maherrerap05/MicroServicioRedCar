namespace MicroServicio.RedCar.DataManagement.Models
{
    public class ReservaConductorDataModel
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_reserva_conductor { get; set; }

        // =========================
        // IDENTIFICACIÓN
        // =========================
        public Guid reserva_conductor_guid { get; set; }

        // =========================
        // RELACIONES
        // =========================
        public int id_reserva { get; set; }
        public int id_conductor { get; set; }

        // =========================
        // DATOS OPERATIVOS
        // =========================
        public string tipo_conductor { get; set; } = null!;
        public bool es_principal { get; set; }
        public DateTime fecha_asignacion_utc { get; set; }

        // =========================
        // ESTADO
        // =========================
        public string estado_reserva_conductor { get; set; } = null!;
        public bool es_eliminado { get; set; }

        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        // =========================
        // AUDITORÍA
        // =========================
        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificado_desde_ip { get; set; }

        public string origen_registro { get; set; } = null!;
        public byte[] row_version { get; set; } = null!;
    }
}