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
    [Authorize]
    public class ProvidersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Providers
        public ActionResult Index(int page = 1, bool isReset = false)
        {


            List<Provider> foods = new List<Provider>();

            foods = db.Providers.ToList();
            var valueSearch = Request.QueryString["valueSearch"];
            if (!string.IsNullOrEmpty(valueSearch))
            {
                foods = foods.FindAll(x =>

                   x.Name_Provider != null && x.Name_Provider.ToLower().Contains(valueSearch.Trim().ToLower())

                );
            }


            int itemsPerPage = 4;
            int totalItems = foods.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));

            var startIndex = (page - 1) * itemsPerPage;
            var endIndex = Math.Min(startIndex + itemsPerPage - 1, totalItems - 1);

            List<Provider> foodPage;

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

        // GET: Admin/Providers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // GET: Admin/Providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Provider,Name_Provider,Address_Provider,Phone_Number_Provider,Email_Provider")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(provider);
        }

        // GET: Admin/Providers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Admin/Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Provider,Name_Provider,Address_Provider,Phone_Number_Provider,Email_Provider")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provider);
        }

        // GET: Admin/Providers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Admin/Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provider provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
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
