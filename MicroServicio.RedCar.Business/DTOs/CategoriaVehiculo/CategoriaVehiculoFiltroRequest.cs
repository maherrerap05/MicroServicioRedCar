using System;

namespace MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo
{
    public class CategoriaVehiculoFiltroRequest
    {
        public string? codigo_categoria_vehiculo { get; set; }
        public string? nombre_categoria_vehiculo { get; set; }
        public string? descripcion_categoria_vehiculo { get; set; }

        public string? estado_categoria_vehiculo { get; set; }
        public string? origen_registro { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}