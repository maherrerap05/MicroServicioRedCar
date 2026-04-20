namespace MicroServicio.RedCar.DataManagement.Models
{
    public class ReservaExtraDataModel
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_reserva_extra { get; set; }

        // =========================
        // IDENTIFICACIÓN
        // =========================
        public Guid reserva_extra_guid { get; set; }

        // =========================
        // RELACIONES
        // =========================
        public int id_reserva { get; set; }
        public int id_extra { get; set; }

        // =========================
        // DATOS OPERATIVOS
        // =========================
        public int cantidad { get; set; }
        public decimal valor_unitario_extra { get; set; }
        public decimal subtotal_extra { get; set; }

        // =========================
        // ESTADO
        // =========================
        public string estado_reserva_extra { get; set; } = null!;
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