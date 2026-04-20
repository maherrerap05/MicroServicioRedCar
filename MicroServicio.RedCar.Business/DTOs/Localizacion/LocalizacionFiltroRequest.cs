using System;

namespace MicroServicio.RedCar.Business.DTOs.Localizacion
{
    public class LocalizacionFiltroRequest
    {
        public string? codigo_localizacion { get; set; }
        public string? nombre_localizacion { get; set; }
        public string? direccion_localizacion { get; set; }

        public string? telefono_contacto { get; set; }
        public string? correo_contacto { get; set; }
        public string? horario_atencion { get; set; }

        public string? zona_horaria { get; set; }
        public int? id_ciudad { get; set; }

        public string? estado_localizacion { get; set; }
        public string? origen_registro { get; set; }

        public int page_number { get; set; } = 1;
        public int page_size { get; set; } = 10;
    }
}