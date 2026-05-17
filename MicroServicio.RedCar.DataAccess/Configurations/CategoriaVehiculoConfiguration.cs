using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class CategoriaVehiculoConfiguration : IEntityTypeConfiguration<CategoriaVehiculoEntity>
    {
        public void Configure(EntityTypeBuilder<CategoriaVehiculoEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("categoria_vehiculos", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_CATEGORIA_VEHICULOS_ESTADO", "estado_categoria_vehiculo IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_CATEGORIA_VEHICULOS_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(c => c.id_categoria_vehiculo)
                   .HasName("PK_CATEGORIA_VEHICULOS");

            builder.Property(c => c.id_categoria_vehiculo)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(c => c.categoria_vehiculo_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); 

            builder.Property(c => c.codigo_categoria_vehiculo)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(c => c.nombre_categoria_vehiculo)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(c => c.descripcion_categoria_vehiculo)
                   .HasMaxLength(250);
            // IsUnicode(false) eliminado

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(c => c.estado_categoria_vehiculo)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(c => c.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(c => c.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(c => c.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(c => c.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(c => c.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(c => c.modificado_desde_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            builder.Property(c => c.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(c => c.motivo_inhabilitacion)
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado


            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(c => c.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(c => c.categoria_vehiculo_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_CATEGORIA_VEHICULOS_GUID");

            builder.HasIndex(c => c.codigo_categoria_vehiculo)
                   .IsUnique()
                   .HasDatabaseName("UQ_CATEGORIA_VEHICULOS_CODIGO");

            builder.HasIndex(c => c.nombre_categoria_vehiculo)
                   .IsUnique()
                   .HasDatabaseName("UQ_CATEGORIA_VEHICULOS_NOMBRE");
        }
    }
}