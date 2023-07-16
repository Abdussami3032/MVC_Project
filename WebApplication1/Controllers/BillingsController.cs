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
    public class BillingsController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();

        // GET: Billings
        public ActionResult Bills()
        {
            var billings = db.Billings.Include(b => b.Purchase);
            return View(billings.ToList());
        }
        public ActionResult BillsByCustomer(int? id)
        {
            List<Billing> bills = db.Billings.Where(x => x.Pur_Details == id).ToList();
            return View("Bills",bills );
        }

        // GET: Billings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billing billing = db.Billings.Find(id);
            if (billing == null)
            {
                return HttpNotFound();
            }
            return View(billing);
        }

        // GET: Billings/Create
        public ActionResult Create()
        {
            ViewBag.Pur_Details = new SelectList(db.Purchases, "Pur_Id", "Pur_Date");
            return View();
        }

        // POST: Billings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Billing billing)
        {
            if (ModelState.IsValid)
            {

                billing.Bill_Id = Convert.ToInt32(Session["UserID"]);
                db.Billings.Add(billing);
                db.SaveChanges();
                return RedirectToAction("BillsByCustomer", new { id = Convert.ToInt32(Session["UserID"]) });
            }

            ViewBag.Pur_Details = new SelectList(db.Purchases, "Pur_Id", "Pur_Date", billing.Pur_Details);
            return View(billing);
        }

        // GET: Billings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billing billing = db.Billings.Find(id);
            if (billing == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pur_Details = new SelectList(db.Purchases, "Pur_Id", "Pur_Date", billing.Pur_Details);
            return View(billing);
        }

        // POST: Billings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Bill_Id,Pur_Details,Bill_Date,Pur_Amount")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Bills");
            }
            ViewBag.Pur_Details = new SelectList(db.Purchases, "Pur_Id", "Pur_Date", billing.Pur_Details);
            return View(billing);
        }

        // GET: Billings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billing billing = db.Billings.Find(id);
            if (billing == null)
            {
                return HttpNotFound();
            }
            return View(billing);
        }

        // POST: Billings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Billing billing = db.Billings.Find(id);
            db.Billings.Remove(billing);
            db.SaveChanges();
            return RedirectToAction("Bills");
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
