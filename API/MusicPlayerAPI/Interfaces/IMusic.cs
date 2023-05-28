using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.Interfaces
{
    public interface IMusic
    {
        #region Artists
        public List<Artists> GetArtists();
        public Artists GetArtist(int id);
        public Artists GetArtist(string artistName);
        public bool AddArtist(string artistName);
        #endregion

        #region Albums
        public List<Albums> GetAlbums();
        public Albums GetAlbum(int id);
        public Albums GetAlbum(string albumName);
        public bool AddAlbum(string albumName);
        #endregion

        #region Songs
        public List<Songs> GetSongsForAlbum(int albumId);
        public Songs GetSong(int id);
        public Songs GetSong(string songName);
        public bool AddSongToAlbum(string songName);
        #endregion

    }
}
