using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class SongLogic
    {
        private readonly MusicPlayerContext _context;
        private readonly IMusic _music;

        public SongLogic(MusicPlayerContext context, IMusic music)
        {
            _context = context;
            _music = music;
        }

        public List<Songs> GetSongs()
        {
            try
            {
                var Songs = _context.Songs.Select(x => x).ToList();
                return Songs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Songs GetSong(int id)
        {
            try
            {
                var Song = _context.Songs.Where(x => x.Id == id).Select(x => x).First();
                return Song;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public Songs AddSong(int id)
        //{
        //    try
        //    {
        //        var Song = _context.Songs.Where(x => x.Id == id).Select(x => x).First();
        //        return Song;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //}
        public bool AddSong(Songs Song)
        {
            try
            {
                _context.Songs.Add(Song);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateSong(Songs Song)
        {
            try
            {
                _context.Songs.Update(Song);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool RemoveSong(Songs Song)
        {
            try
            {
                _context.Songs.Remove(Song);
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
