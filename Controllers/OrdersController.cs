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
    public class OrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Orders
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
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            //ViewBag.idBuyer = new SelectList(db.Users, "id", "username");
            return View();
        }

        // POST: Orders/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding.
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            order.idBuyer = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            order.date = DateTime.Now;
            order.state = "In preparazione";

            if (ModelState.IsValid)
            {
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

            //ViewBag.idBuyer = new SelectList(db.Users, "id", "username", order.idBuyer);
            return View(order);
        }

        // GET: Orders/Edit/5
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
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding.
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
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