using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class BudgetFromDirectorate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Directorates_DirectorateId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_DirectorateId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "DirectorateId",
                table: "Budgets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DirectorateId",
                table: "Budgets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_DirectorateId",
                table: "Budgets",
                column: "DirectorateId",
                unique: true,
                filter: "[DirectorateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Directorates_DirectorateId",
                table: "Budgets",
                column: "DirectorateId",
                principalTable: "Directorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
