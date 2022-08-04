using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemoveOldUnitIdFromTeamEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_FullUnits_FullUnitId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Units_UnitId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_FullUnitId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FullUnitId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "NewUnitId",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "UnitId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_FullUnits_UnitId",
                table: "Teams",
                column: "UnitId",
                principalTable: "FullUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_FullUnits_UnitId",
                table: "Teams");

            migrationBuilder.AlterColumn<long>(
                name: "UnitId",
                table: "Teams",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullUnitId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewUnitId",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FullUnitId",
                table: "Teams",
                column: "FullUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_FullUnits_FullUnitId",
                table: "Teams",
                column: "FullUnitId",
                principalTable: "FullUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Units_UnitId",
                table: "Teams",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
