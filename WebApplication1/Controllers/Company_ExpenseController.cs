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
    public class Company_ExpenseController : Controller
    {
        private Vehicle_Insurance_ManagmentEntities db = new Vehicle_Insurance_ManagmentEntities();

        // GET: Company_Expense
        public ActionResult CompanyExp()
        {
            return View(db.Company_Expense.ToList());
        }

        // GET: Company_Expense/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Expense company_Expense = db.Company_Expense.Find(id);
            if (company_Expense == null)
            {
                return HttpNotFound();
            }
            return View(company_Expense);
        }

        // GET: Company_Expense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company_Expense/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Exp_Id,Exp_Description,Exp_Amt,Exp_Date")] Company_Expense company_Expense)
        {
            if (ModelState.IsValid)
            {
                db.Company_Expense.Add(company_Expense);
                db.SaveChanges();
                return RedirectToAction("CompanyExp");
            }

            return View(company_Expense);
        }

        // GET: Company_Expense/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Expense company_Expense = db.Company_Expense.Find(id);
            if (company_Expense == null)
            {
                return HttpNotFound();
            }
            return View(company_Expense);
        }

        // POST: Company_Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Exp_Id,Exp_Description,Exp_Amt,Exp_Date")] Company_Expense company_Expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company_Expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CompanyExp");
            }
            return View(company_Expense);
        }

        // GET: Company_Expense/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Expense company_Expense = db.Company_Expense.Find(id);
            if (company_Expense == null)
            {
                return HttpNotFound();
            }
            return View(company_Expense);
        }

        // POST: Company_Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company_Expense company_Expense = db.Company_Expense.Find(id);
            db.Company_Expense.Remove(company_Expense);
            db.SaveChanges();
            return RedirectToAction("CompanyExp");
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
