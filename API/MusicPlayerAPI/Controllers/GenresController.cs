using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;

namespace SongsPlayerAPI.Controllers
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Genres>> GetGenres(int id)
        {
            var Genres = await _Genres.GetGenre(id);

            if (Genres == null)
            {
                return NotFound();
            }

            return Genres;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenres(int id, Genres Genres)
        {
            Genres.UpdatedDate = DateTime.Now;
            Genres.Id = id;
            if (id != Genres.Id)
            {
                return BadRequest();
            }

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

        [HttpPost]
        public async Task<ActionResult<Genres>> PostGenres(Genres Genres)
        {
            //Genres.Id = 3;
            try
            {
                _Genres.AddGenre(Genres);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenresExists(Genres.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGenres", new { id = Genres.Id }, Genres);
        }


        [HttpDelete("{id}")]
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

        private bool GenresExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
