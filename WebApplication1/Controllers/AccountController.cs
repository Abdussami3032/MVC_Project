using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Insurance()
        {
            return View();
        }
        public ActionResult Testimonials()
        {
            return View();
        }
        public ActionResult Sitemap()
        {
            return View();
        }
        public ActionResult Services_Data()
        {
            return  PartialView(db.InsurancePolicies.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Cust_Id,Cust_Name,Cust_Add,Cust_Phone,Cust_Email,Cust_Pass,Cust_CNIC")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(customer);
        }
        public ActionResult Testmonial_Data()
        {
            return PartialView(db.testimonials.ToList());
        }
        public ActionResult Vehicle_Data()
        {
            return PartialView(db.Vehicles.ToList());
        }
        public ActionResult ContactUS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            

            using (Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities())
            {
                var obj = db.Customers.Where(a => a.Cust_Name.Equals(Username) && a.Cust_Pass.Equals(Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.Cust_Id.ToString();
                    Session["UserEmail"] = obj.Cust_Email.ToString();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.message = "Inavlid Credentials";
            return View();

        }

        
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            return View("Index");
        }
        public ActionResult Team_Data()
        {
            return PartialView(db.Company_Admin.ToList());
        }
        
    }
       
}