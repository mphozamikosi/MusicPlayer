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

        [Route("GetArtist")]
        [HttpGet]
        public async Task<ActionResult<Artists>> GetArtists(int id)
        {
            var Artists = await _Artists.GetArtist(id);

            if (Artists == null)
            {
                return NotFound();
            }

            return Artists;
        }
        [Route("EditArtist")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtists(Artists Artists)
        {
            Artists.UpdatedDate = DateTime.Now;

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

        [Route("CreateArtist")]
        [HttpPost]
        public async Task<ActionResult<Artists>> PostArtists(object artist)
        {
            //Artists.Id = 3;
            var artistObj = JsonConvert.DeserializeObject<Artists>(artist.ToString());
            try
            {
                _Artists.AddArtist(artistObj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistsExists(artistObj.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArtists", new { id = artistObj.Id }, artistObj);
        }

        [Route("DeleteArtist")]
        [HttpPut("{id}")]
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

        [Route("SearchArtists")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artists>>> SearchArtists(string artistName)
        {
            var Artists = await _Artists.SearchArtists(artistName);

            if (Artists == null)
            {
                return NotFound();
            }

            return Artists;
        }
        private bool ArtistsExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
