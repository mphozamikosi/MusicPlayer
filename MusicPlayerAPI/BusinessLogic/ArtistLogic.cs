using MusicPlayerAPI.Data;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Interfaces;

namespace MusicPlayerAPI.BusinessLogic
{
    public class ArtistLogic
    {
        private readonly MusicPlayerContext _context;
        private readonly IMusic _music;

        public ArtistLogic(MusicPlayerContext context, IMusic music)
        {
            _context = context;
            _music = music;
        }

        public List<Artists> GetArtists()
        {
            try
            {
                var artists = _context.Artists.Select(x => x).ToList();
                return artists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Artists GetArtist(int id)
        {
            try
            {
                var artist = _context.Artists.Where(x => x.Id == id).Select(x => x).First();
                return artist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public Artists AddArtist(int id)
        //{
        //    try
        //    {
        //        var artist = _context.Artists.Where(x => x.Id == id).Select(x => x).First();
        //        return artist;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //}
        public bool AddArtist(Artists artist)
        {

            try
            {
                _context.Artists.Add(artist);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateArtist(Artists artist)
        {
            try
            {
                _context.Artists.Update(artist);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool RemoveArtist(Artists artist)
        {
            try
            {
                _context.Artists.Remove(artist);
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
