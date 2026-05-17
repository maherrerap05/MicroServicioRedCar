using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class ReservaExtraConfiguration : IEntityTypeConfiguration<ReservaExtraEntity>
    {
        public void Configure(EntityTypeBuilder<ReservaExtraEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("res_x_xtras", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_ESTADO", "estado_reserva_extra IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_CANTIDAD", "cantidad >= 1");
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_VALOR_UNITARIO", "valor_unitario_extra >= 0");
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_SUBTOTAL", "subtotal_extra >= 0");

                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...)) en PostgreSQL
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_CREADO_POR_NO_VACIO", "LENGTH(TRIM(creado_por_usuario)) > 0");
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_ORIGEN_REGISTRO_NO_VACIO", "LENGTH(TRIM(origen_registro)) > 0");
                tb.HasCheckConstraint("CHK_RES_X_XTRAS_SUBTOTAL_COHERENTE", "subtotal_extra = (cantidad * valor_unitario_extra)");

                tb.HasCheckConstraint(
                    "CHK_RES_X_XTRAS_INHABILITACION_FECHA_COHERENTE",
                    "((es_eliminado = true AND fecha_inhabilitacion_utc IS NOT NULL) OR (es_eliminado = false AND fecha_inhabilitacion_utc IS NULL))"
                // es_eliminado = 1/0 → true/false
                );

                tb.HasCheckConstraint(
                    "CHK_RES_X_XTRAS_INHABILITACION_MOTIVO_COHERENTE",
                    "((es_eliminado = true AND motivo_inhabilitacion IS NOT NULL AND LENGTH(TRIM(motivo_inhabilitacion)) > 0) OR (es_eliminado = false AND motivo_inhabilitacion IS NULL))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...)), 1/0 → true/false
                );

                tb.HasCheckConstraint(
                    "CHK_RES_X_XTRAS_MODIFICACION_USUARIO_COHERENTE",
                    "((fecha_modificacion_utc IS NULL AND modificado_por_usuario IS NULL) OR (fecha_modificacion_utc IS NOT NULL AND modificado_por_usuario IS NOT NULL AND LENGTH(TRIM(modificado_por_usuario)) > 0))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...))
                );
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(rx => rx.id_reserva_extra)
                   .HasName("PK_RES_X_XTRAS");

            builder.Property(rx => rx.id_reserva_extra)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(rx => rx.reserva_extra_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(rx => rx.id_reserva)
                   .IsRequired();

            builder.Property(rx => rx.id_extra)
                   .IsRequired();

            builder.Property(rx => rx.cantidad)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(rx => rx.valor_unitario_extra)
                   .IsRequired()
                   .HasColumnType("numeric(10,2)"); // decimal(10,2) → numeric(10,2)

            builder.Property(rx => rx.subtotal_extra)
                   .IsRequired()
                   .HasColumnType("numeric(10,2)"); // decimal(10,2) → numeric(10,2)

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(rx => rx.estado_reserva_extra)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(rx => rx.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(rx => rx.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(rx => rx.motivo_inhabilitacion)
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(rx => rx.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(rx => rx.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(rx => rx.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(rx => rx.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(rx => rx.modificado_desde_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado


            builder.Property(rx => rx.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(rx => rx.reserva_extra_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_RES_X_XTRAS_GUID");

            builder.HasIndex(rx => new { rx.id_reserva, rx.id_extra })
                   .IsUnique()
                   .HasDatabaseName("UQ_RES_X_XTRAS_RESERVA_EXTRA");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<ReservaEntity>()
                   .WithMany()
                   .HasForeignKey(rx => rx.id_reserva)
                   .HasConstraintName("FK_RES_X_XTRAS_RESERVAS")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ExtraEntity>()
                   .WithMany()
                   .HasForeignKey(rx => rx.id_extra)
                   .HasConstraintName("FK_RES_X_XTRAS_EXTRAS")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}