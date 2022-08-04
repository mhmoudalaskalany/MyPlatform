using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemoveBudgetIdFromDirectorates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directorates_Budgets_BudgetId",
                table: "Directorates");

            migrationBuilder.DropIndex(
                name: "IX_Directorates_BudgetId",
                table: "Directorates");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "Directorates");

            migrationBuilder.AddColumn<long>(
                name: "DirectorateId",
                table: "Budgets",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "BudgetId",
                table: "Directorates",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directorates_BudgetId",
                table: "Directorates",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directorates_Budgets_BudgetId",
                table: "Directorates",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
