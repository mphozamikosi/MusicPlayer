namespace MusicPlayerAPI.Models
{
    public class Genres
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        //public ICollection<Albums>? Albums { get; set; }
        //public ICollection<Songs>? Songs { get; set; }
    }
}
