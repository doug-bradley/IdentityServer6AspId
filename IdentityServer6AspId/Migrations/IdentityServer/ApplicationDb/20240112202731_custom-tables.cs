using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer6AspId.Migrations.IdentityServer.ApplicationDb
{
    public partial class customtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetLookup",
                columns: table => new
                {
                    LookupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Custom1Header = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    BuiltinID = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetLookup", x => x.LookupId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenant",
                columns: table => new
                {
                    TenantID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CancelledDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ForceUseAAD = table.Column<bool>(type: "bit", nullable: false),
                    FxTenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTenant", x => x.TenantID);
                    table.UniqueConstraint("AK_AspNetTenant_FxTenantID", x => x.FxTenantID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetLookupHistory",
                columns: table => new
                {
                    LookupHistoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupID = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Custom1Header = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    BuiltinID = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AuditAction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetLookupHistory", x => x.LookupHistoryID);
                    table.ForeignKey(
                        name: "FK_LookupHistory_Lookup",
                        column: x => x.LookupID,
                        principalTable: "AspNetLookup",
                        principalColumn: "LookupId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetLookupItem",
                columns: table => new
                {
                    LookupItemID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupID = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Custom1 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetLookupItem", x => x.LookupItemID);
                    table.ForeignKey(
                        name: "FK_LookupItem_Lookup",
                        column: x => x.LookupID,
                        principalTable: "AspNetLookup",
                        principalColumn: "LookupId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenantHistory",
                columns: table => new
                {
                    TenantHistoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ForceUseAAD = table.Column<bool>(type: "bit", nullable: true),
                    FxTenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AuditAction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTenantHistory", x => x.TenantHistoryID);
                    table.ForeignKey(
                        name: "FK_TenantHistory_Tenant",
                        column: x => x.TenantID,
                        principalTable: "AspNetTenant",
                        principalColumn: "TenantID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenantUser",
                columns: table => new
                {
                    TenantUserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FxTenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FxUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LoginUserID = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LoginUsername = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTenantUser", x => x.TenantUserID);
                    table.ForeignKey(
                        name: "FK_TenantUser_Tenant",
                        column: x => x.FxTenantID,
                        principalTable: "AspNetTenant",
                        principalColumn: "FxTenantID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetLookupItemHistory",
                columns: table => new
                {
                    LookupItemHistoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookupItemID = table.Column<long>(type: "bigint", nullable: false),
                    LookupID = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Custom1 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AuditAction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetLookupItemHistory", x => x.LookupItemHistoryID);
                    table.ForeignKey(
                        name: "FK_LookupItemHistory_Lookup",
                        column: x => x.LookupID,
                        principalTable: "AspNetLookup",
                        principalColumn: "LookupId");
                    table.ForeignKey(
                        name: "FK_LookupItemHistory_LookupItem",
                        column: x => x.LookupItemID,
                        principalTable: "AspNetLookupItem",
                        principalColumn: "LookupItemID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenantConfig",
                columns: table => new
                {
                    TenantConfigID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<long>(type: "bigint", nullable: false),
                    SiteType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ApiURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DataSource = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    InitialCatalog = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CustomLogo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CustomStyleSheet = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsInitialized = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    ExtApiURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTenantConfig", x => x.TenantConfigID);
                    table.ForeignKey(
                        name: "FK_AspNetTenantConfig_AspNetLookupItem_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "AspNetLookupItem",
                        principalColumn: "LookupItemID");
                    table.ForeignKey(
                        name: "FK_TenantConfig_Tenant",
                        column: x => x.TenantID,
                        principalTable: "AspNetTenant",
                        principalColumn: "TenantID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenantConfigHistory",
                columns: table => new
                {
                    TenantConfigHistoryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantConfigID = table.Column<long>(type: "bigint", nullable: true),
                    TenantID = table.Column<long>(type: "bigint", nullable: true),
                    SiteType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ApplicationID = table.Column<long>(type: "bigint", nullable: true),
                    WebsiteURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ApiURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DataSource = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    InitialCatalog = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CustomLogo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CustomStyleSheet = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsInitialized = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    AuditDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurrentUser = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AuditAction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExtApiURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTenantConfigHistory", x => x.TenantConfigHistoryID);
                    table.ForeignKey(
                        name: "FK_AspNetTenantConfigHistory_AspNetLookupItem_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "AspNetLookupItem",
                        principalColumn: "LookupItemID");
                    table.ForeignKey(
                        name: "FK_TenantConfigHistory_Tenant",
                        column: x => x.TenantID,
                        principalTable: "AspNetTenant",
                        principalColumn: "TenantID");
                    table.ForeignKey(
                        name: "FK_TenantConfigHistory_TenantConfig",
                        column: x => x.TenantConfigID,
                        principalTable: "AspNetTenantConfig",
                        principalColumn: "TenantConfigID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetLookup1",
                table: "AspNetLookup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetLookup2",
                table: "AspNetLookup",
                column: "BuiltinID",
                unique: true,
                filter: "([BuiltinID] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_LookupHistory1",
                table: "AspNetLookupHistory",
                column: "LookupID");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItem1",
                table: "AspNetLookupItem",
                columns: new[] { "LookupID", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupItemHistory1",
                table: "AspNetLookupItemHistory",
                column: "LookupItemID");

            migrationBuilder.CreateIndex(
                name: "IX_LookupItemHistory2",
                table: "AspNetLookupItemHistory",
                column: "LookupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant1",
                table: "AspNetTenant",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant2",
                table: "AspNetTenant",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant4",
                table: "AspNetTenant",
                column: "FxTenantID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetTenantConfig_ApplicationID",
                table: "AspNetTenantConfig",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetTenantConfig_TenantID",
                table: "AspNetTenantConfig",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantConfig1",
                table: "AspNetTenantConfig",
                columns: new[] { "IsActive", "IsInitialized", "TenantID", "SiteType", "ApplicationID" },
                unique: true,
                filter: "[IsActive] IS NOT NULL AND [IsInitialized] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetTenantConfigHistory_ApplicationID",
                table: "AspNetTenantConfigHistory",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantConfigHistory1",
                table: "AspNetTenantConfigHistory",
                column: "TenantConfigID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantConfigHistory2",
                table: "AspNetTenantConfigHistory",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantHistory1",
                table: "AspNetTenantHistory",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetTenantUser_FxTenantID",
                table: "AspNetTenantUser",
                column: "FxTenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUser1",
                table: "AspNetTenantUser",
                columns: new[] { "FxUserID", "FxTenantID", "LoginProvider", "LoginUserID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetLookupHistory");

            migrationBuilder.DropTable(
                name: "AspNetLookupItemHistory");

            migrationBuilder.DropTable(
                name: "AspNetTenantConfigHistory");

            migrationBuilder.DropTable(
                name: "AspNetTenantHistory");

            migrationBuilder.DropTable(
                name: "AspNetTenantUser");

            migrationBuilder.DropTable(
                name: "AspNetTenantConfig");

            migrationBuilder.DropTable(
                name: "AspNetLookupItem");

            migrationBuilder.DropTable(
                name: "AspNetTenant");

            migrationBuilder.DropTable(
                name: "AspNetLookup");
        }
    }
}
