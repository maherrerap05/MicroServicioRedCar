using System;

namespace MicroServicio.RedCar.Business.DTOs.Cliente
{
    public class ClienteResponse
    {
        public int id_cliente { get; set; }
        public Guid cliente_guid { get; set; }

        public string tipo_identificacion { get; set; } = null!;
        public string numero_identificacion { get; set; } = null!;
        public string? razon_social { get; set; }

        public string nombres { get; set; } = null!;
        public string? apellidos { get; set; }

        public string correo { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public string direccion { get; set; } = null!;

        public string estado { get; set; } = null!;
        public bool es_eliminado { get; set; }

        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }

        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? modificacion_ip { get; set; }

        public string servicio_origen { get; set; } = null!;
        public string? motivo_inhabilitacion { get; set; }
    }
}