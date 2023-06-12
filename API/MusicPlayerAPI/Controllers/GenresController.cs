using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GenresPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenres _Genres;
        private readonly MusicPlayerContext _context;

        public GenresController(MusicPlayerContext context, IGenres Genres)
        {
            _context = context;
            _Genres = Genres;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> GetGenres()
        {
            return await _Genres.GetGenres();
        }

        [Route("GetGenre")]
        [HttpGet]
        public async Task<ActionResult<Genres>> GetGenres(int id)
        {
            var Genres = await _Genres.GetGenre(id);

            if (Genres == null)
            {
                return NotFound();
            }

            return Genres;
        }
        [Route("EditGenre")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres(Genres Genres)
        {
            Genres.UpdatedDate = DateTime.Now;

            _Genres.UpdateGenre(Genres);
            //_context.Entry(Genres).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!GenresExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        [Route("AddGenre")]
        [HttpPost]
        public async Task<ActionResult<Genres>> PostGenres(object Genre)
        {
            //Genres.Id = 3;
            var GenreObj = JsonConvert.DeserializeObject<Genres>(Genre.ToString());
            try
            {
                _Genres.AddGenre(GenreObj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenresExists(GenreObj.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGenres", new { id = GenreObj.Id }, GenreObj);
        }

        [Route("DeleteGenre")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Genres>> DeleteGenres(int id)
        {
            var Genres = await _context.Genres.FindAsync(id);
            if (Genres == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(Genres);
            await _context.SaveChangesAsync();

            return Genres;
        }

        [Route("SearchGenres")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> SearchGenres(string GenreName)
        {
            var Genres = await _Genres.SearchGenres(GenreName);

            if (Genres == null)
            {
                return NotFound();
            }

            return Genres;
        }

        private bool GenresExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
