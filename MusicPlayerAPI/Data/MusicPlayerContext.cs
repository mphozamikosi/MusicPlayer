using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data
{
    public class MusicPlayerContext : DbContext
    {
        public MusicPlayerContext(DbContextOptions<MusicPlayerContext> options) : base(options)
        {
        }

        public DbSet<Music> Music { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Music>().ToTable("Music");
        }


    }
}
