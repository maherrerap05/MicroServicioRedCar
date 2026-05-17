using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class ReservaConductorConfiguration : IEntityTypeConfiguration<ReservaConductorEntity>
    {
        public void Configure(EntityTypeBuilder<ReservaConductorEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("res_x_con", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_RES_X_CON_TIPO_CONDUCTOR", "tipo_conductor IN ('PRI', 'ADI')");
                tb.HasCheckConstraint("CHK_RES_X_CON_ES_PRINCIPAL", "es_principal IN (0,1)");
                tb.HasCheckConstraint("CHK_RES_X_CON_ESTADO", "estado_reserva_conductor IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_RES_X_CON_ELIMINADO", "es_eliminado IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(rc => rc.id_reserva_conductor)
                   .HasName("PK_RES_X_CON");

            builder.Property(rc => rc.id_reserva_conductor)
                   .ValueGeneratedOnAdd();

            // =========================
            // CAMPOS PRINCIPALES
            // =========================
            builder.Property(rc => rc.reserva_conductor_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(rc => rc.id_reserva)
                   .IsRequired();

            builder.Property(rc => rc.id_conductor)
                   .IsRequired();

            builder.Property(rc => rc.tipo_conductor)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength();
            // IsUnicode(false) eliminado

            builder.Property(rc => rc.es_principal)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(rc => rc.fecha_asignacion_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            // =========================
            // ESTADO / CICLO DE VIDA
            // =========================
            builder.Property(rc => rc.estado_reserva_conductor)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(rc => rc.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(rc => rc.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(rc => rc.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(rc => rc.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(rc => rc.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(rc => rc.modificado_desde_ip)
                   .HasMaxLength(45);
            // IsUnicode(false) eliminado

            builder.Property(rc => rc.fecha_inhabilitacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz

            builder.Property(rc => rc.motivo_inhabilitacion)
                   .HasMaxLength(200);
            // IsUnicode(false) eliminado


            builder.Property(rc => rc.origen_registro)
                   .IsRequired()
                   .HasMaxLength(20);
            // IsUnicode(false) eliminado

            // =========================
            // ÍNDICES / UNIQUE
            // =========================
            builder.HasIndex(rc => rc.reserva_conductor_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_RES_X_CON_GUID");

            builder.HasIndex(rc => new { rc.id_reserva, rc.id_conductor })
                   .IsUnique()
                   .HasDatabaseName("UQ_RES_X_CON_RESERVA_CONDUCTOR");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<ReservaEntity>()
                   .WithMany()
                   .HasForeignKey(rc => rc.id_reserva)
                   .HasConstraintName("FK_RES_X_CON_RESERVAS")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ConductorEntity>()
                   .WithMany()
                   .HasForeignKey(rc => rc.id_conductor)
                   .HasConstraintName("FK_RES_X_CON_CONDUCTORES")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}