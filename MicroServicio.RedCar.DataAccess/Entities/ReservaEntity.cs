using System;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class ReservaEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_reserva { get; set; }

        // =========================
        // IDENTIFICACIÓN TÉCNICA
        // =========================
        public Guid guid_reserva { get; set; }

        public string codigo_reserva { get; set; } = null!;

        // =========================
        // RELACIONES PRINCIPALES
        // =========================
        public int id_cliente { get; set; }
        public int id_vehiculo { get; set; }
        public int id_localizacion_recogida { get; set; }
        public int id_localizacion_devolucion { get; set; }

        // =========================
        // FECHAS TRANSVERSALES
        // =========================
        public DateTime fecha_reserva_utc { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }

        // =========================
        // FECHAS PROPIAS DEL DOMINIO
        // =========================
        public DateTime fecha_recogida { get; set; }
        public TimeSpan hora_recogida { get; set; }

        public DateTime fecha_devolucion { get; set; }
        public TimeSpan hora_devolucion { get; set; }

        public DateTime fecha_hora_recogida { get; set; }
        public DateTime fecha_hora_devolucion { get; set; }

        // =========================
        // DATOS PROPIOS DEL DOMINIO
        // =========================
        public int cantidad_dias_reserva { get; set; }

        public decimal subtotal_reserva { get; set; }
        public decimal valor_iva { get; set; }
        public decimal total_reserva { get; set; }

        public string? observaciones_reserva { get; set; }
        public string origen_canal_reserva { get; set; } = null!;

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado_reserva { get; set; } = null!;

        public DateTime? fecha_confirmacion_utc { get; set; }
        public DateTime? fecha_cancelacion_utc { get; set; }
        public string? motivo_cancelacion { get; set; }

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
        public string? modificacion_ip { get; set; }

        // =========================
        // INTEGRACIÓN / ORIGEN
        // =========================
        public string servicio_origen { get; set; } = null!;

        // =========================
        // CONCURRENCIA
        // =========================
        public byte[] row_version { get; set; } = null!;
    }
}