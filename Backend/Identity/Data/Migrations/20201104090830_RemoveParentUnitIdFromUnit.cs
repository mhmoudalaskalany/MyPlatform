using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemoveParentUnitIdFromUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Unit_ParentUnitId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ParentUnitId",
                table: "Unit");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentId",
                table: "Unit",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Unit_ParentId",
                table: "Unit",
                column: "ParentId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Unit_ParentId",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ParentId",
                table: "Unit");

            migrationBuilder.AddColumn<long>(
                name: "ParentUnitId",
                table: "Unit",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
