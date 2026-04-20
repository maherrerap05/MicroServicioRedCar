using System;

namespace MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo
{
    public class CrearCategoriaVehiculoRequest
    {
        public string codigo_categoria_vehiculo { get; set; } = null!;
        public string nombre_categoria_vehiculo { get; set; } = null!;
        public string? descripcion_categoria_vehiculo { get; set; }

        public string estado_categoria_vehiculo { get; set; } = "ACT";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificado_desde_ip { get; set; }
        public string origen_registro { get; set; } = null!;
    }
}