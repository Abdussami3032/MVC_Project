using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        public Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Company_Admin.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {


            using (Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities())
            {
                var adm = db.Company_Admin.Where(b => b.Admin_Name.Equals(Username) && b.Admin_Pass.Equals(Password)).FirstOrDefault();
                if (adm != null)
                {
                    Session["Admin"] = adm.Admin_Id.ToString();
                    Session["AdminName"] = adm.Admin_Name.ToString();
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
            return RedirectToAction("Index");
        }
        public ActionResult Customer()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult Vehicle()
        {
            return View(db.Vehicles.ToList());
        }
        public ActionResult Insurance()
        {
            return View(db.InsurancePolicies.ToList());
        }
        public ActionResult Purchase()
        {
            return View(db.Purchases.ToList());
        }
        public ActionResult Bill()
        {
            return View(db.Billings.ToList());
        }
        public ActionResult Claim()
        {
            return View(db.Claims.ToList());
        }
        public ActionResult Comp_Exp()
        {
            return View(db.Company_Expense.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Admin_Id,Admin_Name,Admin_Pass,Admin_Email,Admin_Phone")] Company_Admin company_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Company_Admin.Add(company_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company_Admin);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Admin company_Admin = db.Company_Admin.Find(id);
            if (company_Admin == null)
            {
                return HttpNotFound();
            }
            return View(company_Admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Admin_Id,Admin_Name,Admin_Pass,Admin_Email,Admin_Phone")] Company_Admin company_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company_Admin);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Admin company_Admin = db.Company_Admin.Find(id);
            if (company_Admin == null)
            {
                return HttpNotFound();
            }
            return View(company_Admin);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company_Admin company_Admin = db.Company_Admin.Find(id);
            db.Company_Admin.Remove(company_Admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Admin company_Admin = db.Company_Admin.Find(id);
            if (company_Admin == null)
            {
                return HttpNotFound();
            }
            return View("Index");

        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Admin_Data()
        {
            return PartialView(db.Company_Admin.ToList());
        }
        public ActionResult Testimonials()
        {
            return View();
        }
        public ActionResult Testmonial_Data()
        {
            return PartialView(db.testimonials.ToList());
        }
        public ActionResult Sitemap()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Services_Data()
        {
            return PartialView(db.InsurancePolicies.ToList());
        }
        public ActionResult Team_Data()
        {
            return PartialView(db.Company_Admin.ToList());
        }
    }
}
