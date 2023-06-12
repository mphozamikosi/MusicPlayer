using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.RestCalls;
using MusicApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace TestFrontEndApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly RestCalls _res = new RestCalls();
        // GET: GenresController
        public ActionResult Index()
        {
            var response = _res.GetAllGenres();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Genres = JsonConvert.DeserializeObject<List<GenresViewModel>>(responseBody.ToString());
            return View(Genres);
        }
        public ActionResult Search(IFormCollection form)
        {
            var response = _res.GetAllGenres(form["SearchText"]);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var artists = JsonConvert.DeserializeObject<List<GenresViewModel>>(responseBody.ToString());
            return View(artists);
        }

        // GET: GenresController/Details/5
        public ActionResult Details(int id)
        {
            var response = _res.GetGenre(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Genre = JsonConvert.DeserializeObject<GenresViewModel>(responseBody);

            return View(Genre);
        }

        // GET: GenresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenresViewModel Genre)
        {
            try
            {
                _res.CreateGenre(Genre);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenresController/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _res.GetGenre(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Genre = JsonConvert.DeserializeObject<GenresViewModel>(responseBody);
            return View(Genre);
        }

        // POST: GenresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GenresViewModel Genre)
        {
            try
            {
                _res.UpdateGenre(Genre);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenresController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _res.GetGenre(id);
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            var Genre = JsonConvert.DeserializeObject<GenresViewModel>(responseBody);
            return View(Genre);
        }

        // POST: GenresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _res.DeleteGenre(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}