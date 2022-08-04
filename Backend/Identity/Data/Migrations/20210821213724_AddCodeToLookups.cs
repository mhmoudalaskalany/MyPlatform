using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddCodeToLookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Nationalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Budgets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Budgets");
        }
    }
}
