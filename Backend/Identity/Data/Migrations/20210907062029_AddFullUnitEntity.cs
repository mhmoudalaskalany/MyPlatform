using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddFullUnitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FullUnits",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    FullNameEn = table.Column<string>(nullable: true),
                    FullNameAr = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ParentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullUnits_FullUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FullUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FullUnits_ParentId",
                table: "FullUnits",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullUnits");
        }
    }
}
