using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Data;

namespace ArtistsPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicPlayerContext _context;

        public ArtistsController(MusicPlayerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artists>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artists>> GetArtists(int id)
        {
            var Artists = await _context.Artists.FindAsync(id);

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
            if (id != Artists.Id)
            {
                return BadRequest();
            }

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
            Artists.UpdatedDate = DateTime.Now;
            _context.Artists.Add(Artists);
            try
            {
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

        [Route("PostMultipleArtists")]
        [HttpPost]
        public async Task<ActionResult<Artists>> PostMultipleArtists(Artists[] Artists)
        {
            //Artists.Id = 3;
            foreach (var item in Artists)
            {
                item.UpdatedDate = DateTime.Now;
                _context.Artists.Add(item);
            }


            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    foreach (var item in Artists)
            //    {
            //        if (ArtistsExists(item.Id))
            //        {
            //            return Conflict();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //}
            return NoContent();
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
