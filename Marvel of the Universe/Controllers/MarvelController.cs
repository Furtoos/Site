using Marvel_of_the_Universe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Marvel_of_the_Universe.Controllers
{
    public class MarvelController : Controller
    {
        private MarvelContext db = new MarvelContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Heroes(int page = 1)
        {
            int pageSize = 6;
            IEnumerable<Heroe> heroesOnPage = db.Heroes
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Heroes.Count() };
            IndexViewHeroes ivh = new IndexViewHeroes { PageInfo = pageInfo, Heroes = heroesOnPage };
            return View(ivh);
        }
        public ActionResult Movies(int page = 1)
        {
            int pageSize = 6;
            IEnumerable<Movie> moviesOnPage = db.Movies
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Movies.Count() };
            IndexViewMovies ivh = new IndexViewMovies { PageInfo = pageInfo, Movies = moviesOnPage };
            return View(ivh);
        }
        public FileResult GetImage()
        {
            return File(Server.MapPath("~/Images/Marvel.png"), "image/png");
        }
        public ActionResult Movie(long id, int list = 1)
        {
            Movie movie = db.Movies.Find(id);
            int listSize = 3;
            IEnumerable<Heroe> heroesOnList = movie.Heroes
                .OrderBy(x => x.Id)
                .Skip((list - 1) * listSize)
                .Take(listSize).ToList();
            ListInfo listInfo = new ListInfo { ListNumber = list, ListSize = listSize, TotalItems = movie.Heroes.Count() };
            ViewMovie vm = new ViewMovie { ListInfo = listInfo, Movie = movie, Heroes = heroesOnList };
            return View(vm);
        }
        public ActionResult Heroe(long id,int list = 1)
        {
            Heroe heroe = db.Heroes.Find(id);
            int listSize = 3;
            IEnumerable<Movie> moviesOnList = heroe.Movies
                .OrderBy(x => x.Id)
                .Skip((list - 1) * listSize)
                .Take(listSize).ToList();
            ListInfo listInfo = new ListInfo { ListNumber = list, ListSize = listSize, TotalItems = heroe.Movies.Count() };
            ViewHeroe vh = new ViewHeroe { ListInfo = listInfo, Heroe = heroe, Movies = moviesOnList };
            return View(vh);
        }
        [Authorize]
        [HttpPost]
        public ActionResult NewComment(Comment comment)
        {
            if (comment.HeroeId != null)
            {
                Heroe heroe = db.Heroes.Find(comment.HeroeId);
                if (heroe == null)
                    return HttpNotFound();
                heroe.Comments.Add(comment);
                db.SaveChanges();
                return PartialView("Comment",comment);
            }
            else
            {
                Movie movie = db.Movies.Find(comment.MovieId);
                if (movie == null)
                    return HttpNotFound();
                movie.Comments.Add(comment);
                db.SaveChanges();
                return PartialView("Comment", comment);
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult LikeHeroe(int? heroeId)
        {
            Like like = db.Likes.Where(l => l.UserName == User.Identity.Name).FirstOrDefault();
            Heroe heroe = db.Heroes.Find(heroeId);
            if (heroe == null)
                return HttpNotFound();
            if (like == null)
            {
                like = new Like()
                {
                    Id = db.Likes.Count() + 1,
                    like = true,
                    UserName = User.Identity.Name,
                    HeroeId = heroeId,
                    MovieId = null
                };
                heroe.Likes.Add(like);
                db.SaveChanges();
            }
            else
            {
                if (like.like)
                {
                    db.Likes.Remove(like);
                    db.SaveChanges();
                }
                else
                {
                    db.Likes.Remove(like);
                    like = new Like()
                    {
                        Id = db.Likes.Count() + 1,
                        like = true,
                        UserName = User.Identity.Name,
                        HeroeId = heroeId,
                        MovieId = null
                    };
                    heroe.Likes.Add(like);
                    db.SaveChanges();
                }
            }
            return PartialView("LikeHeroe", heroe);
        }
        [Authorize]
        [HttpPost]
        public ActionResult DislikeHeroe(int? heroeId)
        {
            Like like = db.Likes.Where(l => l.UserName == User.Identity.Name).FirstOrDefault();
            Heroe heroe = db.Heroes.Find(heroeId);
            if (heroe == null)
                return HttpNotFound();
            if (like == null)
            {
                like = new Like()
                {
                    Id = db.Likes.Count() + 1,
                    like = false,
                    UserName = User.Identity.Name,
                    HeroeId = heroeId,
                    MovieId = null
                };
                heroe.Likes.Add(like);
                db.SaveChanges();
            }
            else
            {
                if (like.like)
                {
                    db.Likes.Remove(like);
                    like = new Like()
                    {
                        Id = db.Likes.Count() + 1,
                        like = false,
                        UserName = User.Identity.Name,
                        HeroeId = heroeId,
                        MovieId = null
                    };
                    heroe.Likes.Add(like);
                    db.SaveChanges();
                }
                else
                {
                    db.Likes.Remove(like);
                    db.SaveChanges();
                }
            }
            return PartialView("LikeHeroe", heroe);
        }
        [Authorize]
        [HttpPost]
        public ActionResult LikeMovie(int? movieId)
        {
            Like like = db.Likes.Where(l => l.UserName == User.Identity.Name).FirstOrDefault();
            Movie movie = db.Movies.Find(movieId);
            if (movie == null)
                return HttpNotFound();
            if (like == null)
            {
                like = new Like()
                {
                    Id = db.Likes.Count() + 1,
                    like = true,
                    UserName = User.Identity.Name,
                    HeroeId = null,
                    MovieId = movieId
                };
                movie.Likes.Add(like);
                db.SaveChanges();
            }
            else
            {
                if (like.like)
                {
                    db.Likes.Remove(like);
                    db.SaveChanges();
                }
                else
                {
                    db.Likes.Remove(like);
                    like = new Like()
                    {
                        Id = db.Likes.Count() + 1,
                        like = true,
                        UserName = User.Identity.Name,
                        HeroeId = null,
                        MovieId = movieId
                    };
                    movie.Likes.Add(like);
                    db.SaveChanges();
                }
            }
            return PartialView("LikeMovie", movie);
        }
        [Authorize]
        [HttpPost]
        public ActionResult DislikeMovie(int? movieId)
        {
            Like like = db.Likes.Where(l => l.UserName == User.Identity.Name).FirstOrDefault();
            Movie movie = db.Movies.Find(movieId);
            if (movie == null)
                return HttpNotFound();
            if (like == null)
            {
                like = new Like()
                {
                    Id = db.Likes.Count() + 1,
                    like = false,
                    UserName = User.Identity.Name,
                    HeroeId = null,
                    MovieId = movieId
                };
                movie.Likes.Add(like);
                db.SaveChanges();
            }
            else
            {
                if (like.like)
                {
                    db.Likes.Remove(like);
                    like = new Like()
                    {
                        Id = db.Likes.Count() + 1,
                        like = false,
                        UserName = User.Identity.Name,
                        HeroeId = null,
                        MovieId = movieId
                    };
                    movie.Likes.Add(like);
                    db.SaveChanges();
                }
                else
                {
                    db.Likes.Remove(like);
                    db.SaveChanges();
                }
            }
            return PartialView("LikeMovie", movie);
        }
        public ActionResult ListMovies(int heroeId, int list = 1)
        {
            Heroe heroe = db.Heroes.Find(heroeId);
            int listSize = 3;
            IEnumerable<Movie> moviesOnList = heroe.Movies
                .OrderBy(x => x.Id)
                .Skip((list - 1) * listSize)
                .Take(listSize).ToList();
            return PartialView(moviesOnList);
        }
        public ActionResult ListHeroes(int movieId, int list = 1)
        {
            Movie movie = db.Movies.Find(movieId);
            int listSize = 3;
            IEnumerable<Heroe> heroeOnList = movie.Heroes
                .OrderBy(x => x.Id)
                .Skip((list - 1) * listSize)
                .Take(listSize).ToList();
            return PartialView(heroeOnList);
        }
    }
}