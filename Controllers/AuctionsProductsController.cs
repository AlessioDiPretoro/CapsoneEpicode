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
    public class AuctionsProductsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: AuctionsProducts
        public ActionResult Index()
        {
            var auctionsProducts = db.AuctionsProducts.Include(a => a.Product).Include(a => a.Users);
            return View(auctionsProducts.ToList());
        }

        // GET: AuctionsProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionsProducts auctionsProducts = db.AuctionsProducts.Find(id);
            if (auctionsProducts == null)
            {
                return HttpNotFound();
            }
            return View(auctionsProducts);
        }

        // GET: AuctionsProducts/Create
        public ActionResult Create()
        {
            ViewBag.idProduct = new SelectList(db.Product, "id", "name");
            return View();
        }

        // POST: AuctionsProducts/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding.
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idProduct,dataStart,dataEnd,startPrice,isActive")] AuctionsProducts auctionsProducts)
        {
            if (ModelState.IsValid)
            {
                auctionsProducts.isActive = true;
                db.AuctionsProducts.Add(auctionsProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProduct = new SelectList(db.Product, "id", "name", auctionsProducts.idProduct);
            return View(auctionsProducts);
        }

        // GET: AuctionsProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionsProducts auctionsProducts = db.AuctionsProducts.Find(id);
            if (auctionsProducts == null)
            {
                return HttpNotFound();
            }
            return View(auctionsProducts);
        }

        // POST: AuctionsProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idProduct,dataStart,dataEnd,startPrice,isActive")] AuctionsProducts auctionsProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auctionsProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(auctionsProducts);
        }

        // GET: AuctionsProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionsProducts auctionsProducts = db.AuctionsProducts.Find(id);
            if (auctionsProducts == null)
            {
                return HttpNotFound();
            }
            return View(auctionsProducts);
        }

        // POST: AuctionsProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuctionsProducts auctionsProducts = db.AuctionsProducts.Find(id);
            db.AuctionsProducts.Remove(auctionsProducts);
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

        public ActionResult Auctions(FormCollection categories, FormCollection subjects)
        {
            List<string> selCat = categories.GetValues("category")?.ToList();
            List<string> selSub = subjects.GetValues("subject")?.ToList();
            List<AuctionsProducts> product = db.AuctionsProducts.Where(x => x.isActive == true).Include(p => p.Product.ProductCategory).ToList();

            if (selCat != null && selCat.Count > 0)
            {
                product = product.Where(x => selCat.Contains(x.Product.idCategory.ToString())).ToList();
            }
            if (selSub != null && selSub.Count > 0)
            {
                product = product.Where(x => selSub.Contains(x.Product.idSubject.ToString())).ToList();
            }

            if (product.Count == 0)
            {
                ViewBag.NoResults = "Spiacente ma non esistono prodotti con le caratteristiche ricercate";
            }

            ViewBag.idCategory = new SelectList(db.ProductCategory, "id", "name");
            ViewBag.idSubject = new SelectList(db.ProductSubject, "id", "name");
            return View(product);
        }
    }
}