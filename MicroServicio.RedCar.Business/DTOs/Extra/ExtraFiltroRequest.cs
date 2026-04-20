using System;

namespace MicroServicio.RedCar.Business.DTOs.Extra
{
    public class ExtraFiltroRequest
    {
        public string? codigo_extra { get; set; }
        public string? nombre_extra { get; set; }
        public string? descripcion_extra { get; set; }

        public decimal? valor_fijo_desde { get; set; }
        public decimal? valor_fijo_hasta { get; set; }

        public string? estado_extra { get; set; }
        public string? origen_registro { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}