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

namespace ArtistsPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtists _Artists;
        private readonly MusicPlayerContext _context;

        public ArtistsController(MusicPlayerContext context, IArtists artists)
        {
            _context = context;
            _Artists = artists;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artists>>> GetArtists()
        {
            return await _Artists.GetArtists();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artists>> GetArtists(int id)
        {
            var Artists = await _Artists.GetArtist(id);

            if (Artists == null)
            {
                return NotFound();
            }

            return Artists;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtists(int id, Artists Artists)
        {
            Artists.UpdatedDate = DateTime.Now;
            Artists.Id = id;
            if (id != Artists.Id)
            {
                return BadRequest();
            }

            _Artists.UpdateArtist(Artists);
            //_context.Entry(Artists).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ArtistsExists(id))
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
        public async Task<ActionResult<Artists>> PostArtists(Artists Artists)
        {
            //Artists.Id = 3;
            try
            {
                _Artists.AddArtist(Artists);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistsExists(Artists.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtists", new { id = Artists.Id }, Artists);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Artists>> DeleteArtists(int id)
        {
            var Artists = await _context.Artists.FindAsync(id);
            if (Artists == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(Artists);
            await _context.SaveChangesAsync();

            return Artists;
        }

        private bool ArtistsExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
