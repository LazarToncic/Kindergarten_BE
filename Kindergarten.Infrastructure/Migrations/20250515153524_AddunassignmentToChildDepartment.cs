using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddunassignmentToChildDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UnassignedAt",
                table: "ChildDepartmentAssignments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnassignedByUserId",
                table: "ChildDepartmentAssignments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnassignedAt",
                table: "ChildDepartmentAssignments");

            migrationBuilder.DropColumn(
                name: "UnassignedByUserId",
                table: "ChildDepartmentAssignments");
        }
    }
}
