using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class AlbumLogic : IAlbums
    {
        private readonly MusicPlayerContext _context;

        public AlbumLogic(MusicPlayerContext context)
        {
            _context = context;
        }

        public Task<List<Albums>> GetAlbums()
        {
            try
            {
                var Albums = _context.Albums.ToListAsync();
                return Albums;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Task<Albums> GetAlbum(int id)
        {
            try
            {
                var Album = _context.Albums.Where(x => x.Id == id).Select(x => x).FirstAsync();
                return Album;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Albums GetAlbum(string songName)
        {
            try
            {
                var Album = _context.Albums.Where(x => x.AlbumName.Contains(songName)).Select(x => x).First();
                return Album;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public bool AddAlbum(Albums Album)
        //{
        //    try
        //    {
        //        Album.CreatedDate = DateTime.Now;
        //        _context.Albums.Add(Album);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        public bool AddAlbum(Albums Album)
        {
            Album.CreatedDate = DateTime.Now;
            try
            {
                _context.Albums.Add(Album);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool UpdateAlbum(Albums Album)
        {
            try
            {
                _context.Albums.Update(Album);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
