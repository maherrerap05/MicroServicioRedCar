using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class ConductorConfiguration : IEntityTypeConfiguration<ConductorEntity>
    {
        public void Configure(EntityTypeBuilder<ConductorEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("conductores", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_CONDUCTORES_ESTADO", "estado_conductor IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_CONDUCTORES_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_CONDUCTORES_EDAD", "edad_conductor >= 18");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(c => c.id_conductor)
                   .HasName("PK_CONDUCTORES");

            builder.Property(c => c.id_conductor)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(c => c.conductor_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(c => c.codigo_conductor)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(c => c.tipo_identificacion)
                   .IsRequired()
                   .HasMaxLength(10);
            // IsUnicode(false) eliminado

            builder.Property(c => c.numero_identificacion)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(c => c.con_nombre1)
                   .IsRequired()
                   .HasMaxLength(80);
            // IsUnicode(false) eliminado

            builder.Property(c => c.con_nombre2)
                   .HasMaxLength(80);
            // IsUnicode(false) eliminado

            builder.Property(c => c.con_apellido1)
                   .IsRequired()
                   .HasMaxLength(80);
            // IsUnicode(false) eliminado

            builder.Property(c => c.con_apellido2)
                   .HasMaxLength(80);
            // IsUnicode(false) eliminado

            builder.Property(c => c.numero_licencia)
                   .IsRequired()
                   .HasMaxLength(30);
            // IsUnicode(false) eliminado

            builder.Property(c => c.fecha_vencimiento_licencia)
                   .IsRequired()
                   .HasColumnType("date");
            // date es igual en PostgreSQL, sin cambio

            builder.Property(c => c.edad_conductor)
                   .IsRequired()
                   .HasColumnType("smallint"); // tinyint → smallint (PostgreSQL no tiene tinyint)

            builder.Property(c => c.con_telefono)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            builder.Property(c => c.con_correo)
                   .IsRequired()
                   .HasMaxLength(120);
            // IsUnicode(false) eliminado

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(c => c.estado_conductor)
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
            builder.HasIndex(c => c.conductor_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_CONDUCTORES_GUID");

            builder.HasIndex(c => c.codigo_conductor)
                   .IsUnique()
                   .HasDatabaseName("UQ_CONDUCTORES_CODIGO");

            builder.HasIndex(c => c.numero_identificacion)
                   .IsUnique()
                   .HasDatabaseName("UQ_CONDUCTORES_IDENTIFICACION");

            builder.HasIndex(c => c.numero_licencia)
                   .IsUnique()
                   .HasDatabaseName("UQ_CONDUCTORES_LICENCIA");
        }
    }
}