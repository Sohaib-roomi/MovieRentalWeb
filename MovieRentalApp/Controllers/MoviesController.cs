using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using MovieRentalApp.Models;
using System.Web.Mvc;
using MovieRentalApp.ViewModel;
using System.IO;

namespace MovieRentalApp.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.Genre).Where(m => m.Id == id).FirstOrDefault();

            if (movies == null)
                return HttpNotFound();
            else
            {
                return View(movies);
            }
        }

        public ActionResult AddNewMovie()
        {
            var Genres = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genre = Genres
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddNewMovie(MovieFormViewModel model,HttpPostedFileBase ImageURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Movies movies1 = new Movies();

                    movies1.Name = model.Movies.Name;
                    movies1.ReleaseDate = model.Movies.ReleaseDate;
                    movies1.NumberInStock = model.Movies.NumberInStock;
                    movies1.GenreId = model.Movies.GenreId;

                    movies1.DateAdded = DateTime.Now;

                    if (ImageURL != null)
                    {
                        string name = Path.GetFileName(ImageURL.FileName);
                        string path = Server.MapPath("~/Images/MovieCovers/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        ImageURL.SaveAs(path + name);
                        movies1.ImageURL = name;
                    }
                    

                    //string path = Path.Combine(Server.MapPath("~/Images/MovieCovers/"), Path.GetFileName(ImageURL.FileName));
                    //ImageURL.SaveAs(path);

                    //movies1.ImageURL = path;

                    _context.Movies.Add(movies1);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Movies");
                }
                var Genres = _context.Genre.ToList();
                var viewModel = new MovieFormViewModel
                {
                    Genre = Genres
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {

                ViewBag.erro = "Exception : " + ex.Message;
                return View();
            }
        }

        public ActionResult EditMov(int id)
        {
            var movies = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            var viewModel = new MovieFormViewModel
            {
                Movies = movies,
                Genre = _context.Genre.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditMov(MovieFormViewModel model, HttpPostedFileBase ImageURL)
        {
            if (model.Movies.Id == 0)
            {
                return HttpNotFound();
            }


            var moviesInDB = _context.Movies.Single(m => m.Id == model.Movies.Id);
            moviesInDB.Name = model.Movies.Name;
            moviesInDB.ReleaseDate = model.Movies.ReleaseDate;
            moviesInDB.NumberInStock = model.Movies.NumberInStock;
            moviesInDB.GenreId = model.Movies.GenreId;

            if (ImageURL != null)
            {
                string name = Path.GetFileName(ImageURL.FileName);
                string path = Server.MapPath("~/Images/MovieCovers/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                ImageURL.SaveAs(path + name);
                moviesInDB.ImageURL = name;
            }
            

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Delete(int Id)
        {
            var data = _context.Movies.Where(m => m.Id == Id).FirstOrDefault();
            _context.Movies.Remove(data);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Movies");

        }
    }
}