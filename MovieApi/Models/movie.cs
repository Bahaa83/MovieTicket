using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Movie_Name { get; set; }
        [Column("Genre")]
        public string Movie_Type { get; set; }
        [Column("Seats Left")]
        public int SeatsLeft { get; set; }
        public DateTime Showtime { get; set; }
        public int Price { get; set; }


    }
}
