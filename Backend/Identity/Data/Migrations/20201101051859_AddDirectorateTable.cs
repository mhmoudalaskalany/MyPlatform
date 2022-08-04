using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddDirectorateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Sectors_SectorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SectorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Departments");

            migrationBuilder.AddColumn<long>(
                name: "DirectorateId",
                table: "Departments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Directorate",
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
                    SectorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directorate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directorate_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DirectorateId",
                table: "Departments",
                column: "DirectorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Directorate_SectorId",
                table: "Directorate",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Directorate_DirectorateId",
                table: "Departments",
                column: "DirectorateId",
                principalTable: "Directorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Directorate_DirectorateId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Directorate");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DirectorateId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DirectorateId",
                table: "Departments");

            migrationBuilder.AddColumn<long>(
                name: "SectorId",
                table: "Departments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SectorId",
                table: "Departments",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Sectors_SectorId",
                table: "Departments",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
