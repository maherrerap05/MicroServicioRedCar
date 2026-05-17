using System;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class FacturaEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_factura { get; set; }

        // =========================
        // IDENTIFICACIÓN TÉCNICA
        // =========================
        public Guid guid_factura { get; set; } = Guid.NewGuid();

        // =========================
        // RELACIONES PRINCIPALES
        // =========================
        public int id_cliente { get; set; }
        public int id_reserva { get; set; }

        // =========================
        // DATOS DE FACTURACIÓN
        // =========================
        public string numero_factura { get; set; } = null!;
        public DateTime fecha_emision { get; set; }

        public decimal subtotal { get; set; }
        public decimal valor_iva { get; set; }
        public decimal total { get; set; }

        public string? observaciones_factura { get; set; }
        public string? origen_canal_factura { get; set; }

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado { get; set; } = null!;
        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public bool es_eliminado { get; set; }

        // =========================
        // AUDITORÍA
        // =========================
        public string creado_por_usuario { get; set; } = null!;
        public DateTime fecha_registro_utc { get; set; }

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificacion_ip { get; set; }

        // =========================
        // INTEGRACIÓN / ORIGEN
        // =========================
        public string servicio_origen { get; set; } = null!;

        // =========================
        // CAMPOS OPCIONALES EXTENDIDOS
        // =========================
        public string? motivo_inhabilitacion { get; set; }

        // =========================
        // CONCURRENCIA
        // =========================
        
    }
}