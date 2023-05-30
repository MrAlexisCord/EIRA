using EIRA.Domain.EIRAEntities.Bas;
using Microsoft.EntityFrameworkCore;

namespace EIRA.Infrastructure.DbContexts.SqlServerContexts.EntitiesConfig
{
    public static class EntitiesConfigBas
    {
        public static void ApplyConfigBas(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasField>(entity =>
            {
                entity.HasKey(e => e.FieldId);

                entity.ToTable("BAS_FIELD", tb => tb.HasComment("Configuración de los campos"));

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
                entity.Property(e => e.FieldKey)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Llave del Campo")
                    .HasColumnName("FIELD_KEY");
                entity.Property(e => e.FieldType)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Tipo del Campo")
                    .HasColumnName("FIELD_TYPE");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("¿Esta Activo? (1-True, 0-False)")
                    .HasColumnName("IS_ACTIVE");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre o Descripción del Campo")
                    .HasColumnName("NAME");
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
            });

            modelBuilder.Entity<BasProject>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("BAS_PROJECT", tb => tb.HasComment("Configuración de los Proyectos"));

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Identificador Único del Proyecto")
                    .HasColumnName("PROJECT_ID");
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
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Nombre o Descripción del Proyecto")
                    .HasColumnName("NAME");
                entity.Property(e => e.ProjectKey)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Llave del Proyecto")
                    .HasColumnName("PROJECT_KEY");
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
            });
        }
    }
}
