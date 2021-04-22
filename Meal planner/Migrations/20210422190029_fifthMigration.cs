using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meal_planner.Migrations
{
    public partial class fifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Categories_CategoryId1",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_CategoryId1",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipe");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CategoryId",
                table: "Recipe",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Categories_CategoryId",
                table: "Recipe",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Categories_CategoryId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_CategoryId",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Recipe",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipe",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CategoryId1",
                table: "Recipe",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Categories_CategoryId1",
                table: "Recipe",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
