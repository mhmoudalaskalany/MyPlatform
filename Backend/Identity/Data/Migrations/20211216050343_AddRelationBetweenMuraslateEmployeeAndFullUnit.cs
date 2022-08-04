using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddRelationBetweenMuraslateEmployeeAndFullUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeAr",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeEn",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpPhone",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "MurasalatEmployees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTeamManager",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeAr",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeEn",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByEmployeeId",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedById",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Retired",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitType",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatEmployees_TeamId",
                table: "MurasalatEmployees",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatEmployees_UnitId",
                table: "MurasalatEmployees",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MurasalatEmployees_Teams_TeamId",
                table: "MurasalatEmployees",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MurasalatEmployees_FullUnits_UnitId",
                table: "MurasalatEmployees",
                column: "UnitId",
                principalTable: "FullUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MurasalatEmployees_Teams_TeamId",
                table: "MurasalatEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_MurasalatEmployees_FullUnits_UnitId",
                table: "MurasalatEmployees");

            migrationBuilder.DropIndex(
                name: "IX_MurasalatEmployees_TeamId",
                table: "MurasalatEmployees");

            migrationBuilder.DropIndex(
                name: "IX_MurasalatEmployees_UnitId",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeAr",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEn",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "IpPhone",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "IsTeamManager",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeAr",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeEn",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedByEmployeeId",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "Retired",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "FullUnits");
        }
    }
}
