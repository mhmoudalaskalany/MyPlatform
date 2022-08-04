using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DeleteMurasalatEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTeams_MurasalatEmployees_MurasalatEmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.DropTable(
                name: "MurasalatEmployees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTeams_MurasalatEmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.DropColumn(
                name: "MurasalatEmployeeId",
                table: "EmployeeTeams");

            migrationBuilder.AddColumn<string>(
                name: "NewUnitId",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewUnitId",
                table: "Teams");

            migrationBuilder.AddColumn<Guid>(
                name: "MurasalatEmployeeId",
                table: "EmployeeTeams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MurasalatEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArCurrentFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArCurrentLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArCurrentMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArCurrentSecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArDirectManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArDirectManagerPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArGrandDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArGrandParentDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArParentDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArPositiontype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CivilNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByEmployeeAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByEmployeeEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DirectManagerCivilNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectManagerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectManagerFileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnCurrentFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnCurrentLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnCurrentMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnCurrentSecondNamee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnDirectManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnDirectManagerPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnGrandDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnGrandParentDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnParentDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnPositiontype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrandDepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrandParentDepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedByEmployeeAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByEmployeeEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentDepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MurasalatEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MurasalatEmployees_FullUnits_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "FullUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeams_MurasalatEmployeeId",
                table: "EmployeeTeams",
                column: "MurasalatEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatEmployees_DepartmentCode",
                table: "MurasalatEmployees",
                column: "DepartmentCode");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTeams_MurasalatEmployees_MurasalatEmployeeId",
                table: "EmployeeTeams",
                column: "MurasalatEmployeeId",
                principalTable: "MurasalatEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
