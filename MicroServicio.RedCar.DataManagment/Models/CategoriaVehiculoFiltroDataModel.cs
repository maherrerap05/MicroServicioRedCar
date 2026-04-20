namespace MicroServicio.RedCar.DataManagement.Models
{
    public class CategoriaVehiculoFiltroDataModel
    {
        public string? codigo_categoria_vehiculo { get; set; }
        public string? nombre_categoria_vehiculo { get; set; }
        public string? estado_categoria_vehiculo { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}