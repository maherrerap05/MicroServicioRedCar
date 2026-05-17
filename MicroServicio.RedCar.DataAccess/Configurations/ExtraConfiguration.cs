using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class ExtraConfiguration : IEntityTypeConfiguration<ExtraEntity>
    {
        public void Configure(EntityTypeBuilder<ExtraEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("extras", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_EXTRAS_ESTADO", "estado_extra IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_EXTRAS_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_EXTRAS_VALOR_FIJO", "valor_fijo > 0");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(e => e.id_extra)
                   .HasName("PK_EXTRAS");

            builder.Property(e => e.id_extra)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(e => e.extra_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(e => e.codigo_extra)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(e => e.nombre_extra)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(e => e.descripcion_extra)
                   .IsRequired()
                   .HasMaxLength(250);
            // IsUnicode(false) eliminado

            builder.Property(e => e.valor_fijo)
                   .IsRequired()
                   .HasColumnType("numeric(10,2)"); // decimal(10,2) → numeric(10,2)

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(e => e.estado_extra)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(e => e.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(e => e.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(e => e.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(e => e.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(e => e.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(e => e.modificado_desde_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            builder.Property(e => e.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(e => e.motivo_inhabilitacion)
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado


            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(e => e.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(e => e.extra_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_EXTRAS_GUID");

            builder.HasIndex(e => e.codigo_extra)
                   .IsUnique()
                   .HasDatabaseName("UQ_EXTRAS_CODIGO");
        }
    }
}