using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.Interfaces
{
    public interface IAlbums
    {
        #region Albums
        public Task<List<Albums>> GetAlbums();
        public Task<Albums> GetAlbum(int id);
        public Albums GetAlbum(string AlbumName);
        public bool AddAlbum(Albums Albums);
        public bool UpdateAlbum(Albums Album);
        #endregion
    }
}
