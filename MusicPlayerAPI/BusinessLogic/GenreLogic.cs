using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class GenreLogic
    {
        private readonly MusicPlayerContext _context;
        private readonly IMusic _music;

        public GenreLogic(MusicPlayerContext context, IMusic music)
        {
            _context = context;
            _music = music;
        }
        public List<Genres> GetGenres()
        {
            try
            {
                var Genres = _context.Genres.Select(x => x).ToList();
                return Genres;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Genres GetGenre(int id)
        {
            try
            {
                var Genre = _context.Genres.Where(x => x.Id == id).Select(x => x).First();
                return Genre;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public Genres AddGenre(int id)
        //{
        //    try
        //    {
        //        var Genre = _context.Genres.Where(x => x.Id == id).Select(x => x).First();
        //        return Genre;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //}
        public bool AddGenre(Genres Genre)
        {

            try
            {
                _context.Genres.Add(Genre);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateGenre(Genres Genre)
        {
            try
            {
                _context.Genres.Update(Genre);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool RemoveGenre(Genres Genre)
        {
            try
            {
                _context.Genres.Remove(Genre);
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
