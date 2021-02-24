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
    public class product_imagesController : Controller
    {
        private projet_DataBaseEntities db = new projet_DataBaseEntities();

        // GET: product_images
        public ActionResult Index()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product_images = db.product_images.Include(p => p.product1);
            return View(product_images.ToList());
        }

        // GET: product_images/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_images product_images = db.product_images.Find(id);
            if (product_images == null)
            {
                return HttpNotFound();
            }
            return View(product_images);
        }

        // GET: product_images/Create
        public ActionResult Create()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.product = new SelectList(db.products, "id", "label");
            return View();
        }

        // POST: product_images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,smallSize,mediumSize,largeSize,product")] product_images product_images)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.product_images.Add(product_images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product = new SelectList(db.products, "id", "label", product_images.product);
            return View(product_images);
        }

        // GET: product_images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_images product_images = db.product_images.Find(id);
            if (product_images == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = new SelectList(db.products, "id", "label", product_images.product);
            return View(product_images);
        }

        // POST: product_images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,smallSize,mediumSize,largeSize,product")] product_images product_images)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Entry(product_images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product = new SelectList(db.products, "id", "label", product_images.product);
            return View(product_images);
        }

        // GET: product_images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_images product_images = db.product_images.Find(id);
            if (product_images == null)
            {
                return HttpNotFound();
            }
            return View(product_images);
        }

        // POST: product_images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            product_images product_images = db.product_images.Find(id);
            db.product_images.Remove(product_images);
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
