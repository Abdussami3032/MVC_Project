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
    public class PurchasesController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();

        // GET: Purchases
        public ActionResult Purchase()
        {
            var purchases = db.Purchases.Include(p => p.Pur_Date).Include(p => p.Vehicle);
            return View(purchases.ToList());
        }
        public ActionResult PurchaseByCustomer(int? id)
        {
            List<Purchase> Purchase = db.Purchases.Where(x => x.Pur_Pol == id).ToList();
            return View("Purchase", Purchase);
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.Pur_Pol = new SelectList(db.InsurancePolicies, "Pol_Id", "PolicyName");
            ViewBag.Vehicle = new SelectList(db.Vehicles, "Veh_Id", "Veh_Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pur_Id,Vehicle,Pur_Pol,Pur_Date,Total_Amt,PP_Status")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.Pur_Pol = Convert.ToInt32(Session["UserID"]);
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("PurchaseByCustomer", new { id = Convert.ToInt32(Session["UserID"]) });
            }

            ViewBag.Pur_Pol = new SelectList(db.InsurancePolicies, "Pol_Id", "PolicyName", purchase.Pur_Pol);
            ViewBag.Vehicle = new SelectList(db.Vehicles, "Veh_Id", "Veh_Name", purchase.Vehicle);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pur_Pol = new SelectList(db.InsurancePolicies, "Pol_Id", "PolicyName", purchase.Pur_Pol);
            ViewBag.Vehicle = new SelectList(db.Vehicles, "Veh_Id", "Veh_Name", purchase.Vehicle);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pur_Id,Vehicle,Pur_Pol,Pur_Date,Total_Amt,PP_Status")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Purchase");
            }
            ViewBag.Pur_Pol = new SelectList(db.InsurancePolicies, "Pol_Id", "PolicyName", purchase.Pur_Pol);
            ViewBag.Vehicle = new SelectList(db.Vehicles, "Veh_Id", "Veh_Name", purchase.Vehicle);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
            return RedirectToAction("Purchase");
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
