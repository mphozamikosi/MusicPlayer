using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.RestCalls;
using MusicApp.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace TestFrontEndApp.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly RestCalls _res = new RestCalls();

        private string _searchString = "";
        // GET: ArtistsController
        public ActionResult Index()
        {
            var response = _res.GetAllArtists();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<ArtistsViewModel>>(responseBody.ToString());
            return View(artists);
        }

        public ActionResult Search(IFormCollection form)
        {
            _searchString = form["SearchText"];

            var response = _res.GetAllArtists(_searchString);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<ArtistsViewModel>>(responseBody.ToString());
            return View(artists);
        }

        // GET: ArtistsController/Details/5
        public ActionResult Details(int id)
        {
            var response = _res.GetArtist(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artist = JsonConvert.DeserializeObject<ArtistsViewModel>(responseBody);
            return View(artist);
        }

        // GET: ArtistsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistsViewModel artist)
        {
            try
            {
                _res.CreateArtist(artist);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistsController/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _res.GetArtist(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artist = JsonConvert.DeserializeObject<ArtistsViewModel>(responseBody);
            return View(artist);
        }

        // POST: ArtistsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArtistsViewModel artist)
        {
            try
            {
                _res.UpdateArtist(artist);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistsController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _res.GetArtist(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artist = JsonConvert.DeserializeObject<ArtistsViewModel>(responseBody);
            return View(artist);
        }

        // POST: ArtistsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _res.DeleteArtist(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}