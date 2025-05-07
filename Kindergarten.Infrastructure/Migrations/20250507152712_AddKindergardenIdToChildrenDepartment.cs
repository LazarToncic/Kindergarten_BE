using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKindergardenIdToChildrenDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "KindergartenId",
                table: "ChildDepartmentAssignments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChildDepartmentAssignments_KindergartenId",
                table: "ChildDepartmentAssignments",
                column: "KindergartenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildDepartmentAssignments_Kindergarten_KindergartenId",
                table: "ChildDepartmentAssignments",
                column: "KindergartenId",
                principalTable: "Kindergarten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildDepartmentAssignments_Kindergarten_KindergartenId",
                table: "ChildDepartmentAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ChildDepartmentAssignments_KindergartenId",
                table: "ChildDepartmentAssignments");

            migrationBuilder.DropColumn(
                name: "KindergartenId",
                table: "ChildDepartmentAssignments");
        }
    }
}
