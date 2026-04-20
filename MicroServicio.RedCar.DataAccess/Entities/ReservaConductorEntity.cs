using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class ReservaConductorEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_reserva_conductor { get; set; }

        // =========================
        // CAMPOS PRINCIPALES
        // =========================
        public Guid reserva_conductor_guid { get; set; }

        public int id_reserva { get; set; }
        public int id_conductor { get; set; }

        public string tipo_conductor { get; set; } = null!;
        public bool es_principal { get; set; }

        public DateTime fecha_asignacion_utc { get; set; }

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado_reserva_conductor { get; set; } = null!;
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