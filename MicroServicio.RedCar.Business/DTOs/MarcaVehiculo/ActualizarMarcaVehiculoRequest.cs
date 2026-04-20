using System;

namespace MicroServicio.RedCar.Business.DTOs.MarcaVehiculo
{
    public class ActualizarMarcaVehiculoRequest
    {
        public int id_marca_vehiculo { get; set; }

        public string codigo_marca_vehiculo { get; set; } = null!;
        public string nombre_marca_vehiculo { get; set; } = null!;
        public string? descripcion_marca_vehiculo { get; set; }

        public string estado_marca_vehiculo { get; set; } = null!;

        public string modificado_por_usuario { get; set; } = null!;
        public string? modificado_desde_ip { get; set; }
        public string origen_registro { get; set; } = null!;

        public string? motivo_inhabilitacion { get; set; }
    }
}