using MusicPlayerAPI.Data;
using MusicPlayerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerAPI.BusinessLogic
{
    public class MusicLogic
    {
        private readonly MusicPlayerContext _context;

        public MusicLogic(MusicPlayerContext context)
        {
            _context = context;
        }

        public List<Music> GetMusic()
        {
            try
            {
                var music = _context.Music.Select(x => x).ToList();
                return music;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public Music GetContact(int id)
        {
            try
            {
                var music = _context.Music.Find(id);
                return music;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public bool AddContact(Music contact)
        {
            
            try
            {
                _context.Music.Add(contact);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool UpdateContact(Music contact)
        {
            try
            {
                _context.Music.Update(contact);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool RemoveContact(Music contact)
        {
            try
            {
                _context.Music.Remove(contact);
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
