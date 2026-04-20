namespace MicroServicio.RedCar.DataManagement.Models
{
    public class ReservaFiltroDataModel
    {
        public string? codigo_reserva { get; set; }

        public int? id_cliente { get; set; }
        public int? id_vehiculo { get; set; }

        public int? id_localizacion_recogida { get; set; }
        public int? id_localizacion_devolucion { get; set; }

        public string? estado_reserva { get; set; }
        public string? origen_canal_reserva { get; set; }

        // =========================
        // RANGOS DE FECHAS
        // =========================
        public DateTime? fecha_inicio_desde { get; set; }
        public DateTime? fecha_inicio_hasta { get; set; }

        public DateTime? fecha_fin_desde { get; set; }
        public DateTime? fecha_fin_hasta { get; set; }

        // =========================
        // RANGOS DE VALORES
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