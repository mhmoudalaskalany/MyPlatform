using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddDirectorateIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DirectorateId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DirectorateId",
                table: "AspNetUsers",
                column: "DirectorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Directorate_DirectorateId",
                table: "AspNetUsers",
                column: "DirectorateId",
                principalTable: "Directorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Directorate_DirectorateId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DirectorateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DirectorateId",
                table: "AspNetUsers");
        }
    }
}
