using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class FacturaConfiguration : IEntityTypeConfiguration<FacturaEntity>
    {
        public void Configure(EntityTypeBuilder<FacturaEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("facturas", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_FACTURAS_ESTADO", "estado IN ('ABI', 'APR', 'INA')");
                tb.HasCheckConstraint("CHK_FACTURAS_ES_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_FACTURAS_SUBTOTAL", "subtotal >= 0");
                tb.HasCheckConstraint("CHK_FACTURAS_VALOR_IVA", "valor_iva >= 0");
                tb.HasCheckConstraint("CHK_FACTURAS_TOTAL", "total >= 0");
                tb.HasCheckConstraint("CHK_FACTURAS_TOTAL_COHERENTE", "total = (subtotal + valor_iva)");

                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...)) en PostgreSQL
                tb.HasCheckConstraint("CHK_FACTURAS_NUMERO_NO_VACIO", "LENGTH(TRIM(numero_factura)) > 0");
                tb.HasCheckConstraint("CHK_FACTURAS_CREADO_POR_NO_VACIO", "LENGTH(TRIM(creado_por_usuario)) > 0");
                tb.HasCheckConstraint("CHK_FACTURAS_SERVICIO_ORIGEN_NO_VACIO", "LENGTH(TRIM(servicio_origen)) > 0");

                tb.HasCheckConstraint(
                    "CHK_FACTURAS_INHABILITACION_FECHA_COHERENTE",
                    "((es_eliminado = true AND fecha_inhabilitacion_utc IS NOT NULL) OR (es_eliminado = false AND fecha_inhabilitacion_utc IS NULL))"
                // es_eliminado = 1/0 → true/false en PostgreSQL
                );

                tb.HasCheckConstraint(
                    "CHK_FACTURAS_INHABILITACION_MOTIVO_COHERENTE",
                    "((es_eliminado = true AND motivo_inhabilitacion IS NOT NULL AND LENGTH(TRIM(motivo_inhabilitacion)) > 0) OR (es_eliminado = false AND motivo_inhabilitacion IS NULL))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...)), 1/0 → true/false
                );

                tb.HasCheckConstraint(
                    "CHK_FACTURAS_MODIFICACION_USUARIO_COHERENTE",
                    "((fecha_modificacion_utc IS NULL AND modificado_por_usuario IS NULL) OR (fecha_modificacion_utc IS NOT NULL AND modificado_por_usuario IS NOT NULL AND LENGTH(TRIM(modificado_por_usuario)) > 0))"
                // LEN(LTRIM(RTRIM(...))) → LENGTH(TRIM(...))
                );
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(f => f.id_factura)
                   .HasName("PK_FACTURAS");

            builder.Property(f => f.id_factura)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN TÉCNICA
            // =========================
            builder.Property(f => f.guid_factura)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            // =========================
            // RELACIONES PRINCIPALES
            // =========================
            builder.Property(f => f.id_cliente)
                   .IsRequired();

            builder.Property(f => f.id_reserva)
                   .IsRequired();

            // =========================
            // DATOS DE FACTURACIÓN
            // =========================
            builder.Property(f => f.numero_factura)
                   .IsRequired()
                   .HasMaxLength(40)
                   .HasDefaultValue("PENDIENTE");
            // IsUnicode(false) eliminado

            builder.Property(f => f.fecha_emision)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(f => f.subtotal)
                   .IsRequired()
                   .HasColumnType("numeric(12,2)"); // decimal(12,2) → numeric(12,2)

            builder.Property(f => f.valor_iva)
                   .IsRequired()
                   .HasColumnType("numeric(12,2)"); // decimal(12,2) → numeric(12,2)

            builder.Property(f => f.total)
                   .IsRequired()
                   .HasColumnType("numeric(12,2)"); // decimal(12,2) → numeric(12,2)

            builder.Property(f => f.observaciones_factura)
                   .HasMaxLength(300);
            // IsUnicode(false) eliminado

            builder.Property(f => f.origen_canal_factura)
                   .HasMaxLength(50);
            // IsUnicode(false) eliminado

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(f => f.estado)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ABI");
            // IsUnicode(false) eliminado

            builder.Property(f => f.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(f => f.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(f => f.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(f => f.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(f => f.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(f => f.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(f => f.modificacion_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(f => f.servicio_origen)
                   .IsRequired()
                   .HasMaxLength(50);
            // IsUnicode(false) eliminado

            // =========================
            // CAMPOS OPCIONALES EXTENDIDOS
            // =========================
            builder.Property(f => f.motivo_inhabilitacion)
                   .HasMaxLength(250);
            // IsUnicode(false) eliminado


            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(f => f.guid_factura)
                   .IsUnique()
                   .HasDatabaseName("UQ_FACTURAS_GUID");

            builder.HasIndex(f => f.numero_factura)
                   .IsUnique()
                   .HasDatabaseName("UQ_FACTURAS_NUMERO");

            // =========================
            // FOREIGN KEYS
            // =========================
            builder.HasOne<ClienteEntity>()
                   .WithMany()
                   .HasForeignKey(f => f.id_cliente)
                   .HasConstraintName("FK_FACTURAS_CLIENTES")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ReservaEntity>()
                   .WithMany()
                   .HasForeignKey(f => f.id_reserva)
                   .HasConstraintName("FK_FACTURAS_RESERVAS")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}