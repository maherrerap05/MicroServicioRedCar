using System;

namespace MicroServicio.RedCar.Business.DTOs.Conductor
{
    public class ConductorFiltroRequest
    {
        public string? codigo_conductor { get; set; }

        public string? tipo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }

        public string? con_nombre1 { get; set; }
        public string? con_nombre2 { get; set; }

        public string? con_apellido1 { get; set; }
        public string? con_apellido2 { get; set; }

        public string? numero_licencia { get; set; }

        public DateTime? fecha_vencimiento_licencia_desde { get; set; }
        public DateTime? fecha_vencimiento_licencia_hasta { get; set; }

        public byte? edad_conductor { get; set; }

        public string? con_telefono { get; set; }
        public string? con_correo { get; set; }

        public string? estado_conductor { get; set; }
        public string? origen_registro { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}