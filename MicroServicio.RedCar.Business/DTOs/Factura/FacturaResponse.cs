using System;

namespace MicroServicio.RedCar.Business.DTOs.Factura
{
    public class FacturaResponse
    {
        public int id_factura { get; set; }
        public Guid guid_factura { get; set; }
        public string numero_factura { get; set; } = null!;

        public int id_cliente { get; set; }
        public int id_reserva { get; set; }

        public DateTime fecha_emision { get; set; }

        public decimal subtotal { get; set; }
        public decimal valor_iva { get; set; }
        public decimal total { get; set; }

        public string? observaciones_factura { get; set; }
        public string? origen_canal_factura { get; set; }

        public string estado { get; set; } = null!;
        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public bool es_eliminado { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificacion_ip { get; set; }

        public string servicio_origen { get; set; } = null!;
    }
}