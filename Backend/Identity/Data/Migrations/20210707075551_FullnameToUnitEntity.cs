using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FullnameToUnitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullNameAr",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullNameEn",
                table: "Units",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullNameAr",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "FullNameEn",
                table: "Units");
        }
    }
}
