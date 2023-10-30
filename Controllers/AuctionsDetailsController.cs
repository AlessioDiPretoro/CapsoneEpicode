﻿using System;
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
    public class AuctionsDetailsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: AuctionsDetails
        public ActionResult Index()
        {
            var auctionsDetails = db.AuctionsDetails.Include(a => a.AuctionsProducts).Include(a => a.Users);
            return View(auctionsDetails.ToList());
        }

        // GET: AuctionsDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionsDetails auctionsDetails = db.AuctionsDetails.Find(id);
            if (auctionsDetails == null)
            {
                return HttpNotFound();
            }
            return View(auctionsDetails);
        }

        // GET: AuctionsDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionsDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAuction,data,price")] AuctionsDetails auctionsDetails)
        //public ActionResult Create([Bind(Include = "id,idAuction,idUser,data,price")] AuctionsDetails auctionsDetails)
        {
            if (ModelState.IsValid)
            {
                auctionsDetails.idAuction = Convert.ToInt16(RouteData.Values["id"]);
                //decimal startPrice = db.AuctionsProducts.Where(x => x.id == auctionsDetails.idAuction).FirstOrDefault().startPrice;
                //decimal maxOffert = db.AuctionsDetails.Where(x => x.idAuction == auctionsDetails.idAuction).OrderByDescending(x => x.price).FirstOrDefault().price;
                if (auctionsDetails.price < db.AuctionsProducts
                    .Where(x => x.id == auctionsDetails.idAuction)
                    .FirstOrDefault().startPrice)
                {
                    ViewBag.Priceless = "La tua offerta è inferiore al prezzo di partenza, aumenta l'offerta";
                    return View(auctionsDetails);
                }
                if (auctionsDetails.price <= db.AuctionsDetails.Where(x => x.idAuction == auctionsDetails.idAuction)
                    .OrderByDescending(x => x.price)
                    .Select(x => x.price)
                    .FirstOrDefault())
                {
                    ViewBag.Priceless = "Esistono offerte superiori alla tua, aumenta l'offerta";
                    return View(auctionsDetails);
                }

                auctionsDetails.idUser = db.Users.Where(u => u.username == User.Identity.Name).FirstOrDefault().id;
                auctionsDetails.data = DateTime.Now;
                db.AuctionsDetails.Add(auctionsDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(auctionsDetails);
        }

        // GET: AuctionsDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionsDetails auctionsDetails = db.AuctionsDetails.Find(id);
            if (auctionsDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAuction = new SelectList(db.AuctionsProducts, "id", "id", auctionsDetails.idAuction);
            ViewBag.idUser = new SelectList(db.Users, "id", "username", auctionsDetails.idUser);
            return View(auctionsDetails);
        }

        // POST: AuctionsDetails/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding.
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idAuction,idUser,data,price")] AuctionsDetails auctionsDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auctionsDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAuction = new SelectList(db.AuctionsProducts, "id", "id", auctionsDetails.idAuction);
            ViewBag.idUser = new SelectList(db.Users, "id", "username", auctionsDetails.idUser);
            return View(auctionsDetails);
        }

        // GET: AuctionsDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuctionsDetails auctionsDetails = db.AuctionsDetails.Find(id);
            if (auctionsDetails == null)
            {
                return HttpNotFound();
            }
            return View(auctionsDetails);
        }

        // POST: AuctionsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuctionsDetails auctionsDetails = db.AuctionsDetails.Find(id);
            db.AuctionsDetails.Remove(auctionsDetails);
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

        public ActionResult UserAuctions()
        {
            var auctionsDetails = db.AuctionsDetails.Where(x => x.idUser == db.Users.Where(y => y.username == User.Identity.Name).FirstOrDefault().id);
            return View(auctionsDetails.ToList());
        }
    }
}