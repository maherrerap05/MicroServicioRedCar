namespace MicroServicio.RedCar.DataManagement.Models
{
    public class AuditoriaDataModel
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public long id_auditoria { get; set; }

        // =========================
        // IDENTIFICACIÓN
        // =========================
        public Guid auditoria_guid { get; set; }

        // =========================
        // DATOS DEL EVENTO
        // =========================
        public string tabla_afectada { get; set; } = null!;
        public string operacion { get; set; } = null!;

        public string? id_registro_afectado { get; set; }

        public string? datos_anteriores { get; set; }
        public string? datos_nuevos { get; set; }

        public string usuario_ejecutor { get; set; } = null!;
        public string? ip_origen { get; set; }

        public DateTime fecha_evento_utc { get; set; }

        // =========================
        // ESTADO
        // =========================
        public bool activo { get; set; }

        // =========================
        // CONTROL DE CONCURRENCIA
        // =========================
        public byte[] row_version { get; set; } = null!;
    }
}