
using EhBeats.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EhBeats.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [LastNameValidation]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{LastName}{(FirstName == null ? "" : ", " + FirstName)}";

        public ICollection<Song>? Songs { get; set; }
    }
}
