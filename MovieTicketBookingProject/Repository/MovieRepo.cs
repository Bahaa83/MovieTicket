using MovieTicketBookingProject.Data;
using MovieTicketBookingProject.Models;
using MovieTicketBookingProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBookingProject.Repository
{
    public class MovieRepo : MovieInterface
    {
        private Context context;
        public MovieRepo(Context _context)
        {
            context = _context;

        }
        public void AddMovie(Movie movie)
        {
            context.Add(movie);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Movie movie = Find(id);
            context.Remove(movie);
            context.SaveChanges();
        }

        public Movie Find(int id)
        {
            Movie movie = context.Movies.SingleOrDefault(m => m.ID == id);
            return movie;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = context.Movies.ToList();
            return movies;
        }
    }
}
