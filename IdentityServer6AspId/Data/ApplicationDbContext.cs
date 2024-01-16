using IdentityServer6AspId.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer6AspId.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}


        public virtual DbSet<AspNetLookup> AspNetLookup { get; set; }
        public virtual DbSet<AspNetLookupHistory> AspNetLookupHistory { get; set; }
        public virtual DbSet<AspNetLookupItem> AspNetLookupItem { get; set; }
        public virtual DbSet<AspNetLookupItemHistory> AspNetLookupItemHistory { get; set; }
		
        public DbSet<AspNetTenant> AspNetTenant { get; set; }
        public virtual DbSet<AspNetTenantConfig> AspNetTenantConfig { get; set; }
        public virtual DbSet<AspNetTenantConfigHistory> AspNetTenantConfigHistory { get; set; }
        public virtual DbSet<AspNetTenantHistory> AspNetTenantHistory { get; set; }
        public virtual DbSet<AspNetTenantUser> AspNetTenantUser { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            modelBuilder.Entity<AspNetLookup>(entity =>
            {
                entity.HasIndex(e => e.BuiltinId)
                    
                    .HasDatabaseName("IX_AspNetLookup2")
                    .IsUnique()
                    .HasFilter("([BuiltinID] IS NOT NULL)");

                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("IX_AspNetLookup1")
                    .IsUnique();

                entity.Property(e => e.LookupId).HasColumnName("LookupId");

                entity.Property(e => e.BuiltinId).HasColumnName("BuiltinID");

                entity.Property(e => e.Custom1Header).HasMaxLength(40);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<AspNetLookupHistory>(entity =>
            {
                entity.HasIndex(e => e.LookupId)
                    .HasName("IX_LookupHistory1");

                entity.Property(e => e.LookupHistoryId).HasColumnName("LookupHistoryID");

                entity.Property(e => e.AuditAction)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.BuiltinId).HasColumnName("BuiltinID");

                entity.Property(e => e.CurrentUser)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Custom1Header).HasMaxLength(40);

                entity.Property(e => e.LookupId).HasColumnName("LookupID");

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.HasOne(d => d.AspNetLookup)
                    .WithMany(p => p.AspNetLookupHistory)
                    .HasForeignKey(d => d.LookupId)
                    .HasConstraintName("FK_LookupHistory_Lookup");
            });

            modelBuilder.Entity<AspNetLookupItem>(entity =>
            {
                entity.HasIndex(e => new { e.LookupId, e.Value })
                    .HasName("IX_LookupItem1")
                    .IsUnique();

                entity.Property(e => e.LookupItemId).HasColumnName("LookupItemID");

                entity.Property(e => e.Custom1).HasMaxLength(256);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LookupId).HasColumnName("LookupID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.AspNetLookup)
                    .WithMany(p => p.AspNetLookupItem)
                    .HasForeignKey(d => d.LookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookupItem_Lookup");
            });

            modelBuilder.Entity<AspNetLookupItemHistory>(entity =>
            {
                entity.HasIndex(e => e.LookupId)
                    .HasName("IX_LookupItemHistory2");

                entity.HasIndex(e => e.LookupItemId)
                    .HasName("IX_LookupItemHistory1");

                entity.Property(e => e.LookupItemHistoryId).HasColumnName("LookupItemHistoryID");

                entity.Property(e => e.AuditAction)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentUser)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Custom1).HasMaxLength(256);

                entity.Property(e => e.LookupId).HasColumnName("LookupID");

                entity.Property(e => e.LookupItemId).HasColumnName("LookupItemID");

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.HasOne(d => d.AspNetLookup)
                    .WithMany(p => p.AspNetLookupItemHistory)
                    .HasForeignKey(d => d.LookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookupItemHistory_Lookup");

                entity.HasOne(d => d.AspNetLookupItem)
                    .WithMany(p => p.AspNetLookupItemHistory)
                    .HasForeignKey(d => d.LookupItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookupItemHistory_LookupItem");
            });

            modelBuilder.Entity<AspNetTenant>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("IX_Tenant1")
                    .IsUnique();

                entity.HasIndex(e => e.FxTenantId)
                    .HasName("IX_Tenant4")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("IX_Tenant2")
                    .IsUnique();

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ForceUseAad).HasColumnName("ForceUseAAD");

                entity.Property(e => e.FxTenantId)
                    .HasColumnName("FxTenantID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<AspNetTenantConfig>(entity =>
            {
                entity.HasIndex(e => new { e.IsActive, e.IsInitialized, e.TenantId, e.SiteType, e.ApplicationId })
                    .HasName("IX_TenantConfig1")
                    .IsUnique();

                entity.Property(e => e.TenantConfigId).HasColumnName("TenantConfigID");

                entity.Property(e => e.ApiUrl)
                    .IsRequired()
                    .HasColumnName("ApiURL")
                    .HasMaxLength(256);

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.CustomLogo).HasMaxLength(256);

                entity.Property(e => e.CustomStyleSheet).HasMaxLength(256);

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ExtApiUrl)
                    .HasColumnName("ExtApiURL")
                    .HasMaxLength(256);

                entity.Property(e => e.InitialCatalog)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsInitialized).HasDefaultValueSql("((0))");

                entity.Property(e => e.SiteType)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.Property(e => e.WebsiteUrl)
                    .IsRequired()
                    .HasColumnName("WebsiteURL")
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspNetTenantConfig)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.AspNetTenant)
                    .WithMany(p => p.AspNetTenantConfig)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenantConfig_Tenant");
            });

            modelBuilder.Entity<AspNetTenantConfigHistory>(entity =>
            {
                entity.HasIndex(e => e.TenantConfigId)
                    .HasName("IX_TenantConfigHistory1");

                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_TenantConfigHistory2");

                entity.Property(e => e.TenantConfigHistoryId).HasColumnName("TenantConfigHistoryID");

                entity.Property(e => e.ApiUrl)
                    .HasColumnName("ApiURL")
                    .HasMaxLength(256);

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AuditAction)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentUser)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CustomLogo).HasMaxLength(256);

                entity.Property(e => e.CustomStyleSheet).HasMaxLength(256);

                entity.Property(e => e.DataSource).HasMaxLength(256);

                entity.Property(e => e.ExtApiUrl)
                    .HasColumnName("ExtApiURL")
                    .HasMaxLength(256);

                entity.Property(e => e.InitialCatalog).HasMaxLength(256);

                entity.Property(e => e.SiteType).HasMaxLength(40);

                entity.Property(e => e.TenantConfigId).HasColumnName("TenantConfigID");

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.Property(e => e.WebsiteUrl)
                    .HasColumnName("WebsiteURL")
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AspNetTenantConfigHistory)
                    .HasForeignKey(d => d.ApplicationId);

                entity.HasOne(d => d.AspNetTenantConfig)
                    .WithMany(p => p.AspNetTenantConfigHistory)
                    .HasForeignKey(d => d.TenantConfigId)
                    .HasConstraintName("FK_TenantConfigHistory_TenantConfig");

                entity.HasOne(d => d.AspNetTenant)
                    .WithMany(p => p.AspNetTenantConfigHistory)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_TenantConfigHistory_Tenant");
            });

            modelBuilder.Entity<AspNetTenantHistory>(entity =>
            {
                entity.HasIndex(e => e.TenantId)
                    .HasName("IX_TenantHistory1");

                entity.Property(e => e.TenantHistoryId).HasColumnName("TenantHistoryID");

                entity.Property(e => e.AuditAction)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.CancelledDate).HasColumnType("datetime");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CurrentUser)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.ForceUseAad).HasColumnName("ForceUseAAD");

                entity.Property(e => e.FxTenantId).HasColumnName("FxTenantID");

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.HasOne(d => d.AspNetTenant)
                    .WithMany(p => p.AspNetTenantHistory)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenantHistory_Tenant");
            });

            modelBuilder.Entity<AspNetTenantUser>(entity =>
            {
                entity.HasIndex(e => new { e.FxUserId, e.FxTenantId, e.LoginProvider, e.LoginUserId })
                    .HasName("IX_TenantUser1")
                    .IsUnique();

                entity.Property(e => e.TenantUserId).HasColumnName("TenantUserID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FxTenantId).HasColumnName("FxTenantID");

                entity.Property(e => e.FxUserId).HasColumnName("FxUserID");

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.LoginUserId)
                    .IsRequired()
                    .HasColumnName("LoginUserID")
                    .HasMaxLength(256);

                entity.Property(e => e.LoginUsername)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.AspNetTenant)
                    .WithMany(p => p.AspNetTenantUser)
                    .HasPrincipalKey(p => p.FxTenantId)
                    .HasForeignKey(d => d.FxTenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenantUser_Tenant");
            });

        }
    }
}