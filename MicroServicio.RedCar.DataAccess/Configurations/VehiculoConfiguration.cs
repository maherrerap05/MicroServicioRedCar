using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class VehiculoConfiguration : IEntityTypeConfiguration<VehiculoEntity>
    {
        public void Configure(EntityTypeBuilder<VehiculoEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("VEHICULOS", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_VEHICULOS_ESTADO", "estado_vehiculo IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_VEHICULOS_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_VEHICULOS_PRECIO_BASE", "precio_base_dia > 0");
                tb.HasCheckConstraint("CHK_VEHICULOS_KILOMETRAJE", "kilometraje_actual >= 0");
                tb.HasCheckConstraint("CHK_VEHICULOS_CAP_PASAJEROS", "capacidad_pasajeros > 0");
                tb.HasCheckConstraint("CHK_VEHICULOS_CAP_MALETAS", "capacidad_maletas >= 0");
                tb.HasCheckConstraint("CHK_VEHICULOS_NUM_PUERTAS", "numero_puertas > 0");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(v => v.id_vehiculo)
                   .HasName("PK_VEHICULOS");

            builder.Property(v => v.id_vehiculo)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(v => v.vehiculo_guid)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(v => v.codigo_interno_vehiculo)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(v => v.placa_vehiculo)
                   .IsRequired()
                   .HasMaxLength(15)
                   .IsUnicode(false);

            builder.Property(v => v.modelo_vehiculo)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(v => v.anio_fabricacion)
                   .IsRequired()
                   .HasColumnType("smallint");

            builder.Property(v => v.color_vehiculo)
                   .IsRequired()
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(v => v.tipo_combustible)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(v => v.tipo_transmision)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(v => v.capacidad_pasajeros)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(v => v.capacidad_maletas)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(v => v.numero_puertas)
                   .IsRequired()
                   .HasColumnType("tinyint");

            builder.Property(v => v.localizacion_actual)
                    .IsRequired();

            builder.Property(v => v.precio_base_dia)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.Property(v => v.kilometraje_actual)
                   .IsRequired();

            builder.Property(v => v.observaciones_generales)
                   .HasMaxLength(300)
                   .IsUnicode(false);

            builder.Property(v => v.imagen_referencial_url)
                   .HasMaxLength(300)
                   .IsUnicode(false);

            builder.Property(v => v.aire_acondicionado)
                   .IsRequired()
                   .HasDefaultValue(false);


            // =========================
            // CLAVES FORÁNEAS NUEVAS
            // =========================
            builder.Property(v => v.id_marca_vehiculo)
                   .IsRequired();
                   

            builder.Property(v => v.id_categoria_vehiculo)
                   .IsRequired();

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(v => v.estado_vehiculo)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("ACT");

            builder.Property(v => v.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA / INTEGRACIÓN
            // =========================
            builder.Property(v => v.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(v => v.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(v => v.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(v => v.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(v => v.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(v => v.modificado_desde_ip)
                   .HasMaxLength(45)
                   .IsUnicode(false);

            builder.Property(v => v.fecha_inhabilitacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(v => v.motivo_inhabilitacion)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(v => v.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(v => v.vehiculo_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_VEHICULOS_GUID");

            builder.HasIndex(v => v.codigo_interno_vehiculo)
                   .IsUnique()
                   .HasDatabaseName("UQ_VEHICULOS_CODIGO");

            builder.HasIndex(v => v.placa_vehiculo)
                   .IsUnique()
                   .HasDatabaseName("UQ_VEHICULOS_PLACA");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<LocalizacionEntity>()
                   .WithMany()
                   .HasForeignKey(v => v.localizacion_actual)
                   .HasConstraintName("FK_VEHICULOS_LOCALIZACIONES")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<MarcaVehiculoEntity>()
                   .WithMany()
                   .HasForeignKey(v => v.id_marca_vehiculo)
                   .HasConstraintName("FK_VEHICULOS_MARCA_VEHICULOS")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CategoriaVehiculoEntity>()
                   .WithMany()
                   .HasForeignKey(v => v.id_categoria_vehiculo)
                   .HasConstraintName("FK_VEHICULOS_CATEGORIA_VEHICULOS")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}