using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Data;
using MusicPlayerAPI.Interfaces;
using MusicPlayerAPI.Models;

namespace MusicPlayerAPI.BusinessLogic
{
    public class GenreLogic : IGenres
    {
        private readonly MusicPlayerContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public GenreLogic(MusicPlayerContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<List<Genres>> GetGenres()
        {
            try
            {
                var Genres = _context.Genres.ToListAsync();
                return Genres;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Task<Genres> GetGenre(int id)
        {
            try
            {
                var Genre = _context.Genres.Where(x => x.Id == id).Select(x => x).FirstAsync();
                return Genre;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Genres GetGenre(string songName)
        {
            try
            {
                var Genre = _context.Genres.Where(x => x.GenreName.Contains(songName)).Select(x => x).First();
                return Genre;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public bool AddGenre(Genres Genre)
        //{
        //    try
        //    {
        //        Genre.CreatedDate = DateTime.Now;
        //        _context.Genres.Add(Genre);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        public bool AddGenre(Genres Genre)
        {
            Genre.CreatedDate = _dateTimeProvider.Now;
            try
            {
                _context.Genres.Add(Genre);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool UpdateGenre(Genres Genre)
        {
            try
            {
                _context.Genres.Update(Genre);
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
