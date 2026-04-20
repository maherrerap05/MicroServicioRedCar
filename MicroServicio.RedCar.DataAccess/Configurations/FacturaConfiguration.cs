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
            builder.ToTable("FACTURAS", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_FACTURAS_ESTADO", "estado IN ('ABI', 'APR', 'INA')");
                tb.HasCheckConstraint("CHK_FACTURAS_ES_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_FACTURAS_SUBTOTAL", "subtotal >= 0");
                tb.HasCheckConstraint("CHK_FACTURAS_VALOR_IVA", "valor_iva >= 0");
                tb.HasCheckConstraint("CHK_FACTURAS_TOTAL", "total >= 0");
                tb.HasCheckConstraint("CHK_FACTURAS_TOTAL_COHERENTE", "total = (subtotal + valor_iva)");

                // Nuevos constraints agregados
                tb.HasCheckConstraint("CHK_FACTURAS_NUMERO_NO_VACIO", "LEN(LTRIM(RTRIM(numero_factura))) > 0");
                tb.HasCheckConstraint("CHK_FACTURAS_CREADO_POR_NO_VACIO", "LEN(LTRIM(RTRIM(creado_por_usuario))) > 0");
                tb.HasCheckConstraint("CHK_FACTURAS_SERVICIO_ORIGEN_NO_VACIO", "LEN(LTRIM(RTRIM(servicio_origen))) > 0");

                tb.HasCheckConstraint(
                    "CHK_FACTURAS_INHABILITACION_FECHA_COHERENTE",
                    "((es_eliminado = 1 AND fecha_inhabilitacion_utc IS NOT NULL) OR (es_eliminado = 0 AND fecha_inhabilitacion_utc IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_FACTURAS_INHABILITACION_MOTIVO_COHERENTE",
                    "((es_eliminado = 1 AND motivo_inhabilitacion IS NOT NULL AND LEN(LTRIM(RTRIM(motivo_inhabilitacion))) > 0) OR (es_eliminado = 0 AND motivo_inhabilitacion IS NULL))"
                );

                tb.HasCheckConstraint(
                    "CHK_FACTURAS_MODIFICACION_USUARIO_COHERENTE",
                    "((fecha_modificacion_utc IS NULL AND modificado_por_usuario IS NULL) OR (fecha_modificacion_utc IS NOT NULL AND modificado_por_usuario IS NOT NULL AND LEN(LTRIM(RTRIM(modificado_por_usuario))) > 0))"
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
                   .HasDefaultValueSql("NEWID()");

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
                   .IsUnicode(false)
                   .HasDefaultValue("PENDIENTE");

            builder.Property(f => f.fecha_emision)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(f => f.subtotal)
                   .IsRequired()
                   .HasColumnType("decimal(12,2)");

            builder.Property(f => f.valor_iva)
                   .IsRequired()
                   .HasColumnType("decimal(12,2)");

            builder.Property(f => f.total)
                   .IsRequired()
                   .HasColumnType("decimal(12,2)");

            builder.Property(f => f.observaciones_factura)
                   .HasMaxLength(300)
                   .IsUnicode(false);

            builder.Property(f => f.origen_canal_factura)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(f => f.estado)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("ABI");

            builder.Property(f => f.fecha_inhabilitacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(f => f.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(f => f.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(f => f.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(f => f.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(f => f.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            builder.Property(f => f.modificacion_ip)
                   .HasMaxLength(45)
                   .IsUnicode(false);

            // =========================
            // INTEGRACIÓN / ORIGEN
            // =========================
            builder.Property(f => f.servicio_origen)
                   .IsRequired()
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // =========================
            // CAMPOS OPCIONALES EXTENDIDOS
            // =========================
            builder.Property(f => f.motivo_inhabilitacion)
                   .HasMaxLength(250)
                   .IsUnicode(false);

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(f => f.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

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