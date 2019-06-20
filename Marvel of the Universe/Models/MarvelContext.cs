using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Marvel_of_the_Universe.Models
{
    public static class Data
    {
        public static IEnumerable<Movie> GetMovies()
        {
            MarvelContext context = new MarvelContext();
            return context.Movies;
        }
    }
    public class User : IdentityUser
    {
        public User()
        {
        }
    }
    public class MarvelContext : IdentityDbContext<User>
    {
        public MarvelContext() : base("MarvelContext") { }
        public static MarvelContext Create()
        {
            return new MarvelContext();
        }
        public DbSet<Heroe> Heroes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Heroe>().HasMany(m => m.Movies)
        //        .WithMany(h => h.Heroes)
        //        .Map(t => t.MapLeftKey("HeroeId")
        //        .MapRightKey("MovieId")
        //        .ToTable("MovieHeroe"));
        //}
    }
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
                : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                                IOwinContext context)
        {
            MarvelContext db = context.Get<MarvelContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));
            return manager;
        }
    }
}