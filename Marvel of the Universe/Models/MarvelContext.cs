using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public class MarvelContext : DbContext
    {
        public DbSet<Heroe> Heroes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Heroe>().HasMany(m => m.Movies)
                .WithMany(h => h.Heroes)
                .Map(t => t.MapLeftKey("HeroeId")
                .MapRightKey("MovieId")
                .ToTable("MovieHeroe"));
        }
    }
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return Convert.ToInt32(Math.Ceiling((decimal)TotalItems / PageSize)); }
        }
    }
    public class IndexViewHeroe
    {
        public IEnumerable<Heroe> Heroes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    public class IndexViewMovie
    {
        public IEnumerable<Movie> Movies { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}