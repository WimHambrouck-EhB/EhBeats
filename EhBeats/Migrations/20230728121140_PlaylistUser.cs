using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhBeats.Migrations
{
    public partial class PlaylistUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Playlists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_IdentityUserId",
                table: "Playlists",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_IdentityUserId",
                table: "Playlists",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_IdentityUserId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_IdentityUserId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Playlists");
        }
    }
}
