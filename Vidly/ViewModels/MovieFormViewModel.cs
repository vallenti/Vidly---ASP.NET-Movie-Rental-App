using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            NumbersInStock = movie.NumbersInStock;
            ReleaseDate = movie.ReleaseDate;
        }
        public IEnumerable<Genre> Genres { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 20)]
        [Display(Name = "Numbers in Stock")]
        [Required]
        public int? NumbersInStock { get; set; }
        public string Title
        {
            get
            {
                if (Id == 0)
                {
                    return "New Movie";
                }
                return "Edit Movie";
            }
        }
    }
}