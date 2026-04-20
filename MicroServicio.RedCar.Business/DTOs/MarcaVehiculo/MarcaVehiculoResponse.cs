using System;

namespace MicroServicio.RedCar.Business.DTOs.MarcaVehiculo
{
    public class MarcaVehiculoResponse
    {
        public int id_marca_vehiculo { get; set; }
        public Guid marca_vehiculo_guid { get; set; }

        public string codigo_marca_vehiculo { get; set; } = null!;
        public string nombre_marca_vehiculo { get; set; } = null!;
        public string? descripcion_marca_vehiculo { get; set; }

        public string estado_marca_vehiculo { get; set; } = null!;
        public bool es_eliminado { get; set; }

        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificado_desde_ip { get; set; }

        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        public string origen_registro { get; set; } = null!;
    }
}