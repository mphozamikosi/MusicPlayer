namespace MusicPlayerAPI.Models
{
    public class Artists
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Albums>? Albums { get; set; }
        public ICollection<Songs>? Songs { get; set; }
    }
}
