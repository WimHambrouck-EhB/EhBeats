using System.ComponentModel.DataAnnotations;

namespace EhBeats.Models
{
    public enum Danceability
    {
        [Display(Name = "Unknown")]
        UNKNOWN,
        [Display(Name = "Low")]
        LOW,
        [Display(Name = "Medium")]
        MEDIUM,
        [Display(Name = "High")]
        HIGH
    }
}