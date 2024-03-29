﻿using MusicPlayerAPI.Data;
using MusicPlayerAPI.Models;
using MusicPlayerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicPlayerAPI.BusinessLogic
{
    public class ArtistLogic : IArtists
    {
        private readonly MusicPlayerContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ArtistLogic(MusicPlayerContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<List<Artists>> GetArtists()
        {
            try
            {
                var artists = _context.Artists.Select(x => x).ToListAsync();
                return artists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Task<Artists> GetArtist(int id)
        {
            try
            {
                var artist = _context.Artists.Include(a => a.Albums).Where(x => x.Id == id).Select(x => x).FirstAsync();
                return artist;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return null;
            }
        }

        public Artists GetArtist(string songName)
        {
            try
            {
                var artist = _context.Artists.Where(x => x.ArtistName.Contains(songName)).Select(x => x).First();
                return artist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //public bool AddArtist(Artists artist)
        //{
        //    try
        //    {
        //        artist.CreatedDate = DateTime.Now;
        //        _context.Artists.Add(artist);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        public bool AddArtist(Artists artist)
        {
            artist.CreatedDate = _dateTimeProvider.Now;
            artist.UpdatedDate = _dateTimeProvider.Now;
            try
            {
                _context.Artists.Add(artist);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool UpdateArtist(Artists artist)
        {
            artist.UpdatedDate = _dateTimeProvider.Now;
            try
            {
                _context.Artists.Update(artist);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public Task<List<Artists>> SearchArtists(string artistName)
        {
            try
            {
                var artists = _context.Artists.Where(n => n.ArtistName.ToLower().Contains(artistName.ToLower())).Select(x => x).ToListAsync();
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
