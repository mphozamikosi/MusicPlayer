using API.Models;
using System;
using System.Linq;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MusicPlayerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Music.Any())
            {
                return;   
            }

            var contact = new Music[]
            {
                new Music{FirstName="Mpho",Surname="Mikosi",Cel="0671764259", Tel="0671764259", UpdatedDate=DateTime.Now.ToString()},
            };

            foreach (Music s in contact)
            {
                context.Music.Add(s);
            }
            context.SaveChanges();
        }
    }
}