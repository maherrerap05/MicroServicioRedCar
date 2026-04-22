using System;

namespace MicroServicio.RedCar.Business.DTOs.Conductor
{
    public class ActualizarConductorRequest
    {
        public int? id_conductor { get; set; }

        public string? codigo_conductor { get; set; }

        public string? tipo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }

        public string? con_nombre1 { get; set; }
        public string? con_nombre2 { get; set; }

        public string? con_apellido1 { get; set; }
        public string? con_apellido2 { get; set; }

        public string? numero_licencia { get; set; }
        public DateTime? fecha_vencimiento_licencia { get; set; }

        public byte? edad_conductor { get; set; }

        public string? con_telefono { get; set; }
        public string? con_correo { get; set; }

        public string? estado_conductor { get; set; }

        public string? modificado_por_usuario { get; set; }
        public string? modificado_desde_ip { get; set; }
        public string? origen_registro { get; set; }

        public string? motivo_inhabilitacion { get; set; }
    }
}