using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceSite.Models;

namespace EcommerceSite.Controllers
{
    public class subCategoriesController : Controller
    {
        private projet_DataBaseEntities db = new projet_DataBaseEntities();

        // GET: subCategories
        public ActionResult Index()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var subCategories = db.subCategories.Include(s => s.secondaryCategory1);
            return View(subCategories.ToList());
        }

        // GET: subCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subCategory subCategory = db.subCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: subCategories/Create
        public ActionResult Create()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.secondaryCategory = new SelectList(db.secondaryCategories, "id", "label");
            return View();
        }

        // POST: subCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,label,secondaryCategory,isActive,createdAT")] subCategory subCategory)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.subCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.secondaryCategory = new SelectList(db.secondaryCategories, "id", "label", subCategory.secondaryCategory);
            return View(subCategory);
        }

        // GET: subCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subCategory subCategory = db.subCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.secondaryCategory = new SelectList(db.secondaryCategories, "id", "label", subCategory.secondaryCategory);
            return View(subCategory);
        }

        // POST: subCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,label,secondaryCategory,isActive,createdAT")] subCategory subCategory)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.secondaryCategory = new SelectList(db.secondaryCategories, "id", "label", subCategory.secondaryCategory);
            return View(subCategory);
        }

        // GET: subCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subCategory subCategory = db.subCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: subCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            subCategory subCategory = db.subCategories.Find(id);
            db.subCategories.Remove(subCategory);
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
