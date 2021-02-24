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
    public class usersController : Controller
    {
        Login Login = new Login();
        private projet_DataBaseEntities db = new projet_DataBaseEntities();
        
        // GET: users
        public ActionResult Index()
        {
            
            return View(db.users.ToList());
        }




        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (id != int.Parse(Session["id"].ToString())) return RedirectToAction("Details/" + Session["id"]);
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            if(Session["email"] != null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userName,phoneNumber,email,password,address,isAdmin,createdAT,isActive")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login","users");
            }

            return View(user);
        }

        [Route("/Login")]
        // GET: users/LogIn
        public ActionResult login()
        {
            if (Session["email"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: users/login
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(string email, string password)
        {
            var check = db.users.Where(u => u.email == email).Select(u => u.id).FirstOrDefault();
                if(db.users.Find(check) != null)
                {
                    ViewBag.userID = check;
                    Session["id"] = check;
                    Session["email"] = email;
                    return RedirectToAction("Details/"+check);
                }
            return View();
        }


        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (id != int.Parse(Session["id"].ToString())) return RedirectToAction("Details/"+ Session["id"]);
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userName,phoneNumber,email,password,address,isAdmin,createdAT,isActive")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(user);
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult logout() 
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
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
