namespace MicroServicio.RedCar.DataManagement.Models
{
    public class MarcaVehiculoFiltroDataModel
    {
        public string? codigo_marca_vehiculo { get; set; }
        public string? nombre_marca_vehiculo { get; set; }
        public string? estado_marca_vehiculo { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}