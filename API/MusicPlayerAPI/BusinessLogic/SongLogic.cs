using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class SongLogic : ISongs
    {
        private readonly MusicPlayerContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public SongLogic(MusicPlayerContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<List<Songs>> GetSongs()
        {
            try
            {
                var Songs = _context.Songs.Include(a => a.Album).ThenInclude(a => a.Artist).ToListAsync();
                return Songs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Task<Songs> GetSong(int id)
        {
            try
            {
                var Song = _context.Songs.Include(a => a.Album).ThenInclude(a => a.Artist).Where(x => x.Id == id).Select(x => x).FirstAsync();
                return Song;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Songs GetSong(string songName)
        {
            try
            {
                var Song = _context.Songs.Where(x => x.SongName.Contains(songName)).Select(x => x).First();
                return Song;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public bool AddSong(Songs Song)
        //{
        //    try
        //    {
        //        Song.CreatedDate = DateTime.Now;
        //        _context.Songs.Add(Song);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        public bool AddSong(Songs Song)
        {
            Song.CreatedDate = _dateTimeProvider.Now;
            Song.UpdatedDate = _dateTimeProvider.Now;
            try
            {
                _context.Songs.Add(Song);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool UpdateSong(Songs Song)
        {
            try
            {
                _context.Songs.Update(Song);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public Task<List<Songs>> SearchSongs(string songName)
        {
            try
            {
                var songs = _context.Songs.Where(n => n.SongName.ToLower().Contains(songName.ToLower())).Select(x => x).ToListAsync();
                return songs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
