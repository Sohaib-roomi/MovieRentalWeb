using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MovieRentalApp.Models;
using MovieRentalApp.ViewModel;
using System.IO;

namespace MovieRentalApp.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customer = _context.Customer.Include(c => c.MembershipType).ToList();

            return View(customer);
        }

        public ActionResult Details(int id)
        {
            var customers = _context.Customer.Include(c => c.MembershipType).ToList().Where(c => c.Id == id).FirstOrDefault();
            if (customers == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customers);
            }

        }

        public ActionResult AddNewCustomer()
        {
            var MembershipTypes = _context.MembershipType.ToList();
            var viewModel = new CustomerFormVIewModel
            {
                MembershipType = MembershipTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddNewCustomer(Customer customer, HttpPostedFileBase ImageURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ImageURL != null)
                    {
                        string name = Path.GetFileName(ImageURL.FileName);
                        string path = Server.MapPath("~/Images/CustomerPics/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        ImageURL.SaveAs(path + name);
                        customer.ImageURL = name;
                    }
                    _context.Customer.Add(customer);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Customer");
                }
                var MembershipTypes = _context.MembershipType.ToList();
                var viewModel = new CustomerFormVIewModel
                {
                    MembershipType = MembershipTypes
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {

                ViewBag.error = "Exception : " + ex.Message;
                return View();
            }
        }

        public ActionResult EditCust(int id)
        {
            var customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormVIewModel
            {
                Customer = customer,
                MembershipType = _context.MembershipType.ToList()
            };

            return View(viewModel);


        }

        [HttpPost]
        public ActionResult EditCust(CustomerFormVIewModel model, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                if (model.Customer.Id == 0)
                {
                    return HttpNotFound();
                }

                var customerInDB = _context.Customer.Single(c => c.Id == model.Customer.Id);
                customerInDB.Name = model.Customer.Name;
                customerInDB.Email = model.Customer.Email;
                customerInDB.Birthdate = model.Customer.Birthdate;
                customerInDB.isSubscribed = model.Customer.isSubscribed;
                customerInDB.MembershipTypeId = model.Customer.MembershipTypeId;

                if (ImageURL != null)
                {
                    string name = Path.GetFileName(ImageURL.FileName);
                    string path = Server.MapPath("~/Images/CustomerPics/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    ImageURL.SaveAs(path + name);
                    customerInDB.ImageURL = name;
                }

               
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Customer");
               
            }

            var customer = _context.Customer.SingleOrDefault(c => c.Id == model.Customer.Id);
            var viewModel = new CustomerFormVIewModel
            {
                Customer = customer,
                MembershipType = _context.MembershipType.ToList()
            };

            return View(viewModel);

        }

        public ActionResult Delete(int Id)
        {
            var data = _context.Customer.Where(m => m.Id == Id).FirstOrDefault();
            _context.Customer.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");

        }

    }
}