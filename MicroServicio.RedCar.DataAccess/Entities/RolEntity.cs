using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class RolEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_rol { get; set; }

        // =========================
        // IDENTIFICACIÓN
        // =========================
        public Guid rol_guid { get; set; }

        public string nombre_rol { get; set; } = null!;
        public string? descripcion_rol { get; set; }

        // =========================
        // ESTADO
        // =========================
        public string estado_rol { get; set; } = null!; // ACT / INA
        public bool es_eliminado { get; set; }
        public bool activo { get; set; }

        // =========================
        // AUDITORÍA
        // =========================
        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }

        // =========================
        // CONCURRENCIA
        // =========================
        public byte[] row_version { get; set; } = null!;

        // =========================
        // RELACIONES
        // =========================
        public ICollection<UsuarioRolEntity> UsuarioRoles { get; set; } = new List<UsuarioRolEntity>();
    }
}