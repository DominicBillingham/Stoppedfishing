using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoppedFishing.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHourColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startHour",
                table: "Meetings",
                newName: "StartHour");

            migrationBuilder.RenameColumn(
                name: "endHour",
                table: "Meetings",
                newName: "EndHour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartHour",
                table: "Meetings",
                newName: "startHour");

            migrationBuilder.RenameColumn(
                name: "EndHour",
                table: "Meetings",
                newName: "endHour");
        }
    }
}
