using MovieTicketBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.Services
{
   public interface MovieInterface
    {
        void AddMovie(Movie movie);
        IEnumerable<Movie> GetAllMovies();
        void Delete(int id);
        Movie Find(int id);
        
    }
}
