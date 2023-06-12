using MusicApp.Models;

namespace MusicApp.RestCalls
{
    public class RestCalls
    {
        #region Artists
        public HttpResponseMessage GetAllArtists()
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Artists")
                .Result;
        }
        public HttpResponseMessage GetAllArtists(string searchText)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Artists/SearchArtists?artistName=" + searchText)
                .Result;
        }
        public HttpResponseMessage GetArtist(int id)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Artists/GetArtist?id=" + id)
                .Result;
        }
        public HttpResponseMessage CreateArtist(ArtistsViewModel artist)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Artists/CreateArtist", artist)
                .Result;
        }
        public HttpResponseMessage UpdateArtist(ArtistsViewModel artist)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Artists/EditArtist", artist)
                .Result;
        }
        public HttpResponseMessage DeleteArtist(int id)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Artists/DeleteArtist", id)
                .Result;
        }
        #endregion

        #region Genres
        public HttpResponseMessage GetAllGenres()
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Genres")
                .Result;
        }
        public HttpResponseMessage GetAllGenres(string searchText)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Genres/SearchGenres?genreName=" + searchText)
                .Result;
        }
        public HttpResponseMessage GetGenre(int id)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Genres/GetGenre?id=" + id)
                .Result;
        }
        public HttpResponseMessage CreateGenre(GenresViewModel Genre)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Genres/AddGenre", Genre)
                .Result;
        }
        public HttpResponseMessage UpdateGenre(GenresViewModel Genre)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Genres/EditGenre", Genre)
                .Result;
        }
        public HttpResponseMessage DeleteGenre(int id)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Genres/DeleteGenre", id)
                .Result;
        }
        #endregion

        #region Albums
        public HttpResponseMessage GetAllAlbums()
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Albums")
                .Result;
        }
        public HttpResponseMessage GetAllAlbums(string searchText)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Albums/SearchAlbums?albumName=" + searchText)
                .Result;
        }
        public HttpResponseMessage GetAlbum(int id)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Albums/GetAlbum?id=" + id)
                .Result;
        }
        public HttpResponseMessage CreateAlbum(AlbumsViewModel Album)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Albums/AddAlbum", Album)
                .Result;
        }
        public HttpResponseMessage UpdateAlbum(AlbumsViewModel Album)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Albums/EditAlbum", Album)
                .Result;
        }
        public HttpResponseMessage DeleteAlbum(int id)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Albums/DeleteAlbum", id)
                .Result;
        }
        #endregion

        #region Songs
        public HttpResponseMessage GetAllSongs()
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Songs")
                .Result;
        }
        public HttpResponseMessage GetAllSongs(string searchText)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Songs/SearchSongs?songName=" + searchText)
                .Result;
        }
        public HttpResponseMessage GetSong(int id)
        {
            return GlobalVariable.httpClient.GetAsync(GlobalVariable.httpClient.BaseAddress + "api/Songs/GetSong?id=" + id)
                .Result;
        }
        public HttpResponseMessage CreateSong(SongsViewModel Song)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Songs/CreateSong", Song)
                .Result;
        }
        public HttpResponseMessage UpdateSong(SongsViewModel Song)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Songs/EditSong", Song)
                .Result;
        }
        public HttpResponseMessage DeleteSong(int id)
        {
            return GlobalVariable.httpClient.PostAsJsonAsync(GlobalVariable.httpClient.BaseAddress + "api/Songs/DeleteSong", id)
                .Result;
        }
        #endregion
    }
}
