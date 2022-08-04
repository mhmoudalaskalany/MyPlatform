using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddUnitTypeToUnitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Unit_UnitId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Budgets_BudgetId",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Unit_ParentId",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_ParentId",
                table: "Units",
                newName: "IX_Units_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_BudgetId",
                table: "Units",
                newName: "IX_Units_BudgetId");

            migrationBuilder.AddColumn<int>(
                name: "UnitType",
                table: "Units",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Units_UnitId",
                table: "Employees",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Budgets_BudgetId",
                table: "Units",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_ParentId",
                table: "Units",
                column: "ParentId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Units_UnitId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Budgets_BudgetId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_ParentId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Units");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameIndex(
                name: "IX_Units_ParentId",
                table: "Unit",
                newName: "IX_Unit_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_BudgetId",
                table: "Unit",
                newName: "IX_Unit_BudgetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Unit_UnitId",
                table: "Employees",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Budgets_BudgetId",
                table: "Unit",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Unit_ParentId",
                table: "Unit",
                column: "ParentId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
