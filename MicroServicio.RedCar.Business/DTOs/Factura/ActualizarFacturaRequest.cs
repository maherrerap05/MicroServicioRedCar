namespace MicroServicio.RedCar.Business.DTOs.Factura
{
    public class ActualizarFacturaRequest
    {
        public int id_factura { get; set; }

        public string numero_factura { get; set; } = null!;

        // id_cliente eliminado: se deriva automáticamente de la reserva en FacturaService
        public int id_reserva { get; set; }

        public string? observaciones_factura { get; set; }
        public string? origen_canal_factura { get; set; }

        public string estado { get; set; } = null!;

        public string modificado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;

        public string? motivo_inhabilitacion { get; set; }
    }
}