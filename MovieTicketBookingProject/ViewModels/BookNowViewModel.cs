using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.ViewModels
{
    public class BookNowViewModel
    {
        [Required(ErrorMessage = "Movies Name is Required")]
        public string Movie_Name { get; set; }
        public DateTime Movie_Date { get; set; }
        public string User_Name { get; set; }
        public string Movie_Image { get; set; }


        [Required(ErrorMessage ="Your Email is required")]
        [DataType(DataType.EmailAddress)]
        public string  User_Email { get; set; }



        [Required(ErrorMessage = "Choose Number Of Seats")]
        public int NumberOfSeats { get; set; }
       
    }
}
