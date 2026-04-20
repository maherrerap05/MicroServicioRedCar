using System;

namespace MicroServicio.RedCar.Business.DTOs.Cliente
{
    public class CrearClienteRequest
    {
        public string tipo_identificacion { get; set; } = null!;
        public string numero_identificacion { get; set; } = null!;
        public string? razon_social { get; set; }

        public string nombres { get; set; } = null!;
        public string? apellidos { get; set; }

        public string correo { get; set; } = null!;
        public string telefono { get; set; } = null!;
        public string direccion { get; set; } = null!;

        public string estado { get; set; } = "ACT";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;
    }
}