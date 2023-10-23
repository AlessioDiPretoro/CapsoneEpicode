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
    public class PostsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.PostVisibility).Include(p => p.Users);
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
        public ActionResult Create()
        {
            ViewBag.idVisibility = new SelectList(db.PostVisibility, "id", "id");
            ViewBag.idUser = new SelectList(db.Users, "id", "username");
            return View();
        }

        // POST: Posts/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding.
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idUser,idVisibility,idUserResponse,isActive,title,body,date,dateEdit")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
                post.isActive = true;
                post.date = DateTime.Now;
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idVisibility = new SelectList(db.PostVisibility, "id", "id", post.idVisibility);
            ViewBag.idUser = new SelectList(db.Users, "id", "username", post.idUser);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.idVisibility = new SelectList(db.PostVisibility, "id", "id", post.idVisibility);
            ViewBag.idUser = new SelectList(db.Users, "id", "username", post.idUser);
            return View(post);
        }

        // POST: Posts/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding.
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idUser,idVisibility,idUserResponse,isActive,title,body,date,dateEdit")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idVisibility = new SelectList(db.PostVisibility, "id", "id", post.idVisibility);
            ViewBag.idUser = new SelectList(db.Users, "id", "username", post.idUser);
            return View(post);
        }

        // GET: Posts/Delete/5
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
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
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