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
//using Newtonsoft.Json;
//using System.Net.Http.Json;

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

        [Route("GetAlbum")]
        [HttpGet]
        public async Task<ActionResult<Albums>> GetAlbums(int id)
        {
            var Albums = _Albums.GetAlbum(id);

            if (Albums == null)
            {
                return NotFound();
            }

            return Albums;
        }
        [Route("EditAlbum")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbums(object Album)
        {
            var Albums = JsonConvert.DeserializeObject<Albums>(Album.ToString());
            Albums.UpdatedDate = DateTime.Now;

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

        [Route("AddAlbum")]
        [HttpPost]
        public async Task<ActionResult<Albums>> PostAlbums(object Album)
        {
            //Albums.Id = 3;
            var AlbumObj = JsonConvert.DeserializeObject<Albums>(Album.ToString());
            try
            {
                _Albums.AddAlbum(AlbumObj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (AlbumsExists(AlbumObj.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlbums", new { id = AlbumObj.Id }, AlbumObj);
        }

        [Route("DeleteAlbum")]
        [HttpPut("{id}")]
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
        [Route("SearchAlbums")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Albums>>> SearchAlbums(string AlbumName)
        {
            var Albums = await _Albums.SearchAlbums(AlbumName);

            if (Albums == null)
            {
                return NotFound();
            }

            return Albums;
        }

        private bool AlbumsExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }


    }
}
