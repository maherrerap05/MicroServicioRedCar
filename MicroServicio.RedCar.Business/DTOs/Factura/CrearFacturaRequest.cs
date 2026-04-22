namespace MicroServicio.RedCar.Business.DTOs.Factura
{
    public class CrearFacturaRequest
    {
        public string numero_factura { get; set; } = null!;

        // id_cliente se elimina: se deriva automáticamente de la reserva en FacturaService
        public int id_reserva { get; set; }

        public string? observaciones_factura { get; set; }
        public string? origen_canal_factura { get; set; }

        public string estado { get; set; } = "ABI";

        public string creado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;
    }
}