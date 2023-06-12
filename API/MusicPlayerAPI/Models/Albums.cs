namespace MusicPlayerAPI.Models
{
    public class Albums
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public string PhotoLocation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Artists Artist { get; set; }
        public Genres Genre { get; set; }
        public ICollection<Songs> Songs { get; set; }

    }
}