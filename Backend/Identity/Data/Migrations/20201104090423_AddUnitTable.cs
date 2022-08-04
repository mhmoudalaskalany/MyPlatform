using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddUnitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    BudgetId = table.Column<long>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    ParentUnitId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unit_Unit_ParentUnitId",
                        column: x => x.ParentUnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UnitId",
                table: "Employees",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BudgetId",
                table: "Unit",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentUnitId",
                table: "Unit",
                column: "ParentUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Unit_UnitId",
                table: "Employees",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Unit_UnitId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UnitId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Employees");
        }
    }
}
