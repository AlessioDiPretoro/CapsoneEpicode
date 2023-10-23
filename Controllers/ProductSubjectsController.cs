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
    public class ProductSubjectsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: ProductSubjects
        public ActionResult Index()
        {
            return View(db.ProductSubject.ToList());
        }

        // GET: ProductSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubject productSubject = db.ProductSubject.Find(id);
            if (productSubject == null)
            {
                return HttpNotFound();
            }
            return View(productSubject);
        }

        // GET: ProductSubjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductSubjects/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description")] ProductSubject productSubject)
        {
            if (ModelState.IsValid)
            {
                db.ProductSubject.Add(productSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productSubject);
        }

        // GET: ProductSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubject productSubject = db.ProductSubject.Find(id);
            if (productSubject == null)
            {
                return HttpNotFound();
            }
            return View(productSubject);
        }

        // POST: ProductSubjects/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description")] ProductSubject productSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productSubject);
        }

        // GET: ProductSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubject productSubject = db.ProductSubject.Find(id);
            if (productSubject == null)
            {
                return HttpNotFound();
            }
            return View(productSubject);
        }

        // POST: ProductSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSubject productSubject = db.ProductSubject.Find(id);
            db.ProductSubject.Remove(productSubject);
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
