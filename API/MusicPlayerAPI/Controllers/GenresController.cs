using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Data;

namespace SongsPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicPlayerContext _context;

        public SongsController(MusicPlayerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Songs>> GetSongs(int id)
        {
            var Songs = await _context.Songs.FindAsync(id);

            if (Songs == null)
            {
                return NotFound();
            }

            return Songs;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongs(int id, Songs Songs)
        {
            Songs.UpdatedDate = DateTime.Now;
            if (id != Songs.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Songs).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!SongsExists(id))
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
        public async Task<ActionResult<Songs>> PostSongs(Songs Songs)
        {
            //Songs.Id = 3;
            Songs.UpdatedDate = DateTime.Now;
            _context.Songs.Add(Songs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongsExists(Songs.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSongs", new { id = Songs.Id }, Songs);
        }

        [Route("PostMultipleSongs")]
        [HttpPost]
        public async Task<ActionResult<Songs>> PostMultipleSongs(Songs[] Songs)
        {
            //Songs.Id = 3;
            foreach (var item in Songs)
            {
                item.UpdatedDate = DateTime.Now;
                _context.Songs.Add(item);
            }


            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    foreach (var item in Songs)
            //    {
            //        if (SongsExists(item.Id))
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
        public async Task<ActionResult<Songs>> DeleteSongs(int id)
        {
            var Songs = await _context.Songs.FindAsync(id);
            if (Songs == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(Songs);
            await _context.SaveChangesAsync();

            return Songs;
        }

        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
