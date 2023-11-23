using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TuHu_Web.Models;

namespace TuHu_Web.Areas.Admin.Controllers
{
    [Authorize]
    public class LoginsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Logins
        public ActionResult Index(int page = 1, bool isReset = false)
        {
            List<Login> login = new List<Login>();

            login = db.Logins.ToList();
            var valueSearch = Request.QueryString["valueSearch"];
            if (!string.IsNullOrEmpty(valueSearch))
            {
                login = login.FindAll(x =>

                   x.UserName != null && x.UserName.ToLower().Contains(valueSearch.Trim().ToLower())

                );
            }


            int itemsPerPage = 4;
            int totalItems = login.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            page = Math.Max(1, Math.Min(page, totalPages));

            var startIndex = (page - 1) * itemsPerPage;
            var endIndex = Math.Min(startIndex + itemsPerPage - 1, totalItems - 1);

            List<Login> foodPage;

            if (startIndex < 0 || startIndex >= totalItems)
            {
                foodPage = null;
            }
            else
            {
                foodPage = login.GetRange(startIndex, endIndex - startIndex + 1);
            }

            ViewBag.currentPage = page;
            Session["currentPageFood"] = page;
            ViewBag.totalPages = totalPages;




            return View(foodPage);
        }

        // GET: Admin/Logins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Admin/Logins/Create
        public ActionResult Create()
        {
            ViewBag.Id_Position = new SelectList(db.Positions, "Id_Position", "Name_Position");
            return View();
        }

        // POST: Admin/Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password,Id_Position")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Position = new SelectList(db.Positions, "Id_Position", "Name_Position", login.Id_Position);
            return View(login);
        }

        // GET: Admin/Logins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Position = new SelectList(db.Positions, "Id_Position", "Name_Position", login.Id_Position);
            return View(login);
        }

        // POST: Admin/Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Password,Id_Position")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Position = new SelectList(db.Positions, "Id_Position", "Name_Position", login.Id_Position);
            return View(login);
        }

        // GET: Admin/Logins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Admin/Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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
