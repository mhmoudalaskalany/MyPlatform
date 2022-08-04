using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddMurasalatEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HiringDate",
                table: "FullEmployees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MurasalatEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileNumber = table.Column<long>(nullable: true),
                    CivilNumber = table.Column<string>(nullable: true),
                    ArCurrentFirstName = table.Column<string>(nullable: true),
                    ArCurrentSecondName = table.Column<string>(nullable: true),
                    ArCurrentMiddleName = table.Column<string>(nullable: true),
                    ArCurrentLastName = table.Column<string>(nullable: true),
                    ArFullName = table.Column<string>(nullable: true),
                    EnCurrentFirstName = table.Column<string>(nullable: true),
                    EnCurrentSecondNamee = table.Column<string>(nullable: true),
                    EnCurrentMiddleName = table.Column<string>(nullable: true),
                    EnCurrentLastName = table.Column<string>(nullable: true),
                    EnFullName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ArPositiontype = table.Column<string>(nullable: true),
                    EnPositiontype = table.Column<string>(nullable: true),
                    DepartmentCode = table.Column<string>(nullable: true),
                    ArDepartmentName = table.Column<string>(nullable: true),
                    EnDepartmentName = table.Column<string>(nullable: true),
                    DirectManagerFileNumber = table.Column<long>(nullable: true),
                    DirectManagerCivilNumber = table.Column<string>(nullable: true),
                    ArDirectManagerName = table.Column<string>(nullable: true),
                    EnDirectManagerName = table.Column<string>(nullable: true),
                    ParentDepartmentCode = table.Column<string>(nullable: true),
                    ArParentDepartmentName = table.Column<string>(nullable: true),
                    EnParentDepartmentName = table.Column<string>(nullable: true),
                    GrandParentDepartmentCode = table.Column<string>(nullable: true),
                    ArGrandParentDepartmentName = table.Column<string>(nullable: true),
                    EnGrandParentDepartmentName = table.Column<string>(nullable: true),
                    GrandDepartmentCode = table.Column<string>(nullable: true),
                    ArGrandDepartmentName = table.Column<string>(nullable: true),
                    EnGrandDepartmentName = table.Column<string>(nullable: true),
                    HiringDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MurasalatEmployees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MurasalatEmployees");

            migrationBuilder.DropColumn(
                name: "HiringDate",
                table: "FullEmployees");
        }
    }
}
