using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkactivityApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTimeColumnMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time_StartTime",
                table: "Projects",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "Time_EndTime",
                table: "Projects",
                newName: "EndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Projects",
                newName: "Time_StartTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Projects",
                newName: "Time_EndTime");
        }
    }
}
