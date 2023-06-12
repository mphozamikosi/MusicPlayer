using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class ArtistsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "ARTIST NAME")]
        [Required(ErrorMessage = "Please enter artist name")]
        public string ArtistName { get; set; }
        public string SearchText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<AlbumsViewModel>? Albums { get; set; }
    }
}