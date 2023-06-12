using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class AlbumsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "ALBUM NAME")]
        [Required(ErrorMessage = "Please enter album name")]
        public string AlbumName { get; set; }
        public string SearchText { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        [Display(Name = "ARTIST")]
        [Required(ErrorMessage = "Please select artist")]
        public int SelectedArtist { get; set; }
        [Display(Name = "GENRE")]
        [Required(ErrorMessage = "Please select genre")]
        public int SelectedGenre { get; set; }
        [Required(ErrorMessage = "Please choose image")]
        public IFormFile CoverPhoto { get; set; }
        public string PhotoLocation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ArtistsViewModel Artist { get; set; }
        public GenresViewModel Genre { get; set; }
        public ICollection<SongsViewModel> Songs { get; set; }

    }
}
