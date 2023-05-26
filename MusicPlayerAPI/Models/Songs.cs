namespace MusicPlayerAPI.Models
{
    public class Songs
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public Albums? Album { get; set; }
        public Artists Artist { get; set; }
        public Genres Genre { get; set; }

    }
}
