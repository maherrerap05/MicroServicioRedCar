namespace MicroServicio.RedCar.DataManagement.Models
{
    public class ClienteFiltroDataModel
    {
        public string? tipo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public string? razon_social { get; set; }
        public string? nombres { get; set; }
        public string? apellidos { get; set; }
        public string? correo { get; set; }
        public string? telefono { get; set; }
        public string? estado { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}