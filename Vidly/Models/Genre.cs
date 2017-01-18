using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Genre
    {
        [Display(Name = "Genre")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}