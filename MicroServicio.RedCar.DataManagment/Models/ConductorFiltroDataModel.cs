namespace MicroServicio.RedCar.DataManagement.Models
{
    public class ConductorFiltroDataModel
    {
        public string? codigo_conductor { get; set; }
        public string? numero_identificacion { get; set; }
        public string? numero_licencia { get; set; }
        public string? con_nombre1 { get; set; }
        public string? con_apellido1 { get; set; }
        public string? con_correo { get; set; }
        public string? estado_conductor { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}