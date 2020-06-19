using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.ViewModels
{
    public class TicketConfirmViewModel
    {

        public string Movie_Name { get; set; }
        public int NumOfSeats { get; set; }
        public string User_Name { get; set; }
        public DateTime ShowDate { get; set; }
    
        public DateTime TicketsDate { get; set; }
        public decimal Amount { get; set; }
     
        
       
    }
}
