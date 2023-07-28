using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhBeats.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "The", "Beatles" },
                    { 2, "The", "Rolling Stones" },
                    { 3, null, "Aphex Twin" }
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A playlist I made", "My Playlist" },
                    { 2, "Another playlist I made", "My Other Playlist" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "ArtistId", "BPM", "Danceability", "Explicit", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, 80L, 1, false, new DateTime(1965, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yesterday" },
                    { 2, 1, 80L, 1, false, new DateTime(1968, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hey Jude" }
                });

            migrationBuilder.InsertData(
                table: "PlaylistSong",
                columns: new[] { "Id", "PlaylistId", "SongId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "ArtistId", "BPM", "Danceability", "Explicit", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 3, 2, 130L, 3, false, new DateTime(1965, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Satisfaction" },
                    { 4, 3, 130L, 3, false, new DateTime(1990, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Windowlicker" }
                });

            migrationBuilder.InsertData(
                table: "PlaylistSong",
                columns: new[] { "Id", "PlaylistId", "SongId" },
                values: new object[] { 3, 1, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlaylistSong",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlaylistSong",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlaylistSong",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Playlists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
