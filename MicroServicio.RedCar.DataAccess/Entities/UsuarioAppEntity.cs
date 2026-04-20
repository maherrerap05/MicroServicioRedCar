using System;
using System.Collections.Generic;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class UsuarioAppEntity
    {
        public int id_usuario { get; set; }
        public Guid usuario_guid { get; set; }

        public string username { get; set; } = null!;
        public string correo { get; set; } = null!;

        public string password_hash { get; set; } = null!;
        public string password_salt { get; set; } = null!;

        public string estado_usuario { get; set; } = null!;
        public bool es_eliminado { get; set; }
        public bool activo { get; set; }

        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }

        public byte[] row_version { get; set; } = null!;

        public int id_cliente { get; set; }

        // RELACIONES
        public ICollection<UsuarioRolEntity> UsuarioRoles { get; set; } = new List<UsuarioRolEntity>();
    }
}