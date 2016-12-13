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
            var viewModel = new IndexMovieViewModel()
            {
                Movies = new List<Movie>(context.Movies.Include(m => m.Genre))

            };
            return View(viewModel);
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