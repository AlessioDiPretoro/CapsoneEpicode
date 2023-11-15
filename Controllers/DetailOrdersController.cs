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
    public class DetailOrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: DetailOrders
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Index()
        {
            var detailOrder = db.DetailOrder.Include(d => d.Order).Include(d => d.Product);
            return View(detailOrder.ToList());
        }

        // GET: DetailOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = db.DetailOrder.Find(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // GET: DetailOrders/Create
        public ActionResult Create()
        {
            ViewBag.idOrder = new SelectList(db.Order, "id", "address");
            ViewBag.idProduct = new SelectList(db.Product, "id", "name");
            return View();
        }

        // POST: DetailOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idOrder,idProduct,name,quanty,priceCad,state")] DetailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.DetailOrder.Add(detailOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idOrder = new SelectList(db.Order, "id", "address", detailOrder.idOrder);
            ViewBag.idProduct = new SelectList(db.Product, "id", "name", detailOrder.idProduct);
            return View(detailOrder);
        }

        // GET: DetailOrders/Edit/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = db.DetailOrder.Find(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOrder = new SelectList(db.Order, "id", "address", detailOrder.idOrder);
            ViewBag.idProduct = new SelectList(db.Product, "id", "name", detailOrder.idProduct);
            return View(detailOrder);
        }

        // POST: DetailOrders/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idOrder,idProduct,name,quanty,priceCad,state")] DetailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOrder = new SelectList(db.Order, "id", "address", detailOrder.idOrder);
            ViewBag.idProduct = new SelectList(db.Product, "id", "name", detailOrder.idProduct);
            return View(detailOrder);
        }

        // GET: DetailOrders/Delete/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = db.DetailOrder.Find(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // POST: DetailOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailOrder detailOrder = db.DetailOrder.Find(id);
            db.DetailOrder.Remove(detailOrder);
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