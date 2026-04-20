namespace MicroServicio.RedCar.Business.DTOs.Vehiculo
{
    public class VehiculoFiltroRequest
    {
        public string? codigo_interno_vehiculo { get; set; }
        public string? placa_vehiculo { get; set; }
        public string? modelo_vehiculo { get; set; }

        public short? anio_fabricacion { get; set; }

        public string? color_vehiculo { get; set; }
        public string? tipo_combustible { get; set; }
        public string? tipo_transmision { get; set; }

        public byte? capacidad_pasajeros { get; set; }
        public byte? capacidad_maletas { get; set; }
        public byte? numero_puertas { get; set; }

        public int? localizacion_actual { get; set; }

        public decimal? precio_base_dia_min { get; set; }
        public decimal? precio_base_dia_max { get; set; }

        public int? kilometraje_actual_min { get; set; }
        public int? kilometraje_actual_max { get; set; }

        public string? estado_vehiculo { get; set; }

        public int? id_marca_vehiculo { get; set; }
        public int? id_categoria_vehiculo { get; set; }

        public bool? aire_acondicionado { get; set; }

        public string? origen_registro { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}