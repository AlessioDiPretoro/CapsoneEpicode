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
    public class PostsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Posts
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.Users);
            return View(post.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Posts/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idUser,idProduct,idUserResponse,isActive,body,date,dateEdit")] Post post)
        {
            // string route = (RouteData.Values["id"]).ToString();
            if (ModelState.IsValid)
            {
                post.idProduct = Convert.ToInt16(RouteData.Values["id"]);
                post.idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
                post.isActive = true;
                post.date = DateTime.Now;
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction($"Details/{post.idProduct}", "Products");
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult _Create()
        {
            return View("_Create");
        }

        // POST: Posts/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "id,idUser,idProduct,idUserResponse,isActive,body,date,dateEdit")] Post post)
        {
            // string route = (RouteData.Values["id"]).ToString();
            if (ModelState.IsValid)
            {
                post.idProduct = Convert.ToInt16(RouteData.Values["id"]);
                post.idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
                post.isActive = true;
                post.date = DateTime.Now;
                db.Post.Add(post);
                db.SaveChanges();
                //return PartialView();
                return RedirectToAction($"Details/{post.idProduct}", "Products");
                //RedirectToAction($"Details/{post.idProduct}", "Products");
                //return RedirectToAction($"Details/{post.idProduct}", "Products");
            }

            return PartialView(post);
        }

        //crea post da chiamata ajax in jquery dalla text area nella pagina dettaglio prodotti
        [HttpPost]
        public JsonResult CreatePost(Post post)
        {
            post.idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            post.isActive = true;
            post.date = DateTime.Now;
            db.Post.Add(post);
            db.SaveChanges();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            //verifica la proprietà dell'user sul post da editare (evita che tramite modifica al link si possano editare altri post)
            int idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            if (post.idUser != idUser)
            {
                return RedirectToAction("Index", "Home");
            }
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idUser,idProduct,isActive,body,date,dateEdit")] Post post)
        //idUserResponse,
        {
            if (ModelState.IsValid)
            {
                post.dateEdit = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5 // NON LO USO, impostata variabile isActive per nascondere i commenti
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            //verifica la proprietà dell'user sul post da editare (evita che tramite modifica al link si possano eliminare altri post)
            int idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            if (post.idUser != idUser)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            //verifica la proprietà dell'user sul post da editare (evita che tramite modifica al link si possano eliminare altri post)
            int idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            if (post.idUser != idUser)
            {
                return RedirectToAction("Index", "Home");
            }
            db.Post.Remove(post);
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