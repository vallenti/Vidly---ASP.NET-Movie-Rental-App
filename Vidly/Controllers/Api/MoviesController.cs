using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext context;
        public MoviesController()
        {
            this.context = new ApplicationDbContext();
        }

        //GET /api/movies
        public IEnumerable<MovieDto> GetCustomers()
        {
            return context.Movies
                .Include(m => m.Genre)
                .Select(Mapper.Map<Movie, MovieDto>)
                .ToList();
        }

        //GET /api/movies/1
        public IHttpActionResult GetCustomer(int id)
        {
            var movie = context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateCustomer(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            context.Movies.Add(movie);
            context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT /api/movies/1
        [HttpPut]
        public void UpdateCustomer(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var movieInDb = context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(movieDto, movieInDb);

            context.SaveChanges();

        }

        //DELETE /api/movies/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var moviesInDb = context.Movies.SingleOrDefault(m => m.Id == id);
            if (moviesInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            context.Movies.Remove(moviesInDb);
            context.SaveChanges();
        }
    }
}
