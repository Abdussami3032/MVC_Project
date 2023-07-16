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
    public class InsurancePoliciesController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();

        // GET: InsurancePolicies
        public ActionResult TypesOfInsurance()
        {
            return View(db.InsurancePolicies.ToList());
        }
        public ActionResult TypeByCustomer(int? id)
        {
            List<InsurancePolicy> policies = db.InsurancePolicies.Where(x => x.Pol_Id == id).ToList();
            return View("TypesOfInsurance", policies);
        }
        // GET: InsurancePolicies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePolicy insurancePolicy = db.InsurancePolicies.Find(id);
            if (insurancePolicy == null)
            {
                return HttpNotFound();
            }
            return View(insurancePolicy);
        }

        // GET: InsurancePolicies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsurancePolicies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pol_Id,PolicyName,Est_Amount,Ins_Amount,Pol_Duration")] InsurancePolicy insurancePolicy)
        {
            if (ModelState.IsValid)
            {
                insurancePolicy.Pol_Id = Convert.ToInt32(Session["UserID"]);
                db.InsurancePolicies.Add(insurancePolicy);
                db.SaveChanges();
                return RedirectToAction("TypeByCustomer", new { id = Convert.ToInt32(Session["UserID"]) });
                
            }

            return View(insurancePolicy);
        }

        // GET: InsurancePolicies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePolicy insurancePolicy = db.InsurancePolicies.Find(id);
            if (insurancePolicy == null)
            {
                return HttpNotFound();
            }
            return View(insurancePolicy);
        }

        // POST: InsurancePolicies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pol_Id,PolicyName,Est_Amount,Ins_Amount,Pol_Duration")] InsurancePolicy insurancePolicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insurancePolicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TypesOfInsurance");
            }
            return View(insurancePolicy);
        }

        // GET: InsurancePolicies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InsurancePolicy insurancePolicy = db.InsurancePolicies.Find(id);
            if (insurancePolicy == null)
            {
                return HttpNotFound();
            }
            return View(insurancePolicy);
        }

        // POST: InsurancePolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InsurancePolicy insurancePolicy = db.InsurancePolicies.Find(id);
            db.InsurancePolicies.Remove(insurancePolicy);
            db.SaveChanges();
            return RedirectToAction("TypesOfInsurance");
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
