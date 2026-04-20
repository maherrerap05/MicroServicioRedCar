namespace MicroServicio.RedCar.DataManagement.Models
{
    public class FacturaDataModel
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_factura { get; set; }

        // =========================
        // IDENTIFICACIÓN
        // =========================
        public Guid guid_factura { get; set; }
        public string numero_factura { get; set; } = null!;

        // =========================
        // RELACIONES
        // =========================
        public int id_cliente { get; set; }
        public int id_reserva { get; set; }

        // =========================
        // FECHAS
        // =========================
        public DateTime fecha_emision { get; set; }

        // =========================
        // VALORES
        // =========================
        public decimal subtotal { get; set; }
        public decimal valor_iva { get; set; }
        public decimal total { get; set; }

        // =========================
        // OBSERVACIONES Y ORIGEN
        // =========================
        public string? observaciones_factura { get; set; }
        public string? origen_canal_factura { get; set; }

        // =========================
        // ESTADO
        // =========================
        public string estado { get; set; } = null!;
        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public bool es_eliminado { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        // =========================
        // AUDITORÍA
        // =========================
        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificacion_ip { get; set; }

        public string servicio_origen { get; set; } = null!;
        public byte[] row_version { get; set; } = null!;
    }
}