using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class SongsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "SONG NAME")]
        [Required(ErrorMessage = "Please enter song name")]
        public string SongName { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        [Display(Name = "ALBUM")]
        [Required(ErrorMessage = "Please select album")]
        public int SelectedAlbum { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public AlbumsViewModel? Album { get; set; }

    }
}
