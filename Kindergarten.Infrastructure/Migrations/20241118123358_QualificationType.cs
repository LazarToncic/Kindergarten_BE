using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kindergarten.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QualificationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QualificationTypeId",
                table: "Qualification",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "QualificationObtained",
                table: "EmployeeQualifications",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "QualificationType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_QualificationTypeId",
                table: "Qualification",
                column: "QualificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_QualificationType_QualificationTypeId",
                table: "Qualification",
                column: "QualificationTypeId",
                principalTable: "QualificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_QualificationType_QualificationTypeId",
                table: "Qualification");

            migrationBuilder.DropTable(
                name: "QualificationType");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_QualificationTypeId",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "QualificationTypeId",
                table: "Qualification");

            migrationBuilder.AlterColumn<DateTime>(
                name: "QualificationObtained",
                table: "EmployeeQualifications",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
