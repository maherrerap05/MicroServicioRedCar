namespace MicroServicio.RedCar.DataManagement.Models
{
    public class VehiculoFiltroDataModel
    {
        public string? codigo_interno_vehiculo { get; set; }
        public string? placa_vehiculo { get; set; }
        public string? modelo_vehiculo { get; set; }

        public string? tipo_combustible { get; set; }
        public string? tipo_transmision { get; set; }

        public int? id_marca_vehiculo { get; set; }
        public int? id_categoria_vehiculo { get; set; }
        public int? localizacion_actual { get; set; }

        public string? estado_vehiculo { get; set; }

        // Rango de precio (muy importante para búsquedas tipo Booking)
        public decimal? precio_min { get; set; }
        public decimal? precio_max { get; set; }

        // =========================
        // PAGINACIÓN
        // =========================
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}