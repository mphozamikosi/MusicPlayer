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

namespace AlbumsPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbums _Albums;
        private readonly MusicPlayerContext _context;

        public AlbumsController(MusicPlayerContext context, IAlbums Albums)
        {
            _context = context;
            _Albums = Albums;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Albums>>> GetAlbums()
        {
            return await _Albums.GetAlbums();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Albums>> GetAlbums(int id)
        {
            var Albums = await _Albums.GetAlbum(id);

            if (Albums == null)
            {
                return NotFound();
            }

            return Albums;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbums(int id, Albums Albums)
        {
            Albums.UpdatedDate = DateTime.Now;
            Albums.Id = id;
            if (id != Albums.Id)
            {
                return BadRequest();
            }

            _Albums.UpdateAlbum(Albums);
            //_context.Entry(Albums).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AlbumsExists(id))
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
        public async Task<ActionResult<Albums>> PostAlbums(Albums Albums)
        {
            //Albums.Id = 3;
            try
            {
                _Albums.AddAlbum(Albums);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlbumsExists(Albums.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlbums", new { id = Albums.Id }, Albums);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Albums>> DeleteAlbums(int id)
        {
            var Albums = await _context.Albums.FindAsync(id);
            if (Albums == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(Albums);
            await _context.SaveChangesAsync();

            return Albums;
        }

        private bool AlbumsExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}
