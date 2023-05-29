using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.Interfaces
{
    public interface ISongs
    {
        #region Songs
        public Task<List<Songs>> GetSongs();
        public Task<Songs> GetSong(int id);
        public Songs GetSong(string SongName);
        public bool AddSong(Songs Songs);
        public bool UpdateSong(Songs Song);
        #endregion
    }
}
