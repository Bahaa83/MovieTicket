using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTicketBookingProject.Data;
using MovieTicketBookingProject.Models;
using MovieTicketBookingProject.Services;
using MovieTicketBookingProject.ViewModels;

namespace MovieTicketBookingProject.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private MovieInterface Movie;
        private Context _context;
        public HomeController(MovieInterface _Movie, Context context)
        {
            Movie = _Movie;
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = Movie.GetAllMovies();
            return View(movies);
        }
        //public IActionResult BookNow(int id)
        //{
        //    UserBookingViewModel viewModel = new UserBookingViewModel();

        //    Movie movie = Movie.Find(id);

        //    viewModel.MovieId = movie.ID;
        //    viewModel.Movie_Name = movie.Movie_Name;
        //    viewModel.Movie_Date = movie.Showtime;
            

        //    return View(viewModel);

        //}
        [HttpPost]
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
