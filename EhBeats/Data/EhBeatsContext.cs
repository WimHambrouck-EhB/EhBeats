using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EhBeats.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EhBeats.Data
{
    public class EhBeatsContext : IdentityDbContext<IdentityUser>
    {
        public EhBeatsContext(DbContextOptions<EhBeatsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>()
                .HasData(
                    new Artist { Id = 1, FirstName = "The", LastName = "Beatles" },
                    new Artist { Id = 2, FirstName = "The", LastName = "Rolling Stones" },
                    new Artist { Id = 3, LastName = "Aphex Twin" }
                );

            modelBuilder.Entity<Song>().
                HasData(
                    new Song { Id = 1, Title = "Yesterday", ArtistId = 1, BPM = 80, Danceability = Danceability.LOW, ReleaseDate = new DateTime(1965, 8, 6), Explicit = false },
                    new Song { Id = 2, Title = "Hey Jude", ArtistId = 1, BPM = 80, Danceability = Danceability.LOW, ReleaseDate = new DateTime(1968, 8, 26), Explicit = false },
                    new Song { Id = 3, Title = "Satisfaction", ArtistId = 2, BPM = 130, Danceability = Danceability.HIGH, ReleaseDate = new DateTime(1965, 6, 6), Explicit = false },
                    new Song { Id = 4, Title = "Windowlicker", ArtistId = 3, BPM = 130, Danceability = Danceability.HIGH, ReleaseDate = new DateTime(1990, 12, 6), Explicit = false }
                );

            modelBuilder.Entity<Playlist>()
                .HasData(
                    new Playlist { Id = 1, Name = "My Playlist", Description = "A playlist I made" },
                    new Playlist { Id = 2, Name = "My Other Playlist", Description = "Another playlist I made" }
                );

            modelBuilder.Entity<PlaylistSong>()
                .HasData(
                    new PlaylistSong { Id = 1, PlaylistId = 1, SongId = 1 },
                    new PlaylistSong { Id = 2, PlaylistId = 1, SongId = 2 },
                    new PlaylistSong { Id = 3, PlaylistId = 1, SongId = 3 }
                );
        }

        public DbSet<Song> Songs { get; set; } = default!;
        public DbSet<Artist> Artists { get; set; } = default!;
        public DbSet<Playlist> Playlists { get; set; } = default!;
        public DbSet<PlaylistSong> PlaylistSongs { get; set; } = default!;
    }
}
