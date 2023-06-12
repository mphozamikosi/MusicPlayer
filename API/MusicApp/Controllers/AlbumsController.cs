using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicApp.RestCalls;
using MusicApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace TestFrontEndApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly RestCalls _res = new RestCalls();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlbumsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: AlbumsController
        public ActionResult Index()
        {
            var response = _res.GetAllAlbums();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Albums = JsonConvert.DeserializeObject<List<AlbumsViewModel>>(responseBody.ToString());
            return View(Albums);
        }
        public ActionResult Search(IFormCollection form)
        {
            var response = _res.GetAllAlbums(form["SearchText"]);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<AlbumsViewModel>>(responseBody.ToString());
            return View(artists);
        }
        // GET: AlbumsController/Details/5
        public ActionResult Details(int id)
        {
            var response = _res.GetAlbum(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Album = JsonConvert.DeserializeObject<AlbumsViewModel>(responseBody);
            return View(Album);
        }

        // GET: AlbumsController/Create
        public ActionResult Create()
        {
            var response = _res.GetAllArtists();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<ArtistsViewModel>>(responseBody.ToString());

            var responseGenres = _res.GetAllGenres();
            var responseBodyGenres = responseGenres.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var genres = JsonConvert.DeserializeObject<List<GenresViewModel>>(responseBodyGenres.ToString());

            ViewBag.Artists = new SelectList(artists, "Id", "ArtistName");
            ViewBag.Genres = new SelectList(genres, "Id", "GenreName");
            return View();
        }

        // POST: AlbumsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumsViewModel Album)
        {
            try
            {
                if (Album.CoverPhoto != null)
                {
                    string folder = "Images/";
                    folder += Guid.NewGuid().ToString() + "_" + Album.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    Album.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    Album.PhotoLocation = "/" + folder;
                }
                Album.ArtistId = Album.SelectedArtist;
                Album.GenreId = Album.SelectedGenre;
                _res.CreateAlbum(Album);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: AlbumsController/Edit/5
        public ActionResult Edit(int id)
        {
            var responseArtist = _res.GetAllArtists();
            var responseBodyArtist = responseArtist.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<ArtistsViewModel>>(responseBodyArtist.ToString());

            var response = _res.GetAlbum(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Album = JsonConvert.DeserializeObject<AlbumsViewModel>(responseBody);

            var responseGenres = _res.GetAllGenres();
            var responseBodyGenres = responseGenres.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var genres = JsonConvert.DeserializeObject<List<GenresViewModel>>(responseBodyGenres.ToString());

            ViewBag.Artists = new SelectList(artists, "Id", "ArtistName");
            ViewBag.Genres = new SelectList(genres, "Id", "GenreName");
            return View(Album);
        }

        // POST: AlbumsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlbumsViewModel Album)
        {
            try
            {
                if (Album.CoverPhoto != null)
                {
                    string folder = "Images/";
                    folder += Guid.NewGuid().ToString() + "_" + Album.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    Album.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    Album.PhotoLocation = "/" + folder;
                }
                Album.ArtistId = Album.SelectedArtist;
                Album.GenreId = Album.SelectedGenre;
                _res.UpdateAlbum(Album);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlbumsController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _res.GetAlbum(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Album = JsonConvert.DeserializeObject<AlbumsViewModel>(responseBody);
            return View(Album);
        }

        // POST: AlbumsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _res.DeleteAlbum(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}