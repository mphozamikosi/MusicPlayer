using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.Interfaces
{
    public interface IGenres
    {
        #region Genres
        public Task<List<Genres>> GetGenres();
        public Task<Genres> GetGenre(int id);
        public Genres GetGenre(string GenreName);
        public bool AddGenre(Genres Genres);
        public bool UpdateGenre(Genres Genre);
        #endregion


    }
}
