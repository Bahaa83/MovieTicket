using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.Models
{
    public class Admin
    {
        public int ID { get; set; }
      
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
