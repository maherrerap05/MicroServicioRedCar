using System;

namespace MicroServicio.RedCar.DataAccess.Entities
{
    public class VehiculoEntity
    {
        // =========================
        // CLAVE PRIMARIA
        // =========================
        public int id_vehiculo { get; set; }

        // =========================
        // CAMPOS PRINCIPALES
        // =========================
        public Guid vehiculo_guid { get; set; } = Guid.NewGuid();

        public string codigo_interno_vehiculo { get; set; } = null!;
        public string placa_vehiculo { get; set; } = null!;

        public string modelo_vehiculo { get; set; } = null!;
        public short anio_fabricacion { get; set; }

        public string color_vehiculo { get; set; } = null!;
        public string tipo_combustible { get; set; } = null!;
        public string tipo_transmision { get; set; } = null!;

        public byte capacidad_pasajeros { get; set; }
        public byte capacidad_maletas { get; set; }
        public byte numero_puertas { get; set; }

        public int localizacion_actual { get; set; }

        public decimal precio_base_dia { get; set; }
        public int kilometraje_actual { get; set; }

        public string? observaciones_generales { get; set; }
        public string? imagen_referencial_url { get; set; }

        public bool aire_acondicionado { get; set; }

        // =========================
        // CLAVES FORÁNEAS NUEVAS
        // =========================
        public int id_marca_vehiculo { get; set; }
        public int id_categoria_vehiculo { get; set; }

        // =========================
        // ESTADO / CICLO DE VIDA
        // =========================
        public string estado_vehiculo { get; set; } = null!;
        public bool es_eliminado { get; set; }

        // =========================
        // INTEGRACIÓN / ORIGEN
        // =========================
        public string origen_registro { get; set; } = null!;

        // =========================
        // AUDITORÍA
        // =========================
        public DateTime fecha_registro_utc { get; set; }
        public string creado_por_usuario { get; set; } = null!;

        public string? modificado_por_usuario { get; set; }
        public DateTime? fecha_modificacion_utc { get; set; }
        public string? modificado_desde_ip { get; set; }

        public DateTime? fecha_inhabilitacion_utc { get; set; }
        public string? motivo_inhabilitacion { get; set; }

        // =========================
        // CONCURRENCIA
        // =========================
        
    }
}