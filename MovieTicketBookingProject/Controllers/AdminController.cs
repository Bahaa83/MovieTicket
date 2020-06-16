using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploadControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingProject.Data;
using MovieTicketBookingProject.Models;
using MovieTicketBookingProject.Services;
using MovieTicketBookingProject.ViewModels;

namespace MovieTicketBookingProject.Controllers
{
    public class AdminController : Controller
    {
        private Context context;
        private MovieInterface Movie;
        private UploadInterface _upload;
        public AdminController(Context _context, MovieInterface _Movie, UploadInterface upload)
        {
            context = _context;
            Movie = _Movie;
            _upload = upload;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var movies = Movie.GetAllMovies();
            return View(movies);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var movie = Movie.Find(id);

            return View(movie);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IList<IFormFile> files, AdminMovieModel vmodel, Movie movie)
        {
            try
            {
                string path = string.Empty;
                movie.Movie_Name = vmodel.Name;
                movie.Movie_Type = vmodel.Genre;
                movie.SeatsLeft = vmodel.AvailableSeats;
                movie.Showtime = vmodel.DateofShow;
                foreach (var item in files)
                {
                    path = Path.GetFileName(item.FileName.Trim());
                    movie.Movie_ImageUrl = "~/uploads/" + path;
                }
                _upload.uploadfilemultiple(files);
                Movie.AddMovie(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            Movie movie = Movie.Find(id);
            var vmodel = new AdminMovieModel();
            vmodel.ID = movie.ID;
            vmodel.Name = movie.Movie_Name;
            vmodel.Genre = movie.Movie_Type;
            vmodel.DateofShow = movie.Showtime;
            vmodel.AvailableSeats = movie.SeatsLeft;
            vmodel.ImageUrl = movie.Movie_ImageUrl;
            
            return View(vmodel);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IList<IFormFile> files, AdminMovieModel vmodel, Movie movie)
        {
            try
            {
                // TODO: Add update logic here
                movie = Movie.Find(vmodel.ID);
              
                movie.ID = vmodel.ID;
                movie.Movie_Name = vmodel.Name;
                movie.Movie_Type = vmodel.Genre;
                movie.SeatsLeft = vmodel.AvailableSeats;
                movie.Showtime = vmodel.DateofShow;
                movie.Movie_ImageUrl = vmodel.ImageUrl;
                string path = string.Empty;
                string temp = string.Empty;
                foreach (var item in files)
                {
                    path = Path.GetFileName(item.FileName.Trim());
                   temp = "~/uploads/" + path;
                }
                if (temp != movie.Movie_ImageUrl)
                {
                    movie.Movie_ImageUrl = temp;
                    _upload.uploadfilemultiple(files);
                    context.Update(movie);
                    context.SaveChanges();
                }
                else
                {
                   
                    context.Update(movie);
                    context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AdminDelete(int id)
        {
            Movie movie = Movie.Find(id);

            return View(movie);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                Movie.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}