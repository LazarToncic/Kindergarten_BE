using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddlengthOnQualificationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentEmployee_Departments_DepartmentId",
                table: "DepartmentEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentEmployee_Employees_EmployeeId",
                table: "DepartmentEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentEmployee",
                table: "DepartmentEmployee");

            migrationBuilder.RenameTable(
                name: "DepartmentEmployee",
                newName: "DepartmentEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentEmployee_EmployeeId",
                table: "DepartmentEmployees",
                newName: "IX_DepartmentEmployees_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Qualification",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentEmployees",
                table: "DepartmentEmployees",
                columns: new[] { "DepartmentId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentEmployees_Departments_DepartmentId",
                table: "DepartmentEmployees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentEmployees_Employees_EmployeeId",
                table: "DepartmentEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentEmployees_Departments_DepartmentId",
                table: "DepartmentEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentEmployees_Employees_EmployeeId",
                table: "DepartmentEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentEmployees",
                table: "DepartmentEmployees");

            migrationBuilder.RenameTable(
                name: "DepartmentEmployees",
                newName: "DepartmentEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentEmployees_EmployeeId",
                table: "DepartmentEmployee",
                newName: "IX_DepartmentEmployee_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Qualification",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentEmployee",
                table: "DepartmentEmployee",
                columns: new[] { "DepartmentId", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentEmployee_Departments_DepartmentId",
                table: "DepartmentEmployee",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentEmployee_Employees_EmployeeId",
                table: "DepartmentEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
