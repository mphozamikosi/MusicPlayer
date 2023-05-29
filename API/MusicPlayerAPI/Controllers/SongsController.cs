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

namespace GenresPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongs _Songs;
        private readonly MusicPlayerContext _context;

        public SongsController(MusicPlayerContext context, ISongs Songs)
        {
            _context = context;
            _Songs = Songs;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {
            return await _Songs.GetSongs();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Songs>> GetSongs(int id)
        {
            var Songs = await _Songs.GetSong(id);

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
            Songs.Id = id;
            if (id != Songs.Id)
            {
                return BadRequest();
            }

            _Songs.UpdateSong(Songs);
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
            try
            {
                _Songs.AddSong(Songs);
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
