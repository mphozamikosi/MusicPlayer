namespace MusicPlayerAPI.Models
{
    public class Songs
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Albums? Album { get; set; }

    }
}