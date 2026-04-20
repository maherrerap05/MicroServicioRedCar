namespace MicroServicio.RedCar.Business.DTOs.Cliente
{
    public class ClienteFiltroRequest
    {
        public string? tipo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public string? razon_social { get; set; }

        public string? nombres { get; set; }
        public string? apellidos { get; set; }

        public string? correo { get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }

        public string? estado { get; set; }
        public string? servicio_origen { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}