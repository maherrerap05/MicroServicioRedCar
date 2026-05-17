using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class LocalizacionConfiguration : IEntityTypeConfiguration<LocalizacionEntity>
    {
        public void Configure(EntityTypeBuilder<LocalizacionEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("localizaciones", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_LOCALIZACIONES_ESTADO", "estado_localizacion IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_LOCALIZACIONES_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(l => l.id_localizacion)
                   .HasName("PK_LOCALIZACIONES");

            builder.Property(l => l.id_localizacion)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(l => l.localizacion_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(l => l.codigo_localizacion)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(l => l.nombre_localizacion)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(l => l.direccion_localizacion)
                   .IsRequired()
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado

            builder.Property(l => l.telefono_contacto)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(l => l.correo_contacto)
                   .IsRequired()
                   .HasMaxLength(120);
            // IsUnicode(false) eliminado

            builder.Property(l => l.horario_atencion)
                   .IsRequired()
                   .HasMaxLength(120);
            // IsUnicode(false) eliminado

            builder.Property(l => l.zona_horaria)
                   .IsRequired()
                   .HasMaxLength(50);
            // IsUnicode(false) eliminado

            // =========================
            // CLAVE FORÁNEA
            // =========================
            builder.Property(l => l.id_ciudad)
                   .IsRequired();

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(l => l.estado_localizacion)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(l => l.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(l => l.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(l => l.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(l => l.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(l => l.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(l => l.modificado_desde_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            builder.Property(l => l.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(l => l.motivo_inhabilitacion)
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado


            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(l => l.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(l => l.localizacion_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_LOCALIZACIONES_GUID");

            builder.HasIndex(l => l.codigo_localizacion)
                   .IsUnique()
                   .HasDatabaseName("UQ_LOCALIZACIONES_CODIGO");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<CiudadEntity>()
                   .WithMany()
                   .HasForeignKey(l => l.id_ciudad)
                   .HasConstraintName("FK_LOCALIZACIONES_CIUDADES")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}