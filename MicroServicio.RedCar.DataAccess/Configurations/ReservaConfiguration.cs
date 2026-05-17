using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<ReservaEntity>
    {
        public void Configure(EntityTypeBuilder<ReservaEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("reservas", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_RESERVAS_ESTADO", "estado_reserva IN ('PEN', 'CON', 'CAN', 'EXP', 'FIN', 'EMI')");
                tb.HasCheckConstraint("CHK_RESERVAS_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_RESERVAS_CANTIDAD_DIAS", "cantidad_dias_reserva >= 1");
                tb.HasCheckConstraint("CHK_RESERVAS_SUBTOTAL", "subtotal_reserva >= 0");
                tb.HasCheckConstraint("CHK_RESERVAS_IVA", "valor_iva >= 0");
                tb.HasCheckConstraint("CHK_RESERVAS_TOTAL_RESERVA", "total_reserva >= 0");
                tb.HasCheckConstraint("CHK_RESERVAS_FECHAS", "fecha_hora_devolucion > fecha_hora_recogida");

                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...)) en PostgreSQL
                tb.HasCheckConstraint("CHK_RESERVAS_CODIGO_NO_VACIO", "LENGTH(TRIM(codigo_reserva)) > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_CREADO_POR_NO_VACIO", "LENGTH(TRIM(creado_por_usuario)) > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_ORIGEN_CANAL_NO_VACIO", "LENGTH(TRIM(origen_canal_reserva)) > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_SERVICIO_ORIGEN_NO_VACIO", "LENGTH(TRIM(servicio_origen)) > 0");

                tb.HasCheckConstraint("CHK_RESERVAS_CANTIDAD_DIAS_MAYOR_CERO", "cantidad_dias_reserva > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_TOTAL_COHERENTE", "total_reserva = (subtotal_reserva + valor_iva)");

                tb.HasCheckConstraint("CHK_RESERVAS_FECHA_INICIO_FIN", "fecha_fin > fecha_inicio");
                tb.HasCheckConstraint("CHK_RESERVAS_FECHA_INICIO_COHERENTE", "fecha_inicio = fecha_hora_recogida");
                tb.HasCheckConstraint("CHK_RESERVAS_FECHA_FIN_COHERENTE", "fecha_fin = fecha_hora_devolucion");

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_CONFIRMACION_COHERENTE",
                    "((estado_reserva = 'CON' AND fecha_confirmacion_utc IS NOT NULL) OR (estado_reserva <> 'CON' AND fecha_confirmacion_utc IS NULL))"
                // Sin cambio — no usa funciones SQL Server
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_CANCELACION_MOTIVO_COHERENTE",
                    "((estado_reserva = 'CAN' AND motivo_cancelacion IS NOT NULL AND LENGTH(TRIM(motivo_cancelacion)) > 0) OR (estado_reserva <> 'CAN' AND motivo_cancelacion IS NULL))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...))
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_CANCELACION_FECHA_COHERENTE",
                    "((estado_reserva = 'CAN' AND fecha_cancelacion_utc IS NOT NULL) OR (estado_reserva <> 'CAN' AND fecha_cancelacion_utc IS NULL))"
                // Sin cambio
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_INHABILITACION_FECHA_COHERENTE",
                    "((es_eliminado = true AND fecha_inhabilitacion_utc IS NOT NULL) OR (es_eliminado = false AND fecha_inhabilitacion_utc IS NULL))"
                // es_eliminado = 1/0 → true/false
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_INHABILITACION_MOTIVO_COHERENTE",
                    "((es_eliminado = true AND motivo_inhabilitacion IS NOT NULL AND LENGTH(TRIM(motivo_inhabilitacion)) > 0) OR (es_eliminado = false AND motivo_inhabilitacion IS NULL))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...)), 1/0 → true/false
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_MODIFICACION_USUARIO_COHERENTE",
                    "((fecha_modificacion_utc IS NULL AND modificado_por_usuario IS NULL) OR (fecha_modificacion_utc IS NOT NULL AND modificado_por_usuario IS NOT NULL AND LENGTH(TRIM(modificado_por_usuario)) > 0))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...))
                );
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(r => r.id_reserva)
                   .HasName("PK_RESERVAS");

            builder.Property(r => r.id_reserva)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN TÉCNICA
            // =========================
            builder.Property(r => r.guid_reserva)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(r => r.codigo_reserva)
                   .IsRequired()
                   .HasMaxLength(40);
            // IsUnicode(false) eliminado

            // =========================
            // RELACIONES PRINCIPALES
            // =========================
            builder.Property(r => r.id_cliente)
                   .IsRequired();

            builder.Property(r => r.id_vehiculo)
                   .IsRequired();

            builder.Property(r => r.id_localizacion_recogida)
                   .IsRequired();

            builder.Property(r => r.id_localizacion_devolucion)
                   .IsRequired();

            // =========================
            // FECHAS TRANSVERSALES
            // =========================
            builder.Property(r => r.fecha_reserva_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(r => r.fecha_inicio)
                   .IsRequired()
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(r => r.fecha_fin)
                   .IsRequired()
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            // =========================
            // FECHAS PROPIAS DEL DOMINIO
            // =========================
            builder.Property(r => r.fecha_recogida)
                   .IsRequired()
                   .HasColumnType("date"); // date es igual en PostgreSQL

            builder.Property(r => r.hora_recogida)
                   .IsRequired()
                   .HasColumnType("time(0)"); // time(0) es igual en PostgreSQL

            builder.Property(r => r.fecha_devolucion)
                   .IsRequired()
                   .HasColumnType("date"); // date es igual en PostgreSQL

            builder.Property(r => r.hora_devolucion)
                   .IsRequired()
                   .HasColumnType("time(0)"); // time(0) es igual en PostgreSQL

            builder.Property(r => r.fecha_hora_recogida)
                   .IsRequired()
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(r => r.fecha_hora_devolucion)
                   .IsRequired()
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            // =========================
            // DATOS PROPIOS DEL DOMINIO
            // =========================
            builder.Property(r => r.cantidad_dias_reserva)
                   .IsRequired();

            builder.Property(r => r.subtotal_reserva)
                   .IsRequired()
                   .HasColumnType("numeric(12,2)"); // decimal(12,2) → numeric(12,2)

            builder.Property(r => r.valor_iva)
                   .IsRequired()
                   .HasColumnType("numeric(12,2)"); // decimal(12,2) → numeric(12,2)

            builder.Property(r => r.total_reserva)
                   .IsRequired()
                   .HasColumnType("numeric(12,2)"); // decimal(12,2) → numeric(12,2)

            builder.Property(r => r.observaciones_reserva)
                   .HasMaxLength(300);
            // IsUnicode(false) eliminado

            builder.Property(r => r.origen_canal_reserva)
                   .IsRequired()
                   .HasMaxLength(50);
            // IsUnicode(false) eliminado

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(r => r.estado_reserva)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("PEN");
            // IsUnicode(false) eliminado

            builder.Property(r => r.fecha_confirmacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(r => r.fecha_cancelacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(r => r.motivo_cancelacion)
                   .HasMaxLength(250);
            // IsUnicode(false) eliminado

            builder.Property(r => r.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(r => r.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(r => r.motivo_inhabilitacion)
                   .HasMaxLength(250);
            // IsUnicode(false) eliminado

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(r => r.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(r => r.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(r => r.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(r => r.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(r => r.modificacion_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(r => r.servicio_origen)
                   .IsRequired()
                   .HasMaxLength(50);
            // IsUnicode(false) eliminado


            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(r => r.guid_reserva)
                   .IsUnique()
                   .HasDatabaseName("UQ_RESERVAS_GUID");

            builder.HasIndex(r => r.codigo_reserva)
                   .IsUnique()
                   .HasDatabaseName("UQ_RESERVAS_CODIGO");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<ClienteEntity>()
                   .WithMany()
                   .HasForeignKey(r => r.id_cliente)
                   .HasConstraintName("FK_RESERVAS_CLIENTES")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<VehiculoEntity>()
                   .WithMany()
                   .HasForeignKey(r => r.id_vehiculo)
                   .HasConstraintName("FK_RESERVAS_VEHICULOS")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<LocalizacionEntity>()
                   .WithMany()
                   .HasForeignKey(r => r.id_localizacion_recogida)
                   .HasConstraintName("FK_RESERVAS_LOCALIZACION_RECOGIDA")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<LocalizacionEntity>()
                   .WithMany()
                   .HasForeignKey(r => r.id_localizacion_devolucion)
                   .HasConstraintName("FK_RESERVAS_LOCALIZACION_DEVOLUCION")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}