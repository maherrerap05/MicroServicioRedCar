using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<RolEntity>
    {
        public void Configure(EntityTypeBuilder<RolEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("ROL", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_ROLES_ESTADO", "estado_rol IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_ROLES_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_ROLES_ACTIVO", "activo IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(r => r.id_rol)
                   .HasName("PK_ROLES");

            builder.Property(r => r.id_rol)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN
            // =========================
            builder.Property(r => r.rol_guid)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(r => r.nombre_rol)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(r => r.descripcion_rol)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            // =========================
            // ESTADO
            // =========================
            builder.Property(r => r.estado_rol)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("ACT");

            builder.Property(r => r.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(r => r.activo)
                   .IsRequired()
                   .HasDefaultValue(true);

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

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(r => r.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

            // =========================
            // ÍNDICES ÚNICOS
            // =========================
            builder.HasIndex(r => r.rol_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_ROLES_GUID");

            builder.HasIndex(r => r.nombre_rol)
                   .IsUnique()
                   .HasDatabaseName("UQ_ROLES_NOMBRE");
        }
    }
}