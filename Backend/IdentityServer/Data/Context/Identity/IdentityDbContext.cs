using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Configurations.Hr;
using Data.Configurations.Identity;
using Domain.Services;
using Entities.Entities.Audit;
using Entities.Entities.Common;
using Entities.Entities.Hr;
using Entities.Entities.Identity;
using Entities.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Context.Identity
{
    public class IdentityServerDbContext : IdentityDbContext<
        User,
        Role,
        Guid,
        UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken>
    {
        private readonly ISessionStorage _sessionStorage;
        //private readonly IDataInitializer _dataInitializer;
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        {
            //_dataInitializer = BuildServiceCollection();
            _sessionStorage = GetSessionStorage();

        }

        #region Identity Entities

        public virtual DbSet<App> Apps { get; set; }
        public virtual DbSet<UserApp> UserApps { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PagePermission> PagePermissions { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }

        #endregion

        #region Hr Entities
        
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        #endregion

        #region  Common Entities
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TemplateForm> TemplateForms { get; set; }
        public virtual DbSet<Audit> AuditTrails { get; set; }
        #endregion

        #region Protected Ovrriden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region IdentityConfiguration
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new AppConfig());
            modelBuilder.ApplyConfiguration(new RoleClaimConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserLoginConfig());
            modelBuilder.ApplyConfiguration(new UserAppConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());
            modelBuilder.ApplyConfiguration(new PageConfig());
            modelBuilder.ApplyConfiguration(new PagePermissionConfig());
            modelBuilder.ApplyConfiguration(new UserTokenConfig());

            #endregion

            #region Hr Configuration
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            #endregion

            #region Seed Data
            //modelBuilder.Entity<Permission>().HasData(_dataInitializer.SeedPermissions());
            #endregion


            

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            modelBuilder.Entity<Role>(builder =>
            {
                builder.Metadata.RemoveIndex(new[] { builder.Property(r => r.NormalizedName).Metadata });

                builder.HasIndex(r => new { r.NormalizedName, r.AppId }).HasName("RoleNameIndex").IsUnique();
            });
        }

        #endregion

        #region Public Overriden Methods

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync();
            return result;
        }

        #endregion

        #region Private Methods

        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;
                var userId = _sessionStorage?.UserId.ToString();
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name  , UserId = userId ,
                };

                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditTrails.Add(auditEntry.ToAudit());
            }
        }

        #endregion



        //private IDataInitializer BuildServiceCollection()
        //{
        //    var serviceCollection = new ServiceCollection();
        //    serviceCollection.AddTransient<IDataInitializer, DataInitializer.DataInitializer>();

        //    IServiceProvider provider = serviceCollection.BuildServiceProvider();

        //    var instance = provider.GetService<IDataInitializer>();
        //    return instance;
        //}

        private ISessionStorage GetSessionStorage()
        {
            var services = new ServiceCollection();
            services.AddScoped<ISessionStorage, SessionStorage>();
            var provider = services.BuildServiceProvider();
            return provider.GetService<ISessionStorage>();
        }

    }
}
