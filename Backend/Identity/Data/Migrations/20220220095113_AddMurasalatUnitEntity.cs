using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddMurasalatUnitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MurasalatUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedByEmployeeEn = table.Column<string>(nullable: true),
                    CreatedByEmployeeAr = table.Column<string>(nullable: true),
                    ModifiedByEmployeeEn = table.Column<string>(nullable: true),
                    ModifiedByEmployeeAr = table.Column<string>(nullable: true),
                    CreatedByEmployeeId = table.Column<string>(nullable: true),
                    ModifiedByEmployeeId = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ParentCode = table.Column<string>(nullable: true),
                    UnitType = table.Column<int>(nullable: false),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    FullNameEn = table.Column<string>(nullable: true),
                    FullNameAr = table.Column<string>(nullable: true),
                    HierarchyPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MurasalatUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MurasalatUnits_MurasalatUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MurasalatUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MurasalatUnits_ParentId",
                table: "MurasalatUnits",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MurasalatUnits");
        }
    }
}
