using System;

namespace MicroServicio.RedCar.Business.DTOs.Factura
{
    public class AprobarFacturaRequest
    {
        public int id_factura { get; set; }

        public string modificado_por_usuario { get; set; } = null!;
        public string? modificacion_ip { get; set; }
        public string servicio_origen { get; set; } = null!;
    }
}