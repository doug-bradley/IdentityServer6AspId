﻿// <auto-generated />
using System;
using IdentityServer6AspId.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IdentityServer6AspId.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IdentityServer6AspId.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeZone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookup", b =>
                {
                    b.Property<long>("LookupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("LookupId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LookupId"), 1L, 1);

                    b.Property<long?>("BuiltinId")
                        .HasColumnType("bigint")
                        .HasColumnName("BuiltinID");

                    b.Property<string>("Custom1Header")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("LookupId");

                    b.HasIndex("BuiltinId")
                        .IsUnique()
                        .HasDatabaseName("IX_AspNetLookup2")
                        .HasFilter("([BuiltinID] IS NOT NULL)");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_AspNetLookup1");

                    b.ToTable("AspNetLookup");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupHistory", b =>
                {
                    b.Property<long>("LookupHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("LookupHistoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LookupHistoryId"), 1L, 1);

                    b.Property<string>("AuditAction")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("BuiltinId")
                        .HasColumnType("bigint")
                        .HasColumnName("BuiltinID");

                    b.Property<string>("CurrentUser")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Custom1Header")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long?>("LookupId")
                        .HasColumnType("bigint")
                        .HasColumnName("LookupID");

                    b.Property<string>("Name")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("LookupHistoryId");

                    b.HasIndex("LookupId")
                        .HasDatabaseName("IX_LookupHistory1");

                    b.ToTable("AspNetLookupHistory");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupItem", b =>
                {
                    b.Property<long>("LookupItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("LookupItemID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LookupItemId"), 1L, 1);

                    b.Property<string>("Custom1")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<long>("LookupId")
                        .HasColumnType("bigint")
                        .HasColumnName("LookupID");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("LookupItemId");

                    b.HasIndex("LookupId", "Value")
                        .IsUnique()
                        .HasDatabaseName("IX_LookupItem1");

                    b.ToTable("AspNetLookupItem");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupItemHistory", b =>
                {
                    b.Property<long>("LookupItemHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("LookupItemHistoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LookupItemHistoryId"), 1L, 1);

                    b.Property<string>("AuditAction")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CurrentUser")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Custom1")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<long>("LookupId")
                        .HasColumnType("bigint")
                        .HasColumnName("LookupID");

                    b.Property<long>("LookupItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("LookupItemID");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("LookupItemHistoryId");

                    b.HasIndex("LookupId")
                        .HasDatabaseName("IX_LookupItemHistory2");

                    b.HasIndex("LookupItemId")
                        .HasDatabaseName("IX_LookupItemHistory1");

                    b.ToTable("AspNetLookupItemHistory");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenant", b =>
                {
                    b.Property<long>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TenantID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TenantId"), 1L, 1);

                    b.Property<DateTime?>("CancelledDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("ForceUseAad")
                        .HasColumnType("bit")
                        .HasColumnName("ForceUseAAD");

                    b.Property<Guid>("FxTenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FxTenantID")
                        .HasDefaultValueSql("(newid())");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("TenantId");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("IX_Tenant1");

                    b.HasIndex("FxTenantId")
                        .IsUnique()
                        .HasDatabaseName("IX_Tenant4");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Tenant2");

                    b.ToTable("AspNetTenant");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantConfig", b =>
                {
                    b.Property<long>("TenantConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TenantConfigID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TenantConfigId"), 1L, 1);

                    b.Property<string>("ApiUrl")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("ApiURL");

                    b.Property<long>("ApplicationId")
                        .HasColumnType("bigint")
                        .HasColumnName("ApplicationID");

                    b.Property<string>("CustomLogo")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CustomStyleSheet")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("DataSource")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ExtApiUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("ExtApiURL");

                    b.Property<string>("InitialCatalog")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool?>("IsInitialized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("SiteType")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantID");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("WebsiteURL");

                    b.HasKey("TenantConfigId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("TenantId");

                    b.HasIndex("IsActive", "IsInitialized", "TenantId", "SiteType", "ApplicationId")
                        .IsUnique()
                        .HasDatabaseName("IX_TenantConfig1")
                        .HasFilter("[IsActive] IS NOT NULL AND [IsInitialized] IS NOT NULL");

                    b.ToTable("AspNetTenantConfig");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantConfigHistory", b =>
                {
                    b.Property<long>("TenantConfigHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TenantConfigHistoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TenantConfigHistoryId"), 1L, 1);

                    b.Property<string>("ApiUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("ApiURL");

                    b.Property<long?>("ApplicationId")
                        .HasColumnType("bigint")
                        .HasColumnName("ApplicationID");

                    b.Property<string>("AuditAction")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CurrentUser")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CustomLogo")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CustomStyleSheet")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("DataSource")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ExtApiUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("ExtApiURL");

                    b.Property<string>("InitialCatalog")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsInitialized")
                        .HasColumnType("bit");

                    b.Property<string>("SiteType")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<long?>("TenantConfigId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantConfigID");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantID");

                    b.Property<string>("WebsiteUrl")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("WebsiteURL");

                    b.HasKey("TenantConfigHistoryId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("TenantConfigId")
                        .HasDatabaseName("IX_TenantConfigHistory1");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_TenantConfigHistory2");

                    b.ToTable("AspNetTenantConfigHistory");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantHistory", b =>
                {
                    b.Property<long>("TenantHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TenantHistoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TenantHistoryId"), 1L, 1);

                    b.Property<string>("AuditAction")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CancelledDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CurrentUser")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("ForceUseAad")
                        .HasColumnType("bit")
                        .HasColumnName("ForceUseAAD");

                    b.Property<Guid?>("FxTenantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FxTenantID");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("TenantID");

                    b.HasKey("TenantHistoryId");

                    b.HasIndex("TenantId")
                        .HasDatabaseName("IX_TenantHistory1");

                    b.ToTable("AspNetTenantHistory");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantUser", b =>
                {
                    b.Property<long>("TenantUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("TenantUserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TenantUserId"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<Guid>("FxTenantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FxTenantID");

                    b.Property<Guid>("FxUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FxUserID");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LoginUserId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("LoginUserID");

                    b.Property<string>("LoginUsername")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("TenantUserId");

                    b.HasIndex("FxTenantId");

                    b.HasIndex("FxUserId", "FxTenantId", "LoginProvider", "LoginUserId")
                        .IsUnique()
                        .HasDatabaseName("IX_TenantUser1");

                    b.ToTable("AspNetTenantUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupHistory", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetLookup", "AspNetLookup")
                        .WithMany("AspNetLookupHistory")
                        .HasForeignKey("LookupId")
                        .HasConstraintName("FK_LookupHistory_Lookup");

                    b.Navigation("AspNetLookup");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupItem", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetLookup", "AspNetLookup")
                        .WithMany("AspNetLookupItem")
                        .HasForeignKey("LookupId")
                        .IsRequired()
                        .HasConstraintName("FK_LookupItem_Lookup");

                    b.Navigation("AspNetLookup");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupItemHistory", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetLookup", "AspNetLookup")
                        .WithMany("AspNetLookupItemHistory")
                        .HasForeignKey("LookupId")
                        .IsRequired()
                        .HasConstraintName("FK_LookupItemHistory_Lookup");

                    b.HasOne("IdentityServer6AspId.Models.AspNetLookupItem", "AspNetLookupItem")
                        .WithMany("AspNetLookupItemHistory")
                        .HasForeignKey("LookupItemId")
                        .IsRequired()
                        .HasConstraintName("FK_LookupItemHistory_LookupItem");

                    b.Navigation("AspNetLookup");

                    b.Navigation("AspNetLookupItem");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantConfig", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetLookupItem", "Application")
                        .WithMany("AspNetTenantConfig")
                        .HasForeignKey("ApplicationId")
                        .IsRequired();

                    b.HasOne("IdentityServer6AspId.Models.AspNetTenant", "AspNetTenant")
                        .WithMany("AspNetTenantConfig")
                        .HasForeignKey("TenantId")
                        .IsRequired()
                        .HasConstraintName("FK_TenantConfig_Tenant");

                    b.Navigation("Application");

                    b.Navigation("AspNetTenant");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantConfigHistory", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetLookupItem", "Application")
                        .WithMany("AspNetTenantConfigHistory")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("IdentityServer6AspId.Models.AspNetTenantConfig", "AspNetTenantConfig")
                        .WithMany("AspNetTenantConfigHistory")
                        .HasForeignKey("TenantConfigId")
                        .HasConstraintName("FK_TenantConfigHistory_TenantConfig");

                    b.HasOne("IdentityServer6AspId.Models.AspNetTenant", "AspNetTenant")
                        .WithMany("AspNetTenantConfigHistory")
                        .HasForeignKey("TenantId")
                        .HasConstraintName("FK_TenantConfigHistory_Tenant");

                    b.Navigation("Application");

                    b.Navigation("AspNetTenant");

                    b.Navigation("AspNetTenantConfig");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantHistory", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetTenant", "AspNetTenant")
                        .WithMany("AspNetTenantHistory")
                        .HasForeignKey("TenantId")
                        .IsRequired()
                        .HasConstraintName("FK_TenantHistory_Tenant");

                    b.Navigation("AspNetTenant");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantUser", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.AspNetTenant", "AspNetTenant")
                        .WithMany("AspNetTenantUser")
                        .HasForeignKey("FxTenantId")
                        .HasPrincipalKey("FxTenantId")
                        .IsRequired()
                        .HasConstraintName("FK_TenantUser_Tenant");

                    b.Navigation("AspNetTenant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityServer6AspId.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("IdentityServer6AspId.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookup", b =>
                {
                    b.Navigation("AspNetLookupHistory");

                    b.Navigation("AspNetLookupItem");

                    b.Navigation("AspNetLookupItemHistory");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetLookupItem", b =>
                {
                    b.Navigation("AspNetLookupItemHistory");

                    b.Navigation("AspNetTenantConfig");

                    b.Navigation("AspNetTenantConfigHistory");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenant", b =>
                {
                    b.Navigation("AspNetTenantConfig");

                    b.Navigation("AspNetTenantConfigHistory");

                    b.Navigation("AspNetTenantHistory");

                    b.Navigation("AspNetTenantUser");
                });

            modelBuilder.Entity("IdentityServer6AspId.Models.AspNetTenantConfig", b =>
                {
                    b.Navigation("AspNetTenantConfigHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
