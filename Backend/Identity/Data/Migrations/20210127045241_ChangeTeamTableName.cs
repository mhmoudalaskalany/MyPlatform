using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeTeamTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Units_UnitId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamEmployee_Employees_EmployeeId",
                table: "TeamEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamEmployee_Team_TeamId",
                table: "TeamEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamEmployee",
                table: "TeamEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.RenameTable(
                name: "TeamEmployee",
                newName: "TeamEmployees");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.RenameIndex(
                name: "IX_TeamEmployee_EmployeeId",
                table: "TeamEmployees",
                newName: "IX_TeamEmployees_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_UnitId",
                table: "Teams",
                newName: "IX_Teams_UnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamEmployees",
                table: "TeamEmployees",
                columns: new[] { "TeamId", "EmployeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEmployees_Employees_EmployeeId",
                table: "TeamEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEmployees_Teams_TeamId",
                table: "TeamEmployees",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Units_UnitId",
                table: "Teams",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamEmployees_Employees_EmployeeId",
                table: "TeamEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamEmployees_Teams_TeamId",
                table: "TeamEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Units_UnitId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamEmployees",
                table: "TeamEmployees");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.RenameTable(
                name: "TeamEmployees",
                newName: "TeamEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_UnitId",
                table: "Team",
                newName: "IX_Team_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamEmployees_EmployeeId",
                table: "TeamEmployee",
                newName: "IX_TeamEmployee_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamEmployee",
                table: "TeamEmployee",
                columns: new[] { "TeamId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Units_UnitId",
                table: "Team",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEmployee_Employees_EmployeeId",
                table: "TeamEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEmployee_Team_TeamId",
                table: "TeamEmployee",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
