using System;

namespace MicroServicio.RedCar.Business.DTOs.MarcaVehiculo
{
    public class MarcaVehiculoFiltroRequest
    {
        public string? codigo_marca_vehiculo { get; set; }
        public string? nombre_marca_vehiculo { get; set; }
        public string? descripcion_marca_vehiculo { get; set; }

        public string? estado_marca_vehiculo { get; set; }
        public string? origen_registro { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}