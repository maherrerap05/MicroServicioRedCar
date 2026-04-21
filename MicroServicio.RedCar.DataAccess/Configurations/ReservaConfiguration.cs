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
            builder.ToTable("RESERVAS", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_RESERVAS_ESTADO", "estado_reserva IN ('PEN', 'CON', 'CAN', 'EXP', 'FIN', 'EMI')");
                tb.HasCheckConstraint("CHK_RESERVAS_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_RESERVAS_CANTIDAD_DIAS", "cantidad_dias_reserva >= 1");
                tb.HasCheckConstraint("CHK_RESERVAS_SUBTOTAL", "subtotal_reserva >= 0");
                tb.HasCheckConstraint("CHK_RESERVAS_IVA", "valor_iva >= 0");
                tb.HasCheckConstraint("CHK_RESERVAS_TOTAL_RESERVA", "total_reserva >= 0");
                tb.HasCheckConstraint("CHK_RESERVAS_FECHAS", "fecha_hora_devolucion > fecha_hora_recogida");

                // Nuevos constraints agregados
                tb.HasCheckConstraint("CHK_RESERVAS_CODIGO_NO_VACIO", "LEN(LTRIM(RTRIM(codigo_reserva))) > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_CREADO_POR_NO_VACIO", "LEN(LTRIM(RTRIM(creado_por_usuario))) > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_ORIGEN_CANAL_NO_VACIO", "LEN(LTRIM(RTRIM(origen_canal_reserva))) > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_SERVICIO_ORIGEN_NO_VACIO", "LEN(LTRIM(RTRIM(servicio_origen))) > 0");

                tb.HasCheckConstraint("CHK_RESERVAS_CANTIDAD_DIAS_MAYOR_CERO", "cantidad_dias_reserva > 0");
                tb.HasCheckConstraint("CHK_RESERVAS_TOTAL_COHERENTE", "total_reserva = (subtotal_reserva + valor_iva)");

                tb.HasCheckConstraint("CHK_RESERVAS_FECHA_INICIO_FIN", "fecha_fin > fecha_inicio");
                tb.HasCheckConstraint("CHK_RESERVAS_FECHA_INICIO_COHERENTE", "fecha_inicio = fecha_hora_recogida");
                tb.HasCheckConstraint("CHK_RESERVAS_FECHA_FIN_COHERENTE", "fecha_fin = fecha_hora_devolucion");

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_CONFIRMACION_COHERENTE",
                    "((estado_reserva = 'CON' AND fecha_confirmacion_utc IS NOT NULL) OR (estado_reserva <> 'CON' AND fecha_confirmacion_utc IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_CANCELACION_MOTIVO_COHERENTE",
                    "((estado_reserva = 'CAN' AND motivo_cancelacion IS NOT NULL AND LEN(LTRIM(RTRIM(motivo_cancelacion))) > 0) OR (estado_reserva <> 'CAN' AND motivo_cancelacion IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_CANCELACION_FECHA_COHERENTE",
                    "((estado_reserva = 'CAN' AND fecha_cancelacion_utc IS NOT NULL) OR (estado_reserva <> 'CAN' AND fecha_cancelacion_utc IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_INHABILITACION_FECHA_COHERENTE",
                    "((es_eliminado = 1 AND fecha_inhabilitacion_utc IS NOT NULL) OR (es_eliminado = 0 AND fecha_inhabilitacion_utc IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_INHABILITACION_MOTIVO_COHERENTE",
                    "((es_eliminado = 1 AND motivo_inhabilitacion IS NOT NULL AND LEN(LTRIM(RTRIM(motivo_inhabilitacion))) > 0) OR (es_eliminado = 0 AND motivo_inhabilitacion IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_RESERVAS_MODIFICACION_USUARIO_COHERENTE",
                    "((fecha_modificacion_utc IS NULL AND modificado_por_usuario IS NULL) OR (fecha_modificacion_utc IS NOT NULL AND modificado_por_usuario IS NOT NULL AND LEN(LTRIM(RTRIM(modificado_por_usuario))) > 0))"
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
                   .HasDefaultValueSql("NEWID()");

            builder.Property(r => r.codigo_reserva)
                   .IsRequired()
                   .HasMaxLength(40)
                   .IsUnicode(false);

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
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(r => r.fecha_inicio)
                   .IsRequired()
                   .HasColumnType("datetime2(0)");

            builder.Property(r => r.fecha_fin)
                   .IsRequired()
                   .HasColumnType("datetime2(0)");

            // =========================
            // FECHAS PROPIAS DEL DOMINIO
            // =========================
            builder.Property(r => r.fecha_recogida)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(r => r.hora_recogida)
                   .IsRequired()
                   .HasColumnType("time(0)");

            builder.Property(r => r.fecha_devolucion)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(r => r.hora_devolucion)
                   .IsRequired()
                   .HasColumnType("time(0)");

            builder.Property(r => r.fecha_hora_recogida)
                   .IsRequired()
                   .HasColumnType("datetime2(0)");

            builder.Property(r => r.fecha_hora_devolucion)
                   .IsRequired()
                   .HasColumnType("datetime2(0)");

            // =========================
            // DATOS PROPIOS DEL DOMINIO
            // =========================

            builder.Property(r => r.cantidad_dias_reserva)
                   .IsRequired();

            builder.Property(r => r.subtotal_reserva)
                   .IsRequired()
                   .HasColumnType("decimal(12,2)");

            builder.Property(r => r.valor_iva)
                   .IsRequired()
                   .HasColumnType("decimal(12,2)");

            builder.Property(r => r.total_reserva)
                   .IsRequired()
                   .HasColumnType("decimal(12,2)");

            builder.Property(r => r.observaciones_reserva)
                   .HasMaxLength(300)
                   .IsUnicode(false);

            builder.Property(r => r.origen_canal_reserva)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(r => r.estado_reserva)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("PEN");

            builder.Property(r => r.fecha_confirmacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(r => r.fecha_cancelacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(r => r.motivo_cancelacion)
                   .HasMaxLength(250)
                   .IsUnicode(false);

            builder.Property(r => r.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(r => r.fecha_inhabilitacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(r => r.motivo_inhabilitacion)
                   .HasMaxLength(250)
                   .IsUnicode(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(r => r.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(r => r.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(r => r.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(r => r.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(r => r.modificacion_ip)
                   .HasMaxLength(45)
                   .IsUnicode(false);

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(r => r.servicio_origen)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(r => r.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

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