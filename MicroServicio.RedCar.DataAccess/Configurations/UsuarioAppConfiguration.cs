using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class UsuarioAppConfiguration : IEntityTypeConfiguration<UsuarioAppEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioAppEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("usuario_app", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_USUARIOS_APP_ESTADO", "estado_usuario IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_USUARIOS_APP_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_USUARIOS_APP_ACTIVO", "activo IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(u => u.id_usuario)
                   .HasName("PK_USUARIOS_APP");

            builder.Property(u => u.id_usuario)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN
            // =========================
            builder.Property(u => u.usuario_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); // NEWID() → gen_random_uuid()

            builder.Property(u => u.username)
                   .IsRequired()
                   .HasMaxLength(50);
            // IsUnicode(false) eliminado

            builder.Property(u => u.correo)
                   .IsRequired()
                   .HasMaxLength(120);
            // IsUnicode(false) eliminado

            // =========================
            // SEGURIDAD
            // =========================
            builder.Property(u => u.password_hash)
                   .IsRequired()
                   .HasMaxLength(500);
            // IsUnicode(false) eliminado

            builder.Property(u => u.password_salt)
                   .IsRequired()
                   .HasMaxLength(250);
            // IsUnicode(false) eliminado

            // =========================
            // ESTADO
            // =========================
            builder.Property(u => u.estado_usuario)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .HasDefaultValue("ACT");
            // IsUnicode(false) eliminado

            builder.Property(u => u.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(u => u.activo)
                   .IsRequired()
                   .HasDefaultValue(true);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(u => u.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") // datetime2(0) → timestamptz
                   .HasDefaultValueSql("NOW()"); // SYSUTCDATETIME() → NOW()

            builder.Property(u => u.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(u => u.modificado_por_usuario)
                   .HasMaxLength(100);
            // IsUnicode(false) eliminado

            builder.Property(u => u.fecha_modificacion_utc)
                   .HasColumnType("timestamptz"); // datetime2(0) → timestamptz


            // =========================
            // CLAVE FORÁNEA
            // =========================
            builder.Property(u => u.id_cliente)
                   .IsRequired();

            // =========================
            // ÍNDICES ÚNICOS
            // =========================
            builder.HasIndex(u => u.usuario_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_USUARIOS_APP_GUID");

            builder.HasIndex(u => u.username)
                   .IsUnique()
                   .HasDatabaseName("UQ_USUARIOS_APP_USERNAME");

            builder.HasIndex(u => u.correo)
                   .IsUnique()
                   .HasDatabaseName("UQ_USUARIOS_APP_CORREO");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne<ClienteEntity>()
                   .WithMany()
                   .HasForeignKey(u => u.id_cliente)
                   .HasConstraintName("FK_USUARIO_APP_CLIENTES")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}