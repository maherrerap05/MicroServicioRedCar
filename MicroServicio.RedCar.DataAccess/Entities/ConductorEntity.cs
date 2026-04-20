using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class ConductorEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_conductor { get; set; }

        // =========================
        // CAMPOS PRINCIPALES
        // =========================
        public Guid conductor_guid { get; set; }

        public string codigo_conductor { get; set; } = null!;

        public string tipo_identificacion { get; set; } = null!;
        public string numero_identificacion { get; set; } = null!;

        public string con_nombre1 { get; set; } = null!;
        public string? con_nombre2 { get; set; }

        public string con_apellido1 { get; set; } = null!;
        public string? con_apellido2 { get; set; }

        public string numero_licencia { get; set; } = null!;
        public DateTime fecha_vencimiento_licencia { get; set; }

        public byte edad_conductor { get; set; }

        public string con_telefono { get; set; } = null!;
        public string con_correo { get; set; } = null!;

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado_conductor { get; set; } = null!;
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