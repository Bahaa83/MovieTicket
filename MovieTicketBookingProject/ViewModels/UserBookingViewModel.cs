using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.ViewModels
{
    public class UserBookingViewModel
    {
        [Required(ErrorMessage = "Movies Name is Required")]
        public string Movie_Name { get; set; }
        public DateTime Movie_Date { get; set; }

        [Required(ErrorMessage = "Choose a Seat number")]
        public string SeatNo { get; set; }
        [Required(ErrorMessage = "Select the number of tickets")]
        public int Amount { get; set; }
        public int MovieId { get; set; }
    }
}
