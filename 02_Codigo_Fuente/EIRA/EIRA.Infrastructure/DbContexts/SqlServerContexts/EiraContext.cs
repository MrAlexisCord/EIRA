using EFCore.BulkExtensions;
using EIRA.Domain.Common;
using EIRA.Domain.EIRAEntities.App;
using EIRA.Domain.EIRAEntities.Bas;
using EIRA.Infrastructure.DbContexts.SqlServerContexts.EntitiesConfig;
using EIRA.Infrastructure.DbContexts.SqlServerContexts.ProceduresConfig;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace EIRA.Infrastructure.DbContexts.SqlServerContexts;

public partial class EiraContext : DbContext
{
    private readonly IConfiguration _configuration;

    public EiraContext(DbContextOptions<EiraContext> options
            , IConfiguration configuration
            //, IOptions<ConfiguracionBaseDeDatosSetting> dbOption
        )
            : base(options)
    {
        _configuration = configuration;
        //Database.SetCommandTimeout(TimeSpan.FromMinutes(dbOption?.Value?.TimeOutBDC ?? 10));
        Database.SetCommandTimeout(TimeSpan.FromMinutes(20));
    }

    public bool IsValidate { get; set; } = true;

    //public EiraContext()
    //{
    //}

    //public EiraContext(DbContextOptions<EiraContext> options)
    //    : base(options)
    //{
    //}

    public virtual DbSet<AppConfigurationFollowUpReport> AppConfigurationFollowUpReport { get; set; }

    public virtual DbSet<AppConfigurationGlobalReport> AppConfigurationGlobalReport { get; set; }

    public virtual DbSet<AppConfigurationLoadInformation> AppConfigurationLoadInformation { get; set; }

    public virtual DbSet<BasField> BasField { get; set; }

    public virtual DbSet<BasProject> BasProject { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB; database=EIRA; Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigApp();
        
        modelBuilder.ApplyConfigBas();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //modelBuilder.SeedData(_loggerFactory);
        modelBuilder.OnModelCreatingStoredProcedure();
    }

    public override int SaveChanges()
    {
        UpdateSoftDeleteStatuses();
        Validate();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatuses();
        Validate();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateSoftDeleteStatuses();
        Validate();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public async Task InsercionMasivaAsync<T>(IList<T> ts) where T : BaseEntity
    {
        var bulkConfig = new BulkConfig
        {
            SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.Default,
            SetOutputIdentity = true
        };
        await this.BulkInsertAsync(ts, bulkConfig);
        //await this.BulkInsertAsync(ts);
    }

    public async Task ActualizacionMasivaAsync<T>(IList<T> ts) where T : BaseEntity
    {
        var bulkConfig = new BulkConfig
        {
            SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.Default
        };
        await this.BulkUpdateAsync(ts, bulkConfig);
        //await this.BulkUpdateAsync(ts);
    }

    private void UpdateSoftDeleteStatuses()
    {
        var userName = "EIRA";
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    /*if (entry.CurrentValues[nameof(BaseEntity.UsuarioCreacion)] is null)*/
                    entry.Property(nameof(BaseEntity.UserAt)).IsModified = false;
                    /*if (entry.CurrentValues[nameof(BaseEntity.FechaCreacion)] is null)*/
                    entry.Property(nameof(BaseEntity.CreationAt)).IsModified = false;
                    entry.CurrentValues[nameof(BaseEntity.UpdateUser)] = (entry.CurrentValues[nameof(BaseEntity.UpdateUser)] is null || entry.CurrentValues[nameof(BaseEntity.UpdateUser)].ToString().Length <= 0) ? userName : entry.CurrentValues[nameof(BaseEntity.UpdateUser)];
                    entry.CurrentValues[nameof(BaseEntity.UpdatedAt)] = DateTime.Now;
                    break;
                case EntityState.Added:
                    entry.CurrentValues[nameof(BaseEntity.UserAt)] = (entry.CurrentValues[nameof(BaseEntity.UserAt)] is null || entry.CurrentValues[nameof(BaseEntity.UserAt)].ToString().Length <= 0) ? userName : entry.CurrentValues[nameof(BaseEntity.UserAt)];
                    entry.CurrentValues[nameof(BaseEntity.UpdateUser)] = (entry.CurrentValues[nameof(BaseEntity.UpdateUser)] is null || entry.CurrentValues[nameof(BaseEntity.UpdateUser)].ToString().Length <= 0) ? userName : entry.CurrentValues[nameof(BaseEntity.UpdateUser)];
                    entry.CurrentValues[nameof(BaseEntity.CreationAt)] = (entry.CurrentValues[nameof(BaseEntity.CreationAt)] == default ? DateTime.Now : entry.CurrentValues[nameof(BaseEntity.CreationAt)]);
                    entry.CurrentValues[nameof(BaseEntity.UpdatedAt)] = DateTime.Now;
                    break;
            }
        }
    }

    public void Validate()
    {
        if (IsValidate)
        {
            var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToList();

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
            }
        }
    }
}
