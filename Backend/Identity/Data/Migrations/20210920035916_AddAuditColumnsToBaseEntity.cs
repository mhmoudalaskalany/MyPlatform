using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddAuditColumnsToBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "UserApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "TemplateForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "RolePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "PagePermissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "LoginHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "LoginHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "LoginHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "LoginHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "LoginHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "LoginHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "EmployeeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "AuditTrails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Attachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "Apps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "Apps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "UserApps");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "TemplateForms");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "PagePermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "LoginHistories");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "LoginHistories");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "LoginHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "LoginHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "LoginHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "LoginHistories");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "EmployeeType");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "Apps");
        }
    }
}
