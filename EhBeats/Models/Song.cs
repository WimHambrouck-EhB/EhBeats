using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EhBeats.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        [Range(40, 200)]
        public uint? BPM { get; set; }
        public Danceability Danceability { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public bool Explicit { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
        public ICollection<PlaylistSong>? Playlists { get; set; }

    }
}
