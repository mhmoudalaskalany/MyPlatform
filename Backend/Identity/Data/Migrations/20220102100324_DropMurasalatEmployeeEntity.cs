using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DropMurasalatEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MurasalatEmployees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    IsTeamManager = table.Column<bool>(type: "bit", nullable: true),
                    IsUpdated = table.Column<bool>(type: "bit", nullable: true),
                    ModifiedByEmployeeAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByEmployeeEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedByEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentDepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Retired = table.Column<bool>(type: "bit", nullable: true),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MurasalatEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MurasalatEmployees_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MurasalatEmployees_FullUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "FullUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatEmployees_TeamId",
                table: "MurasalatEmployees",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatEmployees_UnitId",
                table: "MurasalatEmployees",
                column: "UnitId");
        }
    }
}
