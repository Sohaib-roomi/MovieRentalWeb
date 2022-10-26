using MovieRentalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalApp.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Register(User user)
        {
            if (user != null)
            {
                _context.User.Add(user);
                var status = _context.SaveChanges();
            }

            return RedirectToAction("Login", "MyAccounts");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var ifexist = _context.User.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();

            if (ifexist != null)
            {
                Session["userid"] = ifexist.Id.ToString();
                Session["username"] = ifexist.UserName.ToString();
                TempData["Success"] = "Login Succsessfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Register First";
                return RedirectToAction("Register", "MyAccounts");
            }

            return View();
        }


    }
}