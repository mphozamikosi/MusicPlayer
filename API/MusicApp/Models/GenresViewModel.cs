using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class GenresViewModel
    {
        public int Id { get; set; }
        [Display(Name = "GENRE NAME")]
        [Required(ErrorMessage = "Please enter genre name")]
        public string GenreName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<AlbumsViewModel>? Albums { get; set; }
        public ICollection<SongsViewModel>? Songs { get; set; }
    }
}