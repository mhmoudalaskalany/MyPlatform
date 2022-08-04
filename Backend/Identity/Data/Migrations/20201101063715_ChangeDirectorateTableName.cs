using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeDirectorateTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Directorate_DirectorateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Directorate_DirectorateId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Directorate_Sectors_SectorId",
                table: "Directorate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directorate",
                table: "Directorate");

            migrationBuilder.RenameTable(
                name: "Directorate",
                newName: "Directorates");

            migrationBuilder.RenameIndex(
                name: "IX_Directorate_SectorId",
                table: "Directorates",
                newName: "IX_Directorates_SectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directorates",
                table: "Directorates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Directorates_DirectorateId",
                table: "AspNetUsers",
                column: "DirectorateId",
                principalTable: "Directorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Directorates_DirectorateId",
                table: "Departments",
                column: "DirectorateId",
                principalTable: "Directorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directorates_Sectors_SectorId",
                table: "Directorates",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Directorates_DirectorateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Directorates_DirectorateId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Directorates_Sectors_SectorId",
                table: "Directorates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directorates",
                table: "Directorates");

            migrationBuilder.RenameTable(
                name: "Directorates",
                newName: "Directorate");

            migrationBuilder.RenameIndex(
                name: "IX_Directorates_SectorId",
                table: "Directorate",
                newName: "IX_Directorate_SectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directorate",
                table: "Directorate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Directorate_DirectorateId",
                table: "AspNetUsers",
                column: "DirectorateId",
                principalTable: "Directorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Directorate_DirectorateId",
                table: "Departments",
                column: "DirectorateId",
                principalTable: "Directorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directorate_Sectors_SectorId",
                table: "Directorate",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
