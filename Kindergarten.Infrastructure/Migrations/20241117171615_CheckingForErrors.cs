using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckingForErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KindergartenDepartment_Departments_DepartmentId",
                table: "KindergartenDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_KindergartenDepartment_Kindergarten_KindergartenId",
                table: "KindergartenDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KindergartenDepartment",
                table: "KindergartenDepartment");

            migrationBuilder.DropIndex(
                name: "IX_KindergartenDepartment_DepartmentId",
                table: "KindergartenDepartment");

            migrationBuilder.RenameTable(
                name: "KindergartenDepartment",
                newName: "KindergartenDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KindergartenDepartments",
                table: "KindergartenDepartments",
                columns: new[] { "DepartmentId", "KindergartenId" });

            migrationBuilder.CreateIndex(
                name: "IX_KindergartenDepartments_KindergartenId",
                table: "KindergartenDepartments",
                column: "KindergartenId");

            migrationBuilder.AddForeignKey(
                name: "FK_KindergartenDepartments_Departments_DepartmentId",
                table: "KindergartenDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KindergartenDepartments_Kindergarten_KindergartenId",
                table: "KindergartenDepartments",
                column: "KindergartenId",
                principalTable: "Kindergarten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KindergartenDepartments_Departments_DepartmentId",
                table: "KindergartenDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_KindergartenDepartments_Kindergarten_KindergartenId",
                table: "KindergartenDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KindergartenDepartments",
                table: "KindergartenDepartments");

            migrationBuilder.DropIndex(
                name: "IX_KindergartenDepartments_KindergartenId",
                table: "KindergartenDepartments");

            migrationBuilder.RenameTable(
                name: "KindergartenDepartments",
                newName: "KindergartenDepartment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KindergartenDepartment",
                table: "KindergartenDepartment",
                columns: new[] { "KindergartenId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_KindergartenDepartment_DepartmentId",
                table: "KindergartenDepartment",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_KindergartenDepartment_Departments_DepartmentId",
                table: "KindergartenDepartment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KindergartenDepartment_Kindergarten_KindergartenId",
                table: "KindergartenDepartment",
                column: "KindergartenId",
                principalTable: "Kindergarten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
