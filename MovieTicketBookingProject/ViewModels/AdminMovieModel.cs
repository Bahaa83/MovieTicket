using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.ViewModels
{
    public class AdminMovieModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        [Display(Name ="Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Genre is required")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "The date and time is required")]
        public DateTime DateofShow { get; set; }
        [Required(ErrorMessage = "The Price is required")]

        public int Price { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Number of places available is required")]
        public int AvailableSeats { get; set; }
    }
}
