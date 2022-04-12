using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddPropertyToBookmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_AspNetUsers_UserId",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_UserId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BookmarkUser",
                table: "Bookmark",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookmarkUser",
                table: "Bookmark");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bookmark",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_UserId",
                table: "Bookmark",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_AspNetUsers_UserId",
                table: "Bookmark",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
