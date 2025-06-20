using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkactivityApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Times_TimeId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TimeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Projects");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time_EndTime",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time_StartTime",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time_EndTime",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Time_StartTime",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TimeId",
                table: "Projects",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Times_TimeId",
                table: "Projects",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
