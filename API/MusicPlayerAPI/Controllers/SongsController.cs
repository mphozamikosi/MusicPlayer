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

namespace SongsPlayerAPI.Controllers
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

        [Route("GetSong")]
        [HttpGet]
        public async Task<ActionResult<Songs>> GetSongs(int id)
        {
            var Songs = await _Songs.GetSong(id);

            if (Songs == null)
            {
                return NotFound();
            }

            return Songs;
        }
        [Route("EditSong")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongs(Songs Songs)
        {
            Songs.UpdatedDate = DateTime.Now;

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

        [Route("CreateSong")]
        [HttpPost]
        public async Task<ActionResult<Songs>> PostSongs(object Song)
        {
            //Songs.Id = 3;
            var SongObj = JsonConvert.DeserializeObject<Songs>(Song.ToString());
            try
            {
                _Songs.AddSong(SongObj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongsExists(SongObj.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSongs", new { id = SongObj.Id }, SongObj);
        }

        [Route("DeleteSong")]
        [HttpPut("{id}")]
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

        [Route("SearchSongs")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> SearchSongs(string SongName)
        {
            var Songs = await _Songs.SearchSongs(SongName);

            if (Songs == null)
            {
                return NotFound();
            }

            return Songs;
        }
        private bool SongsExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
