using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceSite.Models;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace EcommerceSite.Controllers
{
    public class HomeController : Controller
    {
        private projet_DataBaseEntities db = new projet_DataBaseEntities();
        public ActionResult Index()
        {
            return View(db.products.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Cart()
        {
            if (Session["email"] == null)
                return RedirectToAction("Index");
            int id = db.users.Find(int.Parse(Session["id"].ToString())).id;

            return View(db.carts.Where(u=>u.user == id).ToList());
        }

        [HttpPost]
        public ActionResult AddToCart(int? id)
        {
            if (Session["email"] != null)
            {
                if (db.products.Find(id) != null)
                {
                    var prod = db.products.Where(u => u.id == id).ToList();
                    cart cart = new cart();
                    cart.product = prod.Select(u => u.id).FirstOrDefault();
                    cart.user = int.Parse(Session["id"].ToString());
                    cart.Qte = 1;

                    db.carts.Add(cart);
                    db.SaveChanges();
                    return Redirect("/products/Details/" + id);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            else
            {
                return Redirect("/users/Login");
            }
        }



        public ActionResult Products(int? id)
        {
            Session["Category"] = db.subCategories.Where(u => u.id == id).Select(u => u.label).FirstOrDefault();

            return View(db.products.Where(u => u.subCategory == id && u.isActive == true).ToList());
        }



        public ActionResult Payment()
        {
            if (Session["id"] == null)
                return View();
            int id = db.users.Find(int.Parse(Session["id"].ToString())).id;
            return View(db.carts.Where(u=> u.user == id).ToList());
        }

        [HttpPost]
        public ActionResult Payment(payment payment)
        {
            return View();
        }

    }
}