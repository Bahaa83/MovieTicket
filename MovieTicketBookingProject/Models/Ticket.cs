using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        [Column("Seat Number")]
        public int NumOfSeats { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        [Column("Date")]
        public DateTime TicketsDate { get; set; }
        public decimal Amount { get; set; }
        [Column("Movie Name")]
        public int MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie  Movie { get; set; }

    }
}
