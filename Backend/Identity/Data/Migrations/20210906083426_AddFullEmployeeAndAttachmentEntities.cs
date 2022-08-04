using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddFullEmployeeAndAttachmentEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FileId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    AttachmentDisplaySize = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    EmployeeId1 = table.Column<long>(nullable: true),
                    FullEmployeeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FullEmployees",
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
                    IsVaccinated = table.Column<bool>(nullable: false),
                    DoseStatus = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    AttachmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullEmployees_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EmployeeId1",
                table: "Attachments",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_FullEmployees_AttachmentId",
                table: "FullEmployees",
                column: "AttachmentId",
                unique: true,
                filter: "[AttachmentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullEmployees");

            migrationBuilder.DropTable(
                name: "Attachments");
        }
    }
}
