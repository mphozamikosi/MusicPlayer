using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class AlbumLogic
    {
        private readonly MusicPlayerContext _context;
        private readonly IMusic _music;

        public AlbumLogic(MusicPlayerContext context, IMusic music)
        {
            _context = context;
            _music = music;
        }

        public List<Albums> GetAlbums()
        {
            try
            {
                var Albums = _context.Albums.Select(x => x).ToList();
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
                var Album = _context.Albums.Where(x => x.Id == id).Select(x => x).First();
                return Album;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public Albums AddAlbum(int id)
        //{
        //    try
        //    {
        //        var Album = _context.Albums.Where(x => x.Id == id).Select(x => x).First();
        //        return Album;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //}
        public bool AddAlbum(Albums Album)
        {

            try
            {
                _context.Albums.Add(Album);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateAlbum(Albums Album)
        {
            try
            {
                _context.Albums.Update(Album);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool RemoveAlbum(Albums Album)
        {
            try
            {
                _context.Albums.Remove(Album);
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
