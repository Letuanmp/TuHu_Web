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
    public class Sales_Bill_DetailsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Sales_Bill_Details
        public ActionResult Index(int page = 1, bool isReset = false)
        {


            List<Sales_Bill_Details> foods = new List<Sales_Bill_Details>();

            foods = db.Sales_Bill_Details.ToList();
            var valueSearch = Request.QueryString["valueSearch"];
            if (!string.IsNullOrEmpty(valueSearch))
            {
                foods = foods.FindAll(x =>

                   x.Bill_Of_Sale.Customer.Name_Customer != null && x.Bill_Of_Sale.Customer.Name_Customer.ToLower().Contains(valueSearch.Trim().ToLower())

                );
            }


            int itemsPerPage = 4;
            int totalItems = foods.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));

            var startIndex = (page - 1) * itemsPerPage;
            var endIndex = Math.Min(startIndex + itemsPerPage - 1, totalItems - 1);

            List<Sales_Bill_Details> foodPage;

            if (startIndex < 0 || startIndex >= totalItems)
            {
                foodPage = null;
            }
            else
            {
                foodPage = foods.GetRange(startIndex, endIndex - startIndex + 1);
            }

            ViewBag.currentPage = page;
            Session["currentPageFood"] = page;
            ViewBag.totalPages = totalPages;




            return View(foodPage);
        }

        // GET: Admin/Sales_Bill_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Bill_Details sales_Bill_Details = db.Sales_Bill_Details.Find(id);
            if (sales_Bill_Details == null)
            {
                return HttpNotFound();
            }
            return View(sales_Bill_Details);
        }

        // GET: Admin/Sales_Bill_Details/Create
        public ActionResult Create()
        {
            ViewBag.Id_Bill_Of_Sale = new SelectList(db.Bill_Of_Sale, "Id_Bill_Of_Sale", "Id_Bill_Of_Sale");
            ViewBag.Id_Product = new SelectList(db.Products, "Id_Product", "Name_Product");
            return View();
        }

        // POST: Admin/Sales_Bill_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Sales_Bill_Details,Id_Bill_Of_Sale,Id_Product,Amount,Price_Bill_Details")] Sales_Bill_Details sales_Bill_Details)
        {
            if (ModelState.IsValid)
            {
                db.Sales_Bill_Details.Add(sales_Bill_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Bill_Of_Sale = new SelectList(db.Bill_Of_Sale, "Id_Bill_Of_Sale", "Id_Bill_Of_Sale", sales_Bill_Details.Id_Bill_Of_Sale);
            ViewBag.Id_Product = new SelectList(db.Products, "Id_Product", "Name_Product", sales_Bill_Details.Id_Product);
            return View(sales_Bill_Details);
        }

        // GET: Admin/Sales_Bill_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Bill_Details sales_Bill_Details = db.Sales_Bill_Details.Find(id);
            if (sales_Bill_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Bill_Of_Sale = new SelectList(db.Bill_Of_Sale, "Id_Bill_Of_Sale", "Id_Bill_Of_Sale", sales_Bill_Details.Id_Bill_Of_Sale);
            ViewBag.Id_Product = new SelectList(db.Products, "Id_Product", "Name_Product", sales_Bill_Details.Id_Product);
            return View(sales_Bill_Details);
        }

        // POST: Admin/Sales_Bill_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Sales_Bill_Details,Id_Bill_Of_Sale,Id_Product,Amount,Price_Bill_Details")] Sales_Bill_Details sales_Bill_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales_Bill_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Bill_Of_Sale = new SelectList(db.Bill_Of_Sale, "Id_Bill_Of_Sale", "Id_Bill_Of_Sale", sales_Bill_Details.Id_Bill_Of_Sale);
            ViewBag.Id_Product = new SelectList(db.Products, "Id_Product", "Name_Product", sales_Bill_Details.Id_Product);
            return View(sales_Bill_Details);
        }

        // GET: Admin/Sales_Bill_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Bill_Details sales_Bill_Details = db.Sales_Bill_Details.Find(id);
            if (sales_Bill_Details == null)
            {
                return HttpNotFound();
            }
            return View(sales_Bill_Details);
        }

        // POST: Admin/Sales_Bill_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales_Bill_Details sales_Bill_Details = db.Sales_Bill_Details.Find(id);
            db.Sales_Bill_Details.Remove(sales_Bill_Details);
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
