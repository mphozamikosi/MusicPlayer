using Microsoft.AspNetCore.Mvc;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.Interfaces
{
    public interface IArtists
    {
        #region Artists
        public Task<List<Artists>> GetArtists();
        public Task<Artists> GetArtist(int id);
        public Artists GetArtist(string artistName);
        public bool AddArtist(Artists artists);
        public bool UpdateArtist(Artists artist);
        public Task<List<Artists>> SearchArtists(string artistName);
        #endregion

    }
}