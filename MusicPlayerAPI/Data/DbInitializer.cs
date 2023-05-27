using MusicPlayerAPI.Models;
using MusicPlayerAPI.Models;
using System;
using System.Linq;

namespace MusicPlayerAPI.Data
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

            var music = new Music[]
            {
                new Music{FirstName="Mpho",Surname="Mikosi",Cel="0671764259", Tel="0671764259", UpdatedDate=DateTime.Now.ToString()},
            };

            foreach (var m in music)
            {
                context.Music.Add(m);
            }

            var artists = new Artists[]
            {
                new Artists{ArtistName="Default Artist", CreatedDate=DateTime.Now},
            };

            foreach (var artist in artists)
            {
                context.Artists.Add(artist);
            }

            var genres = new Genres[]
            {
                new Genres{GenreName="Default Genre", CreatedDate=DateTime.Now},
            };

            foreach (var genre in genres)
            {
                context.Genres.Add(genre);
            }

            var albums = new Albums[]
            {
                new Albums{AlbumName="Default Album", CreatedDate=DateTime.Now, 
                    ArtistId=context.Artists.FirstOrDefault().Id, GenreId=context.Genres.FirstOrDefault().Id},
            };

            foreach (var album in albums)
            {
                context.Albums.Add(album);
            }

            var songs = new Songs[]
            {
                new Songs{SongName="Default Song", CreatedDate=DateTime.Now, 
                    AlbumId=context.Albums.FirstOrDefault().Id, GenreId=context.Genres.FirstOrDefault().Id, 
                    ArtistId=context.Artists.FirstOrDefault().Id},
            };

            foreach (var song in songs)
            {
                context.Songs.Add(song);
            }

            context.SaveChanges();
        }
    }
}