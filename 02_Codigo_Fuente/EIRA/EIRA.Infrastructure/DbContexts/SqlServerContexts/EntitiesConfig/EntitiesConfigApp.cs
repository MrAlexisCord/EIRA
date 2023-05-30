using EIRA.Domain.EIRAEntities.App;
using Microsoft.EntityFrameworkCore;

namespace EIRA.Infrastructure.DbContexts.SqlServerContexts.EntitiesConfig
{
    public static class EntitiesConfigApp
    {
        public static void ApplyConfigApp(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfigurationFollowUpReport>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.FieldId });

                entity.ToTable("APP_CONFIGURATION_FOLLOW_UP_REPORT", tb => tb.HasComment("Configuración de los Campos por Proyecto para el Reporte de Seguimiento"));

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Proyecto")
                    .HasColumnName("PROJECT_ID");
                entity.Property(e => e.FieldId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Campo")
                    .HasColumnName("FIELD_ID");
                entity.Property(e => e.CreationAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de Creación del Registro")
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_AT");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("¿Esta Activo? (1-True, 0-False)")
                    .HasColumnName("IS_ACTIVE");
                entity.Property(e => e.OrderNumber)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Orden")
                    .HasColumnName("ORDER_NUMBER");
                entity.Property(e => e.UpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Usuario que Realizó la Última Actualización del Registro")
                    .HasColumnName("UPDATE_USER");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de la Última Actualización del Registro")
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATED_AT");
                entity.Property(e => e.UserAt)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Usuario que Realizó la Creación del Registro")
                    .HasColumnName("USER_AT");

                entity.HasOne(d => d.Field).WithMany(p => p.AppConfigurationFollowUpReport)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APP_CONFIGURATION_FOLLOW_UP_REPORT_FIELD_ID");

                entity.HasOne(d => d.Project).WithMany(p => p.AppConfigurationFollowUpReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APP_CONFIGURATION_FOLLOW_UP_REPORT_PROJECT_ID");
            });

            modelBuilder.Entity<AppConfigurationGlobalReport>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.FieldId });

                entity.ToTable("APP_CONFIGURATION_GLOBAL_REPORT", tb => tb.HasComment("Configuración de los Campos por Proyecto para el Reporte Global"));

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Proyecto")
                    .HasColumnName("PROJECT_ID");
                entity.Property(e => e.FieldId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Campo")
                    .HasColumnName("FIELD_ID");
                entity.Property(e => e.CreationAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de Creación del Registro")
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_AT");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("¿Esta Activo? (1-True, 0-False)")
                    .HasColumnName("IS_ACTIVE");
                entity.Property(e => e.OrderNumber)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Orden")
                    .HasColumnName("ORDER_NUMBER");
                entity.Property(e => e.UpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Usuario que Realizó la Última Actualización del Registro")
                    .HasColumnName("UPDATE_USER");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de la Última Actualización del Registro")
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATED_AT");
                entity.Property(e => e.UserAt)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Usuario que Realizó la Creación del Registro")
                    .HasColumnName("USER_AT");

                entity.HasOne(d => d.Field).WithMany(p => p.AppConfigurationGlobalReport)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APP_CONFIGURATION_GLOBAL_REPORT_FIELD_ID");

                entity.HasOne(d => d.Project).WithMany(p => p.AppConfigurationGlobalReport)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APP_CONFIGURATION_GLOBAL_REPORT_PROJECT_ID");
            });

            modelBuilder.Entity<AppConfigurationLoadInformation>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.FieldId });

                entity.ToTable("APP_CONFIGURATION_LOAD_INFORMATION", tb => tb.HasComment("Configuración de los Campos por Proyecto para Carga de Información"));

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Proyecto")
                    .HasColumnName("PROJECT_ID");
                entity.Property(e => e.FieldId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Campo")
                    .HasColumnName("FIELD_ID");
                entity.Property(e => e.CreationAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de Creación del Registro")
                    .HasColumnType("datetime")
                    .HasColumnName("CREATION_AT");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("¿Esta Activo? (1-True, 0-False)")
                    .HasColumnName("IS_ACTIVE");
                entity.Property(e => e.OrderNumber)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Orden")
                    .HasColumnName("ORDER_NUMBER");
                entity.Property(e => e.UpdateUser)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Usuario que Realizó la Última Actualización del Registro")
                    .HasColumnName("UPDATE_USER");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Fecha de la Última Actualización del Registro")
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATED_AT");
                entity.Property(e => e.UserAt)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Usuario que Realizó la Creación del Registro")
                    .HasColumnName("USER_AT");

                entity.HasOne(d => d.Field).WithMany(p => p.AppConfigurationLoadInformation)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APP_CONFIGURATION_LOAD_INFORMATION_FIELD_ID");

                entity.HasOne(d => d.Project).WithMany(p => p.AppConfigurationLoadInformation)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APP_CONFIGURATION_LOAD_INFORMATION_PROJECT_ID");
            });
        }
    }
}
