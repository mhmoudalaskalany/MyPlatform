using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddEmployeeEmailToMurasalatEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirectManagerEmail",
                table: "MurasalatEmployees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MurasalatEmployees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectManagerEmail",
                table: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "MurasalatEmployees");
        }
    }
}
