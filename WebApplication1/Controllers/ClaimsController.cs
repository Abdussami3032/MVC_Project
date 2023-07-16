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
    public class ClaimsController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();

        // GET: Claims
        public ActionResult Claim()
        {
            var claims = db.Claims.Include(c => c.Billing);
            return View(claims.ToList());
        }
        public ActionResult ClaimByCustomer(int? id)
        {
            List<Claim> claim = db.Claims.Where(x => x.Bill_Id == id).ToList();
            return View("Claim", claim);
        }
        // GET: Claims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // GET: Claims/Create
        public ActionResult Create()
        {
            ViewBag.Bill_Id = new SelectList(db.Billings, "Bill_Id", "Bill_Date");
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Claim_Id,Bill_Id,Claim_Date,Accident_Place,Accident_Description,Claim_Amount")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.Bill_Id = Convert.ToInt32(Session["UserID"]);
                db.Claims.Add(claim);
                db.SaveChanges();
                return RedirectToAction("ClaimByCustomer", new { id = Convert.ToInt32(Session["UserID"]) });
                
            }

            ViewBag.Bill_Id = new SelectList(db.Billings, "Bill_Id", "Bill_Date", claim.Bill_Id);
            return View(claim);
        }

        // GET: Claims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bill_Id = new SelectList(db.Billings, "Bill_Id", "Bill_Date", claim.Bill_Id);
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Claim_Id,Bill_Id,Claim_Date,Accident_Place,Accident_Description,Claim_Amount")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Claim");
            }
            ViewBag.Bill_Id = new SelectList(db.Billings, "Bill_Id", "Bill_Date", claim.Bill_Id);
            return View(claim);
        }

        // GET: Claims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Claim claim = db.Claims.Find(id);
            db.Claims.Remove(claim);
            db.SaveChanges();
            return RedirectToAction("Claim");
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
