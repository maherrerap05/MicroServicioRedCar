using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class MarcaVehiculoConfiguration : IEntityTypeConfiguration<MarcaVehiculoEntity>
    {
        public void Configure(EntityTypeBuilder<MarcaVehiculoEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("MARCA_VEHICULOS", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_MARCA_VEHICULOS_ESTADO", "estado_marca_vehiculo IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_MARCA_VEHICULOS_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(m => m.id_marca_vehiculo)
                   .HasName("PK_MARCA_VEHICULOS");

            builder.Property(m => m.id_marca_vehiculo)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN TÉCNICA
            // =========================
            builder.Property(m => m.marca_vehiculo_guid)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(m => m.codigo_marca_vehiculo)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(m => m.nombre_marca_vehiculo)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(m => m.descripcion_marca_vehiculo)
                   .HasMaxLength(250)
                   .IsUnicode(false);

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(m => m.estado_marca_vehiculo)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("ACT");

            builder.Property(m => m.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(m => m.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(m => m.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(m => m.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(m => m.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(m => m.modificado_desde_ip)
                   .HasMaxLength(45)
                   .IsUnicode(false);

            builder.Property(m => m.fecha_inhabilitacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(m => m.motivo_inhabilitacion)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(m => m.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(m => m.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(m => m.marca_vehiculo_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_MARCA_VEHICULOS_GUID");

            builder.HasIndex(m => m.codigo_marca_vehiculo)
                   .IsUnique()
                   .HasDatabaseName("UQ_MARCA_VEHICULOS_CODIGO");

            builder.HasIndex(m => m.nombre_marca_vehiculo)
                   .IsUnique()
                   .HasDatabaseName("UQ_MARCA_VEHICULOS_NOMBRE");
        }
    }
}