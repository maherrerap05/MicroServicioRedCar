namespace MicroServicio.RedCar.DataManagement.Models
{
    public class LocalizacionFiltroDataModel
    {
        public string? codigo_localizacion { get; set; }
        public string? nombre_localizacion { get; set; }
        public string? zona_horaria { get; set; }
        public string? estado_localizacion { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}