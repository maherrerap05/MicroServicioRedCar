namespace MicroServicio.RedCar.DataManagement.Models
{
    public class FacturaFiltroDataModel
    {
        public string? numero_factura { get; set; }

        public int? id_cliente { get; set; }
        public int? id_reserva { get; set; }

        public string? estado { get; set; }
        public string? origen_canal_factura { get; set; }

        // =========================
        // RANGO DE FECHAS
        // =========================
        public DateTime? fecha_emision_desde { get; set; }
        public DateTime? fecha_emision_hasta { get; set; }

        // =========================
        // RANGO DE VALORES
        // =========================
        public decimal? total_min { get; set; }
        public decimal? total_max { get; set; }

        // =========================
        // PAGINACIÓN
        // =========================
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}