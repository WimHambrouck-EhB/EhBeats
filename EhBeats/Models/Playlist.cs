using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EhBeats.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        [Display(Name = "Playlist")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public ICollection<PlaylistSong>? Songs { get; set; }

        public string? IdentityUserId { get; set; }

        public IdentityUser? IdentityUser{ get; set; }
    }
}
