using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stones.Models;

namespace Stones.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        private int IdUser
        {
            get
            {
                int id = 0;
                if (User.Identity.Name != null)
                {
                    string username = User.Identity.Name;
                    id = db.Users.Where(x => x.username == username).FirstOrDefault().id;
                }
                return id;
            }
        }

        // GET: Orders
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.Users);
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.idBuyer != IdUser && !User.IsInRole("SuperAdmin"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create(int? id)
        {
            if (id != null)//si attiva quando si accede tramite link mail vincita asta
            {
                AuctionsDetails a = db.AuctionsDetails.Find(id);
                if (a.idUser != IdUser)
                {

                return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order, int? id)
        {
            order.idBuyer = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            order.date = DateTime.Now;
            order.state = "In preparazione";

            if (ModelState.IsValid)
            {
                if (id != null)//si attiva quando si accede tramite link mail vincita asta
                {
                    db.Order.Add(order);
                    AuctionsDetails a = db.AuctionsDetails.Find(id);
                    Product p = db.Product.Find(a.AuctionsProducts.idProduct);
                    order.DetailOrder.Add(new DetailOrder
                    {
                        idProduct = p.id,
                        name = p.name,
                        quanty = 1,
                        priceCad = a.price,
                        state = "in elaborazione"
                    });
                    p.isAvaiable = false;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                List<_Cart> cart = (List<_Cart>)Session["cart"];

                if (cart.Count() == 0)
                {
                    TempData["EmptyCart"] = "Il carrello è vuoto, inserisci prima qualche articolo";
                    return View();
                }
                else
                {
                    db.Order.Add(order);
                    foreach (_Cart cartItem in cart)
                    {
                        Product p = db.Product.Find(cartItem.idProduct);
                        order.DetailOrder.Add(new DetailOrder
                        {
                            idProduct = p.id,
                            name = p.name,
                            quanty = 1,
                            priceCad = p.price,
                            state = "in elaborazione"
                        });
                        p.isAvaiable = false;
                        db.Entry(p).State = EntityState.Modified;
                    }
                    Session.Remove("cart");
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBuyer = new SelectList(db.Users, "id", "username", order.idBuyer);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idBuyer,address,city,prov,cap,phone,state,date,notes")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBuyer = new SelectList(db.Users, "id", "username", order.idBuyer);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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

        public ActionResult UserOrders()
        {
            int idBuyer = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            List<Order> order = db.Order.Where(o => o.idBuyer == idBuyer).ToList();
            return View(order);
        }
    }
}