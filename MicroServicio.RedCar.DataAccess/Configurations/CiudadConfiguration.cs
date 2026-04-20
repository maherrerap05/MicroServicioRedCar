using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class CiudadConfiguration : IEntityTypeConfiguration<CiudadEntity>
    {
        public void Configure(EntityTypeBuilder<CiudadEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("CIUDADES", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_CIUDADES_ESTADO", "estado_ciudad IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_CIUDADES_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(c => c.id_ciudad)
                   .HasName("PK_CIUDADES");

            builder.Property(c => c.id_ciudad)
                   .ValueGeneratedOnAdd();

            // =========================
            // CLAVE FORÁNEA
            // =========================
            builder.Property(c => c.id_pais)
                   .IsRequired();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(c => c.ciudad_guid)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.nombre_ciudad)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(c => c.estado_ciudad)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("ACT");

            builder.Property(c => c.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(c => c.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(c => c.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(c => c.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(c => c.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(c => c.modificado_desde_ip)
                   .HasMaxLength(45)
                   .IsUnicode(false);

            builder.Property(c => c.fecha_inhabilitacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(c => c.motivo_inhabilitacion)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(c => c.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(c => c.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(c => c.ciudad_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_CIUDADES_GUID");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<PaisEntity>()
                   .WithMany()
                   .HasForeignKey(c => c.id_pais)
                   .HasConstraintName("FK_CIUDADES_PAISES")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}