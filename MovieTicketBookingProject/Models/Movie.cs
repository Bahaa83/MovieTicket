using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The Movie name is required")]
        public string Movie_Name { get; set; }
        [Column("Genre")]
        [Required(ErrorMessage = "The Movie type is required")]
        public string Movie_Type { get; set; }
        [Column("Seats Left")]
        [Required(ErrorMessage = "The Seats Left is required")]
        public int SeatsLeft { get; set; }
        [Required(ErrorMessage = "The Showtime is required")]

        public DateTime Showtime { get; set; }
        [Column("Movie ImageUrl")]
        [Required(ErrorMessage = "The Image is required")]
        public string Movie_ImageUrl { get; set; }
        public int Price { get; set; }

        
    }
}
