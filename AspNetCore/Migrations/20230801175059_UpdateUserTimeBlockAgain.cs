using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoppedFishing.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTimeBlockAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users_SimpleBlocks");

            migrationBuilder.DropTable(
                name: "Users_TimeBlocks");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TimeBlocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SimpleTimeBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimpleBlock = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleTimeBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimpleTimeBlocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeBlocks_UserId",
                table: "TimeBlocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleTimeBlocks_UserId",
                table: "SimpleTimeBlocks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeBlocks_Users_UserId",
                table: "TimeBlocks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeBlocks_Users_UserId",
                table: "TimeBlocks");

            migrationBuilder.DropTable(
                name: "SimpleTimeBlocks");

            migrationBuilder.DropIndex(
                name: "IX_TimeBlocks_UserId",
                table: "TimeBlocks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeBlocks");

            migrationBuilder.CreateTable(
                name: "Users_SimpleBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    SimpleBlock = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_SimpleBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_SimpleBlocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_TimeBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    SimpleBlock = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_TimeBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_TimeBlocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SimpleBlocks_UserId",
                table: "Users_SimpleBlocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TimeBlocks_UserId",
                table: "Users_TimeBlocks",
                column: "UserId");
        }
    }
}
