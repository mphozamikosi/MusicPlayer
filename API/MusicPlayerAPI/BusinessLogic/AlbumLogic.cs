using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Common;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class AlbumLogic : IAlbums
    {
        private readonly MusicPlayerContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AlbumLogic(MusicPlayerContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
            //_fileUploadHelper = fileUploadHelper;
        }

        public Task<List<Albums>> GetAlbums()
        {
            try
            {
                var Albums = _context.Albums.Include(a => a.Artist).Include(s => s.Genre).Include(s => s.Songs).ToListAsync();
                return Albums;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Albums GetAlbum(int id)
        {
            try
            {
                var Album = _context.Albums.Where(x => x.Id == id).Include(a => a.Artist).Include(s => s.Genre).Include(s => s.Songs).Select(x => x).FirstOrDefault();
                //Album.Photo = _fileUploadHelper.GetSavedImage(Album.Photo);
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
            Album.CreatedDate = _dateTimeProvider.Now;
            Album.UpdatedDate = _dateTimeProvider.Now;
            //Album.Photo = _fileUploadHelper.SaveAlbumFile(file);
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

        public Task<List<Albums>> SearchAlbums(string albumName)
        {
            try
            {
                var artists = _context.Albums.Where(n => n.AlbumName.ToLower().Contains(albumName.ToLower())).Select(x => x).ToListAsync();
                return artists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
