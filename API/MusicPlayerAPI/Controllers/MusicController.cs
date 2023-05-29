using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Data;

namespace MusicPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly MusicPlayerContext _context;

        public MusicController(MusicPlayerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetMusic()
        {
            return await _context.Music.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Music>> GetMusic(int id)
        {
            var Music = await _context.Music.FindAsync(id);

            if (Music == null)
            {
                return NotFound();
            }

            return Music;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusic(int id, Music Music)
        {
            Music.UpdatedDate = DateTime.Now.ToString();
            if (id != Music.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Music).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MusicExists(id))
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
        public async Task<ActionResult<Music>> PostMusic(Music Music)
        {
            //Music.Id = 3;
            Music.UpdatedDate = DateTime.Now.ToString();
            _context.Music.Add(Music);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MusicExists(Music.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMusic", new { id = Music.Id }, Music);
        }

        [Route("PostMultipleMusic")]
        [HttpPost]
        public async Task<ActionResult<Music>> PostMultipleMusic(Music[] Music)
        {
            //Music.Id = 3;
            foreach (var item in Music)
            {
                item.UpdatedDate = DateTime.Now.ToString();
                _context.Music.Add(item);
            }


            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    foreach (var item in Music)
            //    {
            //        if (MusicExists(item.Id))
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
        public async Task<ActionResult<Music>> DeleteMusic(int id)
        {
            var Music = await _context.Music.FindAsync(id);
            if (Music == null)
            {
                return NotFound();
            }

            _context.Music.Remove(Music);
            await _context.SaveChangesAsync();

            return Music;
        }

        private bool MusicExists(int id)
        {
            return _context.Music.Any(e => e.Id == id);
        }
    }
}
