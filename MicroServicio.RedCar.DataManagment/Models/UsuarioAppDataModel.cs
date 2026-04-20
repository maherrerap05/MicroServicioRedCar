namespace MicroServicio.RedCar.DataManagement.Models
{
    public class UsuarioAppDataModel
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_usuario { get; set; }

        // =========================
        // IDENTIFICACIÓN
        // =========================
        public Guid usuario_guid { get; set; }

        public string username { get; set; } = null!;
        public string correo { get; set; } = null!;

        // =========================
        // SEGURIDAD
        // =========================
        public string password_hash { get; set; } = null!;
        public string password_salt { get; set; } = null!;

        // =========================
        // ESTADO
        // =========================
        public string estado_usuario { get; set; } = null!;
        public bool es_eliminado { get; set; }
        public bool activo { get; set; }

        // =========================
        // RELACIONES
        // =========================
        public int id_cliente { get; set; }

        // =========================
        // AUDITORÍA
        // =========================
        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }

        public byte[] row_version { get; set; } = null!;
    }
}