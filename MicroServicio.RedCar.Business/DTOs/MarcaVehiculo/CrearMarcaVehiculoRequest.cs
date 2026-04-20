using System;

namespace MicroServicio.RedCar.Business.DTOs.MarcaVehiculo
{
    public class CrearMarcaVehiculoRequest
    {
        public string codigo_marca_vehiculo { get; set; } = null!;
        public string nombre_marca_vehiculo { get; set; } = null!;
        public string? descripcion_marca_vehiculo { get; set; }

        public string estado_marca_vehiculo { get; set; } = "ACT";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificado_desde_ip { get; set; }
        public string origen_registro { get; set; } = null!;
    }
}