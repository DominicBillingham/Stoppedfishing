using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoppedFishing.Migrations
{
    /// <inheritdoc />
    public partial class FixTimeblockColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SimpleBlocks_Users_UserId",
                table: "Users_SimpleBlocks");

            migrationBuilder.DropTable(
                name: "Users_TimeBlock");

            migrationBuilder.CreateTable(
                name: "Users_TimeBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimpleBlock = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_Users_TimeBlocks_UserId",
                table: "Users_TimeBlocks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SimpleBlocks_Users_UserId",
                table: "Users_SimpleBlocks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SimpleBlocks_Users_UserId",
                table: "Users_SimpleBlocks");

            migrationBuilder.DropTable(
                name: "Users_TimeBlocks");

            migrationBuilder.CreateTable(
                name: "Users_TimeBlock",
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
                    table.PrimaryKey("PK_Users_TimeBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_TimeBlock_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TimeBlock_UserId",
                table: "Users_TimeBlock",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SimpleBlocks_Users_UserId",
                table: "Users_SimpleBlocks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
