using System;

namespace MicroServicio.RedCar.Business.DTOs.Localizacion
{
    public class CrearLocalizacionRequest
    {
        public string codigo_localizacion { get; set; } = null!;
        public string nombre_localizacion { get; set; } = null!;
        public string direccion_localizacion { get; set; } = null!;

        public string telefono_contacto { get; set; } = null!;
        public string correo_contacto { get; set; } = null!;
        public string horario_atencion { get; set; } = null!;

        public string zona_horaria { get; set; } = null!;
        public int id_ciudad { get; set; }

        public string estado_localizacion { get; set; } = "ACT";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificado_desde_ip { get; set; }
        public string origen_registro { get; set; } = null!;
    }
}