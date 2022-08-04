using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddPersonIdToUserAndOldUnitIdToFullUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OldUnitId",
                table: "FullUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldUnitId",
                table: "FullUnits");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AspNetUsers");
        }
    }
}
