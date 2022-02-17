﻿using MusicHub.Data.Models;

namespace MusicHub.Data
{
    using Microsoft.EntityFrameworkCore;

    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongPerformer> SongPerformers { get; set; }
        public DbSet<Writer> Writers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SongPerformer>(x =>
            {
                x.HasKey(x => new { x.PerformerId, x.SongId });
            });
            builder.Entity<Song>(x =>
            {
                x.HasOne(x => x.Writer)
                    .WithMany(x => x.Songs)
                    .HasForeignKey(x => x.WriterId)
                    .OnDelete(DeleteBehavior.Restrict);
                x.HasOne(x => x.Album)
                    .WithMany(x => x.Songs)
                    .HasForeignKey(x => x.AlbumId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}