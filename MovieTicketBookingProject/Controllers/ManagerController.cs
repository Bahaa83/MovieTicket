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
    public class ManagerController : Controller
    {
        private Context context;
        private MovieInterface Movie;
        private UploadInterface _upload;

        public ManagerController(Context _context, MovieInterface _Movie, UploadInterface upload)
        {
            context = _context;
            Movie = _Movie;
            _upload = upload;
        }
        public ActionResult UserErrorLoginMassage()
        {

           
            return View();
        }
        public ActionResult HomebackIndex()
        {

            var movies = Movie.GetAllMovies();
            return View(movies);
        }
        public ActionResult AdminIndex()
        {

            var movies = Movie.GetAllMovies();
            return View(movies);
        }
        public ActionResult UserIndex()
        {
            var movies = Movie.GetAllMovies();
            return View(movies);
        }


        public ActionResult Register()
        {
            SignUpViewModel Signup = new SignUpViewModel();

            return View(Signup);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(SignUpViewModel newUser)
        {
            try
            {
                var users = context.Users.ToList();
                foreach (var item in users )
                {
                    if(item.Email==newUser.Email)
                    {
                        ViewBag.ErrorMessage = " Email already exist";
                    }
                    else if(item.Email != newUser.Email)
                    {
                        User userdb = new User();
                        userdb.FirstName = newUser.FirstName;
                        userdb.LastName = newUser.LastName;
                        userdb.Email = newUser.Email;
                        userdb.Password = newUser.Password;
                        context.Add(userdb);
                        context.SaveChanges();
                        return RedirectToAction(nameof(UserIndex));
                    }

                }
              

               return RedirectToAction(nameof(UserErrorLoginMassage));

            }
            catch
            {
                return View();
            }
        }


        public ActionResult Login()
        {
            var loginvm = new LoginViewModel();

            return View(loginvm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            try
            {
                var admins = context.Admins.ToList();
                var users = context.Users.ToList();
                // TODO: Add insert logic here
                foreach (var item in admins)
                {
                    if (item.Email == viewmodel.Email)
                    {
                        return RedirectToAction(nameof(AdminIndex));
                    }
                    else
                    {
                        foreach (var u in users)
                        {
                            if (u.Email == viewmodel.Email && item.Password == viewmodel.Password)
                            {
                                return RedirectToAction(nameof(UserIndex));
                            }
                        }
                    }
                    
                }
               


                return RedirectToAction(nameof(UserErrorLoginMassage));
            }
            catch
            {
                return View();
            }
        }

     
        public ActionResult AdminDetails(int id)
        {
            var movie = Movie.Find(id);

            return View(movie);
        }
        public ActionResult AdminCreate()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreate(IList<IFormFile> files, AdminMovieModel vmodel, Movie movie)
        {
            try
            {
                string path = string.Empty;
                movie.Movie_Name = vmodel.Name;
                movie.Movie_Type = vmodel.Genre;
                movie.SeatsLeft = vmodel.AvailableSeats;
                movie.Showtime = vmodel.DateofShow;
                movie.Price = vmodel.Price;
                foreach (var item in files)
                {
                    path = Path.GetFileName(item.FileName.Trim());
                    movie.Movie_ImageUrl = "~/uploads/" + path;
                }
                _upload.uploadfilemultiple(files);
                Movie.AddMovie(movie);

                return RedirectToAction(nameof(AdminIndex));
            }
            catch
            {
                return View();
            }
        }



        public ActionResult AdminEdit(int id)
        {
            Movie movie = Movie.Find(id);
            var vmodel = new AdminMovieModel();
            vmodel.ID = movie.ID;
            vmodel.Name = movie.Movie_Name;
            vmodel.Genre = movie.Movie_Type;
            vmodel.DateofShow = movie.Showtime;
            vmodel.AvailableSeats = movie.SeatsLeft;
            vmodel.Price = movie.Price;
            vmodel.ImageUrl = movie.Movie_ImageUrl;

            return View(vmodel);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEdit(IList<IFormFile> files, AdminMovieModel vmodel, Movie movie)
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
                movie.Price = vmodel.Price;
                movie.Movie_ImageUrl = vmodel.ImageUrl;
                string path = string.Empty;
                string newpath = string.Empty;
                string oldpath = movie.Movie_ImageUrl;
              

                    foreach (var item in files)
                    {
                        path = Path.GetFileName(item.FileName.Trim());
                        newpath = "~/uploads/" + path;
                    }
                if (newpath == string.Empty)
                {
                    movie.Movie_ImageUrl = oldpath;
                   
                }
                else if (newpath !=string.Empty)
                {
                    movie.Movie_ImageUrl = newpath;
                    _upload.uploadfilemultiple(files);
                }
                    context.Update(movie);
                    context.SaveChanges();
                return RedirectToAction(nameof(AdminIndex));
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
                var movie = context.Movies.SingleOrDefault(m => m.ID == id);
                context.Remove(movie);
                context.SaveChanges();

                return RedirectToAction(nameof(AdminIndex));
            }
            catch
            {
                return View();
            }
        }
    }
}