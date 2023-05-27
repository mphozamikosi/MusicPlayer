using MusicPlayerAPI.Models;
using Microsoft.EntityFrameworkCore;
using MusicPlayerAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayerAPI.Data
{
    public class MusicPlayerContext : DbContext
    {
        public MusicPlayerContext(DbContextOptions<MusicPlayerContext> options) : base(options)
        {
        }

        public DbSet<Music> Music { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Albums> Albums { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Songs> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Music>().ToTable("Music");
            modelBuilder.Entity<Artists>().ToTable("Artists");
            modelBuilder.Entity<Albums>().ToTable("Albums");
            modelBuilder.Entity<Genres>().ToTable("Genres");
            modelBuilder.Entity<Songs>().ToTable("Songs");
        }


    }
}
