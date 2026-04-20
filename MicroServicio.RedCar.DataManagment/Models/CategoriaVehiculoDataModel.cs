namespace MicroServicio.RedCar.DataManagement.Models
{
    public class CategoriaVehiculoDataModel
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_categoria_vehiculo { get; set; }

        // =========================
        // CAMPOS PRINCIPALES
        // =========================
        public Guid categoria_vehiculo_guid { get; set; }

        public string codigo_categoria_vehiculo { get; set; } = null!;
        public string nombre_categoria_vehiculo { get; set; } = null!;
        public string? descripcion_categoria_vehiculo { get; set; }

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado_categoria_vehiculo { get; set; } = null!;
        public bool es_eliminado { get; set; }

        // =========================
        // AUDITORÍA
        // =========================
        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificado_desde_ip { get; set; }

        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        // =========================
        // CONCURRENCIA
        // =========================
        public byte[] row_version { get; set; } = null!;

        // =========================
        // INTEGRACIÓN / ORIGEN
        // =========================
        public string origen_registro { get; set; } = null!;
    }
}