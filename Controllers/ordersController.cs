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
    public class ordersController : Controller
    {
        private projet_DataBaseEntities db = new projet_DataBaseEntities();

        // GET: orders
        public ActionResult Index()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            int id = db.users.Find(Session["id"]).id;
            var orders = db.orders.Where(u => u.user.id == id).ToList();
            return View(orders);
        }

        // GET: orders/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.customer = new SelectList(db.users, "id", "userName");
            ViewBag.payment = new SelectList(db.payments, "id", "paymentType");
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int customer,int payment,string orderDetails)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                order order = new order();
                order.customer = customer;
                order.payment = payment;
                order.orderDetails = orderDetails;
                db.orders.Add(order);
                db.SaveChanges();
                Session["Message_Succed"] = "Your Payment has been done Successfuly";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Cart","Home");
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer = new SelectList(db.users, "id", "userName", order.customer);
            ViewBag.payment = new SelectList(db.payments, "id", "paymentType", order.payment);
            return View(order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customer,payment,orderDetails,createdAT,isActive")] order order)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer = new SelectList(db.users, "id", "userName", order.customer);
            ViewBag.payment = new SelectList(db.payments, "id", "paymentType", order.payment);
            return View(order);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["email"] == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
