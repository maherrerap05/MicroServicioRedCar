using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("CLIENTES", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_CLIENTES_ESTADO", "estado IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_CLIENTES_ES_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(c => c.id_cliente)
                   .HasName("PK_CLIENTES");

            builder.Property(c => c.id_cliente)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN TÉCNICA
            // =========================
            builder.Property(c => c.cliente_guid)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            // =========================
            // IDENTIFICACIÓN DEL CLIENTE
            // =========================
            builder.Property(c => c.tipo_identificacion)
                   .IsRequired()
                   .HasMaxLength(20)
                   .IsUnicode(false);

            builder.Property(c => c.numero_identificacion)
                   .IsRequired()
                   .HasMaxLength(30)
                   .IsUnicode(false);

            // =========================
            // DATOS PERSONALES / FISCALES
            // =========================
            builder.Property(c => c.nombres)
                   .IsRequired()
                   .HasMaxLength(160)
                   .IsUnicode(false);

            builder.Property(c => c.apellidos)
                   .HasMaxLength(160)
                   .IsUnicode(false);

            builder.Property(c => c.razon_social)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            // =========================
            // DATOS DE CONTACTO
            // =========================
            builder.Property(c => c.correo)
                   .IsRequired()
                   .HasMaxLength(150)
                   .IsUnicode(false);

            builder.Property(c => c.telefono)
                   .IsRequired()
                   .HasMaxLength(30)
                   .IsUnicode(false);

            builder.Property(c => c.direccion)
                   .IsRequired()
                   .HasMaxLength(250)
                   .IsUnicode(false);

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(c => c.estado)
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
            builder.Property(c => c.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(c => c.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(c => c.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(c => c.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(c => c.modificacion_ip)
                   .HasMaxLength(45)
                   .IsUnicode(false);

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(c => c.servicio_origen)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // =========================
            // CAMPOS OPCIONALES EXTENDIDOS
            // =========================
            builder.Property(c => c.fecha_inhabilitacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(c => c.motivo_inhabilitacion)
                   .HasMaxLength(250)
                   .IsUnicode(false);

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(c => c.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(c => c.cliente_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_CLIENTES_GUID");

            builder.HasIndex(c => c.numero_identificacion)
                   .IsUnique()
                   .HasDatabaseName("UQ_CLIENTES_NUMERO_IDENTIFICACION");

            builder.HasIndex(c => c.correo)
                   .IsUnique()
                   .HasDatabaseName("UQ_CLIENTES_CORREO");
        }
    }
}