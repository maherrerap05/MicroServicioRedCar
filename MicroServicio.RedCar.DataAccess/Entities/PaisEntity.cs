using System;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class PaisEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_pais { get; set; }

        // =========================
        // IDENTIFICACIÓN TÉCNICA
        // =========================
        public Guid pais_guid { get; set; }

        // =========================
        // CAMPOS PRINCIPALES
        // =========================
        public string nombre_pais { get; set; } = null!;

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado_pais { get; set; } = null!;
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

        // =========================
        // CONCURRENCIA
        // =========================
        public byte[] row_version { get; set; } = null!;

        // =========================
        // INTEGRACIÓN / ORIGEN
        // =========================
        public string origen_registro { get; set; } = null!;

        // =========================
        // CAMPOS OPCIONALES
        // =========================
        public string? motivo_inhabilitacion { get; set; }
    }
}