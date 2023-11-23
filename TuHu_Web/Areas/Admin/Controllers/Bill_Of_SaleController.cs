using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuHu_Web.Models;

namespace TuHu_Web.Areas.Admin.Controllers
{
    public class Bill_Of_SaleController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Bill_Of_Sale
        public ActionResult Index()
        {
            var bill_Of_Sale = db.Bill_Of_Sale.Include(b => b.Customer).Include(b => b.Staff);
            return View(bill_Of_Sale.ToList());
        }

        // GET: Admin/Bill_Of_Sale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Of_Sale bill_Of_Sale = db.Bill_Of_Sale.Find(id);
            if (bill_Of_Sale == null)
            {
                return HttpNotFound();
            }
            return View(bill_Of_Sale);
        }

        // GET: Admin/Bill_Of_Sale/Create
        public ActionResult Create()
        {
            ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "Name_Customer");
            ViewBag.Id_Staff = new SelectList(db.Staffs, "Id_Staff", "Name_Of_Staff");
            return View();
        }

        // POST: Admin/Bill_Of_Sale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Bill_Of_Sale,Id_Staff,Id_Customer,Date_And_Time")] Bill_Of_Sale bill_Of_Sale)
        {
            if (ModelState.IsValid)
            {
                db.Bill_Of_Sale.Add(bill_Of_Sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "Name_Customer", bill_Of_Sale.Id_Customer);
            ViewBag.Id_Staff = new SelectList(db.Staffs, "Id_Staff", "Name_Of_Staff", bill_Of_Sale.Id_Staff);
            return View(bill_Of_Sale);
        }

        // GET: Admin/Bill_Of_Sale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Of_Sale bill_Of_Sale = db.Bill_Of_Sale.Find(id);
            if (bill_Of_Sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "Name_Customer", bill_Of_Sale.Id_Customer);
            ViewBag.Id_Staff = new SelectList(db.Staffs, "Id_Staff", "Name_Of_Staff", bill_Of_Sale.Id_Staff);
            return View(bill_Of_Sale);
        }

        // POST: Admin/Bill_Of_Sale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Bill_Of_Sale,Id_Staff,Id_Customer,Date_And_Time")] Bill_Of_Sale bill_Of_Sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_Of_Sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Customer = new SelectList(db.Customers, "Id_Customer", "Name_Customer", bill_Of_Sale.Id_Customer);
            ViewBag.Id_Staff = new SelectList(db.Staffs, "Id_Staff", "Name_Of_Staff", bill_Of_Sale.Id_Staff);
            return View(bill_Of_Sale);
        }

        // GET: Admin/Bill_Of_Sale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill_Of_Sale bill_Of_Sale = db.Bill_Of_Sale.Find(id);
            if (bill_Of_Sale == null)
            {
                return HttpNotFound();
            }
            return View(bill_Of_Sale);
        }

        // POST: Admin/Bill_Of_Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill_Of_Sale bill_Of_Sale = db.Bill_Of_Sale.Find(id);
            db.Bill_Of_Sale.Remove(bill_Of_Sale);
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
    }
}
