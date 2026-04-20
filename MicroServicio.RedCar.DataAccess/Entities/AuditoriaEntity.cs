using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class AuditoriaEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public long id_auditoria { get; set; }

        // =========================
        // IDENTIFICACIÓN DEL EVENTO
        // =========================
        public Guid auditoria_guid { get; set; }

        public string tabla_afectada { get; set; } = null!;
        public string operacion { get; set; } = null!; // INSERT, UPDATE, DELETE

        public string? id_registro_afectado { get; set; }

        // =========================
        // DATOS DEL CAMBIO
        // =========================
        public string? datos_anteriores { get; set; }
        public string? datos_nuevos { get; set; }

        // =========================
        // CONTEXTO DEL EVENTO
        // =========================
        public string usuario_ejecutor { get; set; } = null!;
        public string? ip_origen { get; set; }

        public DateTime fecha_evento_utc { get; set; }

        // =========================
        // ESTADO
        // =========================
        public bool activo { get; set; }

        // =========================
        // CONCURRENCIA
        // =========================
        public byte[] row_version { get; set; } = null!;
    }
}