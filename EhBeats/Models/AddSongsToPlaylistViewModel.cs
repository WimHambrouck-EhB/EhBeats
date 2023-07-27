using System.ComponentModel.DataAnnotations;

namespace EhBeats.Models
{
    public class AddSongsToPlaylistViewModel
    {
        [Required]
        public int? ArtistId { get; set; }

        public int PlaylistId { get; set; }
        public Dictionary<Song, bool>? Songs { get; set; }
    }
}
