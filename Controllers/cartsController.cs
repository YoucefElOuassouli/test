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
    public class cartsController : Controller
    {
        private projet_DataBaseEntities db = new projet_DataBaseEntities();

        // GET: carts
        public ActionResult Index()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var carts = db.carts.Include(c => c.product1).Include(c => c.user1);
            return View(carts.ToList());
        }

        // GET: carts/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: carts/Create
        public ActionResult Create()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.product = new SelectList(db.products, "id", "label");
            ViewBag.user = new SelectList(db.users, "id", "userName");
            return View();
        }

        // POST: carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product,user,Qte,createdAT")] cart cart)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product = new SelectList(db.products, "id", "label", cart.product);
            ViewBag.user = new SelectList(db.users, "id", "userName", cart.user);
            return View(cart);
        }

        // GET: carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = new SelectList(db.products, "id", "label", cart.product);
            ViewBag.user = new SelectList(db.users, "id", "userName", cart.user);
            return View(cart);
        }

        // POST: carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product,user,Qte,createdAT")] cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product = new SelectList(db.products, "id", "label", cart.product);
            ViewBag.user = new SelectList(db.users, "id", "userName", cart.user);
            return View(cart);
        }

        // GET: carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            cart cart = db.carts.Find(id);
            db.carts.Remove(cart);
            db.SaveChanges();
            return Redirect(Url.Action("Cart","Home"));
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
