using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRolEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioRolEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("USUARIOS_ROLES", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_USUARIOS_ROLES_ESTADO", "estado_usuario_rol IN ('ACT', 'INA')");
                tb.HasCheckConstraint("CHK_USUARIOS_ROLES_ELIMINADO", "es_eliminado IN (0,1)");
                tb.HasCheckConstraint("CHK_USUARIOS_ROLES_ACTIVO", "activo IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(ur => ur.id_usuario_rol)
                   .HasName("PK_USUARIOS_ROLES");

            builder.Property(ur => ur.id_usuario_rol)
                   .ValueGeneratedOnAdd();

            // =========================
            // CLAVES FORÁNEAS
            // =========================
            builder.Property(ur => ur.id_usuario)
                   .IsRequired();

            builder.Property(ur => ur.id_rol)
                   .IsRequired();

            // =========================
            // ESTADO
            // =========================
            builder.Property(ur => ur.estado_usuario_rol)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsUnicode(false)
                   .HasDefaultValue("ACT");

            builder.Property(ur => ur.es_eliminado)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(ur => ur.activo)
                   .IsRequired()
                   .HasDefaultValue(true);

            // =========================
            // AUDITORÍA
            // =========================
            builder.Property(ur => ur.fecha_registro_utc)
                   .IsRequired()
                   .HasColumnType("datetime2(0)")
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(ur => ur.creado_por_usuario)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(ur => ur.modificado_por_usuario)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.Property(ur => ur.fecha_modificacion_utc)
                   .HasColumnType("datetime2(0)");

            // =========================
            // CONCURRENCIA
            // =========================
            builder.Property(ur => ur.row_version)
                   .IsRowVersion()
                   .IsConcurrencyToken();

            // =========================
            // ÍNDICE ÚNICO COMPUESTO
            // =========================
            builder.HasIndex(ur => new { ur.id_usuario, ur.id_rol })
                   .IsUnique()
                   .HasDatabaseName("UQ_USUARIOS_ROLES_USUARIO_ROL");

            // =========================
            // RELACIONES
            // =========================
            builder.HasOne(ur => ur.Usuario)
                   .WithMany(u => u.UsuarioRoles)
                   .HasForeignKey(ur => ur.id_usuario)
                   .HasConstraintName("FK_USUARIOS_ROLES_USUARIO_APP")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Rol)
                   .WithMany(r => r.UsuarioRoles)
                   .HasForeignKey(ur => ur.id_rol)
                   .HasConstraintName("FK_USUARIOS_ROLES_ROL")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}