using System;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class ClienteEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_cliente { get; set; }

        // =========================
        // IDENTIFICACIÓN TÉCNICA
        // =========================
        public Guid cliente_guid { get; set; } = Guid.NewGuid();

        // =========================
        // IDENTIFICACIÓN DEL CLIENTE
        // =========================
        public string tipo_identificacion { get; set; } = null!;
        public string numero_identificacion { get; set; } = null!;

        // =========================
        // DATOS PERSONALES / FISCALES
        // =========================
        public string nombres { get; set; } = null!;
        public string? apellidos { get; set; }
        public string? razon_social { get; set; }

        // =========================
        // DATOS DE CONTACTO
        // =========================
        public string correo { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public string direccion { get; set; } = null!;

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado { get; set; } = null!;
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
        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        // =========================
        // CONCURRENCIA
        // =========================
        
    }
}