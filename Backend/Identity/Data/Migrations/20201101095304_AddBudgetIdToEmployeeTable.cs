using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddBudgetIdToEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BudgetId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BudgetId",
                table: "Employees",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Budgets_BudgetId",
                table: "Employees",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Budgets_BudgetId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BudgetId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "Employees");
        }
    }
}
