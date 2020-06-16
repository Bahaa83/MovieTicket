using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingProject.Data;
using MovieTicketBookingProject.ViewModels;

namespace MovieTicketBookingProject.Controllers
{
    public class LoginController : Controller
    {
        private Context context;
        public LoginController(Context _context)
        {
            context =  _context;

        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            LoginViewModel loginvm = new LoginViewModel();
            return View(loginvm);
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginViewModel loginvm)
        {
            try
            {
                var admins = context.Admins.ToList();
                // TODO: Add insert logic here
                foreach (var item in admins)
                {
                    if(item.Email==loginvm.Email)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return RedirectToAction(nameof(Index), "{controller=Home}");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}