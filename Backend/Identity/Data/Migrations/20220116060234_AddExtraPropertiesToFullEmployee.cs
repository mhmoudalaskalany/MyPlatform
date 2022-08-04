using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddExtraPropertiesToFullEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTeams_MurasalatEmployees_EmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentCode",
                table: "FullEmployees",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArDirectManagerPosition",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectManagerEmail",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnDirectManagerPosition",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpPhone",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "FullEmployees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedById",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MurasalatEmployeeId",
                table: "EmployeeTeams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FullEmployees_DepartmentCode",
                table: "FullEmployees",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeams_MurasalatEmployeeId",
                table: "EmployeeTeams",
                column: "MurasalatEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTeams_FullEmployees_EmployeeId",
                table: "EmployeeTeams",
                column: "EmployeeId",
                principalTable: "FullEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTeams_MurasalatEmployees_MurasalatEmployeeId",
                table: "EmployeeTeams",
                column: "MurasalatEmployeeId",
                principalTable: "MurasalatEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FullEmployees_FullUnits_DepartmentCode",
                table: "FullEmployees",
                column: "DepartmentCode",
                principalTable: "FullUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTeams_FullEmployees_EmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTeams_MurasalatEmployees_MurasalatEmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_FullEmployees_FullUnits_DepartmentCode",
                table: "FullEmployees");

            migrationBuilder.DropIndex(
                name: "IX_FullEmployees_DepartmentCode",
                table: "FullEmployees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTeams_MurasalatEmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.DropColumn(
                name: "ArDirectManagerPosition",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "DirectManagerEmail",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "EnDirectManagerPosition",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "IpPhone",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "FullEmployees");

            migrationBuilder.DropColumn(
                name: "MurasalatEmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentCode",
                table: "FullEmployees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTeams_MurasalatEmployees_EmployeeId",
                table: "EmployeeTeams",
                column: "EmployeeId",
                principalTable: "MurasalatEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
