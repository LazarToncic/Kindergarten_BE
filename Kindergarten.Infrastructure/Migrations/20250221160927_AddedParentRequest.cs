using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedParentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Parent",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "ParentRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "integer", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IsOnlineApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsInPersonApproved = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentRequest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parent_UserId",
                table: "Parent",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentRequest_UserId",
                table: "ParentRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_Users_UserId",
                table: "Parent",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parent_Users_UserId",
                table: "Parent");

            migrationBuilder.DropTable(
                name: "ParentRequest");

            migrationBuilder.DropIndex(
                name: "IX_Parent_UserId",
                table: "Parent");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Parent",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
