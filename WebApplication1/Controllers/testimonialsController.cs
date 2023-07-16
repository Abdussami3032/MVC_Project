using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class testimonialsController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();

        // GET: testimonials
        public ActionResult Testimonial()
        {
            var testimonials = db.testimonials.Include(t => t.Customer);
            return View(testimonials.ToList());
        }

        // GET: testimonials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testimonial testimonial = db.testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // GET: testimonials/Create
        public ActionResult Create()
        {
            ViewBag.Cust_Name = new SelectList(db.Customers, "Cust_Id", "Cust_Name");
            return View();
        }

        // POST: testimonials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Test_ID,Cust_Test,Cust_Name")] testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                db.testimonials.Add(testimonial);
                db.SaveChanges();
                return RedirectToAction("Testimonial");
            }

            ViewBag.Cust_Name = new SelectList(db.Customers, "Cust_Id", "Cust_Name", testimonial.Cust_Name);
            return View(testimonial);
        }

        // GET: testimonials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testimonial testimonial = db.testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cust_Name = new SelectList(db.Customers, "Cust_Id", "Cust_Name", testimonial.Cust_Name);
            return View(testimonial);
        }

        // POST: testimonials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Test_ID,Cust_Test,Cust_Name")] testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testimonial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Testimonial");
            }
            ViewBag.Cust_Name = new SelectList(db.Customers, "Cust_Id", "Cust_Name", testimonial.Cust_Name);
            return View(testimonial);
        }

        // GET: testimonials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testimonial testimonial = db.testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            testimonial testimonial = db.testimonials.Find(id);
            db.testimonials.Remove(testimonial);
            db.SaveChanges();
            return RedirectToAction("Testimonial");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
