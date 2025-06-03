using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToParentChildAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentChild",
                table: "ParentChild");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ParentChild",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentChild",
                table: "ParentChild",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ParentChild_ParentId_ChildId",
                table: "ParentChild",
                columns: new[] { "ParentId", "ChildId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentChild",
                table: "ParentChild");

            migrationBuilder.DropIndex(
                name: "IX_ParentChild_ParentId_ChildId",
                table: "ParentChild");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParentChild");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentChild",
                table: "ParentChild",
                columns: new[] { "ParentId", "ChildId" });
        }
    }
}
