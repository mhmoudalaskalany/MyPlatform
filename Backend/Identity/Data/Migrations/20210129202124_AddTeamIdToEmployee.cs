using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddTeamIdToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamEmployees");

            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamId",
                table: "Employees",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TeamId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "TeamEmployees",
                columns: table => new
                {
                    TeamId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamEmployees", x => new { x.TeamId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_TeamEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamEmployees_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamEmployees_EmployeeId",
                table: "TeamEmployees",
                column: "EmployeeId");
        }
    }
}
