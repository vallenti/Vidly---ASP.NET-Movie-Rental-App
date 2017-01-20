using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext context;
        public MoviesController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };
            var customers = new List<Customer>()
            {
                new Customer() { Name = "Customer 1" },
                new Customer() { Name = "Customer 2" }
            };
            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = context.Genres
            };
            return View("MoviesForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = context.Genres
                };
                return View("MoviesForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                context.Movies.Add(movie);
            }
            else
            {
                var movieFromDb = context.Movies.Find(movie.Id);
                movieFromDb.Name = movie.Name;
                movieFromDb.ReleaseDate = movie.ReleaseDate;
                movieFromDb.GenreId = movie.GenreId;
                movieFromDb.NumbersInStock = movie.NumbersInStock;
                movieFromDb.DateAdded = DateTime.Now;
            }

            context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = context.Genres
            };
            return View("MoviesForm", viewModel);
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = context.Movies
                .Include(m => m.Genre)
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new DetailsMovieViewModel()
            {
                Name = movie.Name,
                Genre = movie.Genre.Name,
                DateAdded = $"{movie.DateAdded}",
                ReleaseDate = $"{movie.ReleaseDate}",
                NumbersInStock = movie.NumbersInStock
            };

            return View(viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}