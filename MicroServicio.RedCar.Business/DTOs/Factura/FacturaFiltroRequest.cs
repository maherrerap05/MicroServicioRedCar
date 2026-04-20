using System;

namespace MicroServicio.RedCar.Business.DTOs.Factura
{
    public class FacturaFiltroRequest
    {
        public string? numero_factura { get; set; }

        public int? id_cliente { get; set; }
        public int? id_reserva { get; set; }

        public DateTime? fecha_emision_desde { get; set; }
        public DateTime? fecha_emision_hasta { get; set; }

        public string? origen_canal_factura { get; set; }
        public string? estado { get; set; }
        public string? servicio_origen { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}