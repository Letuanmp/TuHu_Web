using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuHu_Web.Models;

namespace TuHu_Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Products
        public ActionResult Index(int page = 1, bool isReset = false)
        {
            

            List<Product> foods = new List<Product>();
           
            foods = db.Products.ToList();
            var valueSearch = Request.QueryString["valueSearch"];
            if (!string.IsNullOrEmpty(valueSearch))
            {
                foods = foods.FindAll(x =>

                   x.Name_Product != null && x.Name_Product.ToLower().Contains(valueSearch.Trim().ToLower())

                );
            }


            int itemsPerPage = 4;
            int totalItems = foods.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));

            var startIndex = (page - 1) * itemsPerPage;
            var endIndex = Math.Min(startIndex + itemsPerPage - 1, totalItems - 1);

            List<Product> foodPage;

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

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Product,Name_Product,Price_Product,Inventory_Number,Description,Image")] Product product, HttpPostedFileBase urlFile)
        {
            if (ModelState.IsValid)
            {
                if (urlFile != null && urlFile.ContentLength > 0)
                {
                    // Handle file upload
                    string fileName = Path.GetFileName(urlFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    urlFile.SaveAs(path);
                    product.Image = fileName.ToString();
                }

                // Rest of your code to add the product to the database and save changes
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Product,Name_Product,Price_Product,Inventory_Number,Description,Image")] Product product, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName); // Added a path separator ("/")
                    imageFile.SaveAs(path);
                    product.Image = fileName;
                    db.Entry(product).State = EntityState.Modified;
                }
                else
                {
                    var existingFood = db.Products.FirstOrDefault(item => item.Id_Product == product.Id_Product);

                    if (existingFood != null)
                    {
                        existingFood.Id_Product = product.Id_Product;
                        existingFood.Name_Product = product.Name_Product;
                        existingFood.Price_Product = product.Price_Product;

                        existingFood.Inventory_Number = product.Inventory_Number;
                        existingFood.Description = product.Description;
                        db.Entry(existingFood).State = EntityState.Modified;
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
