using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddDirectManagerPoistionToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArDirectManagerPosition",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnDirectManagerPosition",
                table: "MurasalatEmployees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArDirectManagerPosition",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "EnDirectManagerPosition",
                table: "MurasalatEmployees");
        }
    }
}
