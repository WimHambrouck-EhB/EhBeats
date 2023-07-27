using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhBeats.Migrations
{
    public partial class PlaylistSongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Playlists_PlaylistId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Songs_SongId",
                table: "PlaylistSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong");

            migrationBuilder.RenameTable(
                name: "PlaylistSong",
                newName: "PlaylistSongs");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSong_SongId",
                table: "PlaylistSongs",
                newName: "IX_PlaylistSongs_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSong_PlaylistId",
                table: "PlaylistSongs",
                newName: "IX_PlaylistSongs_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSongs",
                table: "PlaylistSongs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSongs_Playlists_PlaylistId",
                table: "PlaylistSongs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSongs_Songs_SongId",
                table: "PlaylistSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSongs_Playlists_PlaylistId",
                table: "PlaylistSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSongs_Songs_SongId",
                table: "PlaylistSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSongs",
                table: "PlaylistSongs");

            migrationBuilder.RenameTable(
                name: "PlaylistSongs",
                newName: "PlaylistSong");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSongs_SongId",
                table: "PlaylistSong",
                newName: "IX_PlaylistSong_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSongs_PlaylistId",
                table: "PlaylistSong",
                newName: "IX_PlaylistSong_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Playlists_PlaylistId",
                table: "PlaylistSong",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Songs_SongId",
                table: "PlaylistSong",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
