using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentalApp.Models
{
    public class Movies
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required (ErrorMessage = "Name Must be Entered")]
        public string Name { get; set; }

        public Genre Genre { get; set; }
        
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }


        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }


        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }


        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }

        public string ImageURL { get; set; }
    }
}