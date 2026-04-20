namespace MicroServicio.RedCar.DataManagement.Models
{
    public class ExtraFiltroDataModel
    {
        public string? codigo_extra { get; set; }
        public string? nombre_extra { get; set; }
        public string? estado_extra { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}