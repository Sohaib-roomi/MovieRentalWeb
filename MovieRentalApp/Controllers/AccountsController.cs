using MovieRentalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalApp.Controllers
{
    public class AccountsController : Controller
    {

        private ApplicationDbContext _context;
        public AccountsController()
        {
            _context = new ApplicationDbContext();
        }

        public static string UserType = "";

        // GET: Accounts

        public ActionResult Index()
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
                    UserType = ifexist.UserType.ToString();
                    Session["username"] = ifexist.UserName.ToString();
                    TempData["username"] = Session["username"];

                    TempData["Success"] = "Login Succsessfully";
                    return RedirectToAction("Index", "Home");
                }

            else
            {
                TempData["Error"] = "Sign Up First";
                return RedirectToAction("Index", "Accounts");
            }
        }

        public ActionResult LogOut()
        {
            Session["userid"] = "";
            Session["username"] = "";
            TempData["username"] = "";
            return RedirectToAction("Index","Accounts");
        }

        public ActionResult SignUp()
        {

            return View();
        }


        [HttpPost]
        public ActionResult SignUp(UserModel model)
        {
            if (model.UserType == "Select User")
            {
                return View();
            }
            else
            {

                User NewUser = new User();

                if (ModelState.IsValid)
                {
                    /*if (model.EmailIsValid == true)
                    {
                        ModelState.AddModelError("", "Email Already Exists");
                        return View();
                    }*/
                    using (_context = new ApplicationDbContext())
                    {
                        NewUser.FirstName = model.FirstName;
                        NewUser.LastName = model.LastName;
                        NewUser.Email = model.Email;
                        NewUser.UserName = model.UserName;
                        NewUser.Password = model.Password;
                        NewUser.UserType = model.UserType;

                        _context.User.Add(NewUser);
                        _context.SaveChanges();
                        return View("Index");
                    }
                }
                ModelState.AddModelError("", "Model state is not valid");
                return View();

            }

        }

        [HttpPost]
        public JsonResult CheckEmail(string Email)
        {
            
            bool isValid = _context.User.ToList().Exists(p => p.Email.Equals(Email, StringComparison.CurrentCultureIgnoreCase));
            return Json(isValid);
        }

        /*[HttpPost]
        public ActionResult SignUp(User user)
        {
            if (user != null)
            {
                _context.User.Add(user);
                var status = _context.SaveChanges();
            }

            return RedirectToAction("Index", "Accounts");
        }*/



    }
}
