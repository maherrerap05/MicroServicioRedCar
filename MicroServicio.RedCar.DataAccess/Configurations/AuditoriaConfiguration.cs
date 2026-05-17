using MicroServicio.RedCar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServicio.RedCar.DataAccess.Configurations
{
    public class AuditoriaConfiguration : IEntityTypeConfiguration<AuditoriaEntity>
    {
        public void Configure(EntityTypeBuilder<AuditoriaEntity> builder)
        {
            // =========================
            // TABLA
            // =========================
            builder.ToTable("auditoria", "rental", tb =>
            {
                tb.HasCheckConstraint("CHK_AUDITORIA_OPERACION", "operacion IN ('INSERT', 'UPDATE', 'DELETE')");
                tb.HasCheckConstraint("CHK_AUDITORIA_ACTIVO", "activo IN (0,1)");
            });

            // =========================
            // CLAVE PRIMARIA
            // =========================
            builder.HasKey(a => a.id_auditoria)
                   .HasName("PK_AUDITORIA");

            builder.Property(a => a.id_auditoria)
                   .ValueGeneratedOnAdd();

            // =========================
            // IDENTIFICACIÓN DEL EVENTO
            // =========================
            builder.Property(a => a.auditoria_guid)
                   .IsRequired()
                   .ValueGeneratedNever(); 

            builder.Property(a => a.tabla_afectada)
                   .IsRequired()
                   .HasMaxLength(100);
            

            builder.Property(a => a.operacion)
                   .IsRequired()
                   .HasMaxLength(10);
           

            builder.Property(a => a.id_registro_afectado)
                   .HasMaxLength(100);
           

            // =========================
            // DATOS DEL CAMBIO
            // =========================
            builder.Property(a => a.datos_anteriores)
                   .HasColumnType("text"); 

            builder.Property(a => a.datos_nuevos)
                   .HasColumnType("text"); 

            // =========================
            // CONTEXTO DEL EVENTO
            // =========================
            builder.Property(a => a.usuario_ejecutor)
                   .HasMaxLength(100);
            

            builder.Property(a => a.ip_origen)
                   .HasMaxLength(45);
            

            builder.Property(a => a.fecha_evento_utc)
                   .IsRequired()
                   .HasColumnType("timestamptz") 
                   .HasDefaultValueSql("NOW()"); 

            // =========================
            // ESTADO
            // =========================
            builder.Property(a => a.activo)
                   .IsRequired()
                   .HasDefaultValue(true);


            // =========================
            // ÍNDICES ÚNICOS
            // =========================
            builder.HasIndex(a => a.auditoria_guid)
                   .IsUnique()
                   .HasDatabaseName("UQ_AUDITORIA_GUID");

            // =========================
            // ÍNDICES DE APOYO
            // =========================
            builder.HasIndex(a => a.tabla_afectada)
                   .HasDatabaseName("IX_AUDITORIA_TABLA_AFECTADA");

            builder.HasIndex(a => a.fecha_evento_utc)
                   .HasDatabaseName("IX_AUDITORIA_FECHA_EVENTO");

            builder.HasIndex(a => a.usuario_ejecutor)
                   .HasDatabaseName("IX_AUDITORIA_USUARIO_EJECUTOR");
        }
    }
}