using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
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


        public ActionResult SignUp()
        {
            SignUpViewModel Signup = new SignUpViewModel();

            return View(Signup);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel Vmodel, User _user)
        {
            bool exist = false;
            if (ModelState.IsValid)
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    if (user.Email == Vmodel.Email)
                    {
                        exist = true;
                        ModelState.AddModelError("", "It looks like your email is already exist, you should login");
                        return View();
                    }
                }
                if (exist == false)
                {
                    _user = new User()
                    {
                        FirstName = Vmodel.FirstName,
                        LastName = Vmodel.LastName,
                        Email = Vmodel.Email,
                        Password = Vmodel.Password
                    };
                    context.Add(_user);
                    context.SaveChanges();
                    return RedirectToAction(nameof(UserIndex));

                }
                //else
                //{
                //    ModelState.AddModelError("", "You have to fill all the required fields!");
                //    return View();
                //}

            }
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View();
            //return RedirectToAction(nameof(UserErrorLoginMassage));
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
            if (ModelState.IsValid)
            {

                var admins = context.Admins.ToList();
                var users = context.Users.ToList();
                foreach (var admin in admins)
                {
                    if (admin.Email == viewmodel.Email && admin.Password == viewmodel.Password)
                    {

                        return RedirectToAction(nameof(AdminIndex));
                    }
                    else if (admin.Email == viewmodel.Email && admin.Password != viewmodel.Password)
                    {
                        ModelState.AddModelError("Password", "It seems this  password is not correct for admin");
                        return View();
                    }
                    else if (admin.Email != viewmodel.Email && admin.Password == viewmodel.Password)
                    {
                        ModelState.AddModelError("Email", "It seems this Email  is not correct for admin");
                        return View();
                    }
                }
                foreach (var u in users)
                {
                    if (u.Email == viewmodel.Email && u.Password == viewmodel.Password)
                    {

                        return RedirectToAction(nameof(UserIndex));
                    }
                    if (u.Email == viewmodel.Email && u.Password != viewmodel.Password)
                    {
                        ModelState.AddModelError("Password", " password is not correct ");
                        return View();
                    }
                    if (u.Email != viewmodel.Email && u.Password == viewmodel.Password)
                    {
                        ModelState.AddModelError("Email", " Email  is not correct ");
                        return View();
                    }

                }
            }         
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View();
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
        public ActionResult BookNow(int id)
        {
            var movie = Movie.Find(id);
            BookNowViewModel vmodel = new BookNowViewModel();
            vmodel.Movie_Name = movie.Movie_Name;
            vmodel.Movie_Date = movie.Showtime;
            vmodel.Movie_Image = movie.Movie_ImageUrl;
            
           

            return View(vmodel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookNow(BookNowViewModel viewModel,Ticket ticket)
        {
            bool exist = false;
            int userId = 0;
            string image = viewModel.Movie_Image;
            if (ModelState.IsValid)
            {
                var users = context.Users.ToList();
               foreach(var user in users)
                {
                    if(user.Email == viewModel.User_Email)
                    {
                        exist = true;
                        userId = user.ID;
                       
                    }
                }
               if(exist==false)
                {

                    //var _movie = Movie.GetAllMovies().SingleOrDefault(m => m.Movie_Name == viewModel.Movie_Name);
                    ModelState.AddModelError("User_Email", " Your email  is not correct ");

                    //return RedirectToAction(nameof(BookNow),_movie.ID);
                    return View(viewModel);
                }
               if(exist==true)
                {
                    var _movie = Movie.GetAllMovies().SingleOrDefault(m => m.Movie_Name == viewModel.Movie_Name);
                    ticket = new Ticket();
                    ticket.TicketsDate = DateTime.Now;
                    ticket.UserId = userId;
                    ticket.MovieID = _movie.ID;
                    if (viewModel.NumberOfSeats == 0)
                    {
                        ModelState.AddModelError("NumberOfSeats", "You should choose the number of tickets");
                        return View(viewModel);
                    }
                  
                    if (_movie.SeatsLeft - viewModel.NumberOfSeats < 0)
                    {
                        ModelState.AddModelError("NumberOfSeats", "There are not enough seats");
                        return View(viewModel);
                    }
                    else
                    {
                        ticket.Amount = viewModel.NumberOfSeats * _movie.Price;
                        ticket.NumOfSeats = viewModel.NumberOfSeats;
                        _movie.SeatsLeft -= viewModel.NumberOfSeats;
                        context.Update(_movie);
                        context.Add(ticket);
                        context.SaveChanges();
                        return RedirectToAction(nameof(TicketConfirm), new { id = ticket.ID });
                    }
                }
            }
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View(viewModel);
        }
        public ActionResult TicketConfirm(int id)
        {
            var ticket = context.Tickets.SingleOrDefault(T => T.ID == id);
            TicketConfirmViewModel viewModel = new TicketConfirmViewModel();
            viewModel.Amount = ticket.Amount;
            Movie movie = Movie.Find(ticket.MovieID);
            viewModel.Movie_Name = movie.Movie_Name;
            viewModel.NumOfSeats = ticket.NumOfSeats;
            viewModel.ShowDate = movie.Showtime;
            viewModel.TicketsDate = ticket.TicketsDate;
            var user = context.Users.SingleOrDefault(U => U.ID == ticket.UserId);
            viewModel.User_Name = user.FirstName +" "+ user.LastName;

            return View(viewModel);
        }
    }
}