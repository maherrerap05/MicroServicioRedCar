using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class PaisConfiguration : IEntityTypeConfiguration<PaisEntity>
    {
        public void Configure(EntityTypeBuilder<PaisEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("paises", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_PAIS_ESTADO", "estado_pais IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_PAIS_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(p => p.id_pais)
                   .HasName("PK_PAISES");

            builder.Property(p => p.id_pais)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN TÉCNICA
            // =========================
            builder.Property(p => p.pais_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(p => p.nombre_pais)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(p => p.estado_pais)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(p => p.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(p => p.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(p => p.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(p => p.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(p => p.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(p => p.modificado_desde_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            builder.Property(p => p.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(p => p.motivo_inhabilitacion)
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado


            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(p => p.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(p => p.pais_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_PAIS_GUID");
        }
    }
}