using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.RestCalls;
using MusicApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestFrontEndApp.Controllers
{
    public class SongsController : Controller
    {
        private readonly RestCalls _res = new RestCalls();
        // GET: SongsController
        public ActionResult Index()
        {
            var response = _res.GetAllSongs();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Songs = JsonConvert.DeserializeObject<List<SongsViewModel>>(responseBody.ToString());
            return View(Songs);
        }
        public ActionResult Search(IFormCollection form)
        {
            var response = _res.GetAllSongs(form["SearchText"]);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<SongsViewModel>>(responseBody.ToString());
            return View(artists);
        }
        // GET: SongsController/Details/5
        public ActionResult Details(int id)
        {
            var response = _res.GetSong(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Song = JsonConvert.DeserializeObject<SongsViewModel>(responseBody);
            return View(Song);
        }

        // GET: SongsController/Create
        public ActionResult Create()
        {
            var response = _res.GetAllAlbums();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Albums = JsonConvert.DeserializeObject<List<AlbumsViewModel>>(responseBody.ToString());

            ViewBag.Albums = new SelectList(Albums, "Id", "AlbumName");
            return View();
        }

        // POST: SongsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongsViewModel Song)
        {
            Song.AlbumId = Song.SelectedAlbum;
            try
            {
                _res.CreateSong(Song);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SongsController/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _res.GetSong(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Song = JsonConvert.DeserializeObject<SongsViewModel>(responseBody);

            var responseAlbum = _res.GetAllAlbums();
            var responseBodyAlbum = responseAlbum.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Albums = JsonConvert.DeserializeObject<List<AlbumsViewModel>>(responseBodyAlbum.ToString());

            ViewBag.Albums = new SelectList(Albums, "Id", "AlbumName");
            return View(Song);
        }

        // POST: SongsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SongsViewModel Song)
        {
            Song.AlbumId = Song.SelectedAlbum;
            try
            {
                _res.UpdateSong(Song);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SongsController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _res.GetSong(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Song = JsonConvert.DeserializeObject<SongsViewModel>(responseBody);
            return View(Song);
        }

        // POST: SongsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _res.DeleteSong(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}