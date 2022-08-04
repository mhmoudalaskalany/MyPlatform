using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ReCreateEmployeeTeamEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullUnitId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MurasalatEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedByEmployeeEn = table.Column<string>(nullable: true),
                    CreatedByEmployeeAr = table.Column<string>(nullable: true),
                    ModifiedByEmployeeEn = table.Column<string>(nullable: true),
                    ModifiedByEmployeeAr = table.Column<string>(nullable: true),
                    CreatedByEmployeeId = table.Column<string>(nullable: true),
                    ModifiedByEmployeeId = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    FileNumber = table.Column<string>(nullable: true),
                    CivilNumber = table.Column<string>(nullable: true),
                    ArCurrentFirstName = table.Column<string>(nullable: true),
                    ArCurrentSecondName = table.Column<string>(nullable: true),
                    ArCurrentMiddleName = table.Column<string>(nullable: true),
                    ArCurrentLastName = table.Column<string>(nullable: true),
                    ArFullName = table.Column<string>(nullable: true),
                    EnCurrentFirstName = table.Column<string>(nullable: true),
                    EnCurrentSecondNamee = table.Column<string>(nullable: true),
                    EnCurrentMiddleName = table.Column<string>(nullable: true),
                    EnCurrentLastName = table.Column<string>(nullable: true),
                    EnFullName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ArPositiontype = table.Column<string>(nullable: true),
                    EnPositiontype = table.Column<string>(nullable: true),
                    ArDepartmentName = table.Column<string>(nullable: true),
                    EnDepartmentName = table.Column<string>(nullable: true),
                    DirectManagerFileNumber = table.Column<string>(nullable: true),
                    DirectManagerCivilNumber = table.Column<string>(nullable: true),
                    ArDirectManagerName = table.Column<string>(nullable: true),
                    EnDirectManagerName = table.Column<string>(nullable: true),
                    ParentDepartmentCode = table.Column<string>(nullable: true),
                    ArParentDepartmentName = table.Column<string>(nullable: true),
                    EnParentDepartmentName = table.Column<string>(nullable: true),
                    GrandParentDepartmentCode = table.Column<string>(nullable: true),
                    ArGrandParentDepartmentName = table.Column<string>(nullable: true),
                    EnGrandParentDepartmentName = table.Column<string>(nullable: true),
                    GrandDepartmentCode = table.Column<string>(nullable: true),
                    ArGrandDepartmentName = table.Column<string>(nullable: true),
                    EnGrandDepartmentName = table.Column<string>(nullable: true),
                    HiringDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DirectManagerEmail = table.Column<string>(nullable: true),
                    EnDirectManagerPosition = table.Column<string>(nullable: true),
                    ArDirectManagerPosition = table.Column<string>(nullable: true),
                    IsManager = table.Column<bool>(nullable: false),
                    DepartmentCode = table.Column<string>(nullable: true),
                    PhotoId = table.Column<Guid>(nullable: true),
                    IpPhone = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "EmployeeTeams",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<long>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedByEmployeeEn = table.Column<string>(nullable: true),
                    CreatedByEmployeeAr = table.Column<string>(nullable: true),
                    ModifiedByEmployeeEn = table.Column<string>(nullable: true),
                    ModifiedByEmployeeAr = table.Column<string>(nullable: true),
                    CreatedByEmployeeId = table.Column<string>(nullable: true),
                    ModifiedByEmployeeId = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    IsTeamManager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTeams", x => new { x.EmployeeId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_EmployeeTeams_MurasalatEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "MurasalatEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FullUnitId",
                table: "Teams",
                column: "FullUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTeams_TeamId",
                table: "EmployeeTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatEmployees_DepartmentCode",
                table: "MurasalatEmployees",
                column: "DepartmentCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_FullUnits_FullUnitId",
                table: "Teams",
                column: "FullUnitId",
                principalTable: "FullUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_FullUnits_FullUnitId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "EmployeeTeams");

            migrationBuilder.DropTable(
                name: "MurasalatEmployees");

            migrationBuilder.DropIndex(
                name: "IX_Teams_FullUnitId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FullUnitId",
                table: "Teams");
        }
    }
}
