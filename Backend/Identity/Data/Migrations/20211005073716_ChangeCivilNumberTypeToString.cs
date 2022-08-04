using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeCivilNumberTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileNumber",
                table: "MurasalatEmployees",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DirectManagerFileNumber",
                table: "MurasalatEmployees",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileNumber",
                table: "MurasalatEmployees",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DirectManagerFileNumber",
                table: "MurasalatEmployees",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
