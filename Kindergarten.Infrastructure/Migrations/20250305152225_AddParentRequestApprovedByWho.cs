using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParentRequestApprovedByWho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InPersonApprovedByUserId",
                table: "ParentRequest",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnlineApprovedByUserId",
                table: "ParentRequest",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentRequest_InPersonApprovedByUserId",
                table: "ParentRequest",
                column: "InPersonApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentRequest_OnlineApprovedByUserId",
                table: "ParentRequest",
                column: "OnlineApprovedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentRequest_Users_InPersonApprovedByUserId",
                table: "ParentRequest",
                column: "InPersonApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentRequest_Users_OnlineApprovedByUserId",
                table: "ParentRequest",
                column: "OnlineApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentRequest_Users_InPersonApprovedByUserId",
                table: "ParentRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentRequest_Users_OnlineApprovedByUserId",
                table: "ParentRequest");

            migrationBuilder.DropIndex(
                name: "IX_ParentRequest_InPersonApprovedByUserId",
                table: "ParentRequest");

            migrationBuilder.DropIndex(
                name: "IX_ParentRequest_OnlineApprovedByUserId",
                table: "ParentRequest");

            migrationBuilder.DropColumn(
                name: "InPersonApprovedByUserId",
                table: "ParentRequest");

            migrationBuilder.DropColumn(
                name: "OnlineApprovedByUserId",
                table: "ParentRequest");
        }
    }
}
