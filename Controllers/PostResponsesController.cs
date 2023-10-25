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
    public class PostResponsesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: PostResponses
        public ActionResult Index()
        {
            var postResponse = db.PostResponse.Include(p => p.Post);
            return View(postResponse.ToList());
        }

        // GET: PostResponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostResponse postResponse = db.PostResponse.Find(id);
            if (postResponse == null)
            {
                return HttpNotFound();
            }
            return View(postResponse);
        }

        // GET: PostResponses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostResponses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idPost,body,idUser,Username,date,dateEdit,isActive")] PostResponse postResponse)
        {
            if (ModelState.IsValid)
            {
                postResponse.idPost = Convert.ToInt16(RouteData.Values["id"]);
                Users poster = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault();
                postResponse.idUser = poster.id;
                postResponse.Username = poster.username;
                postResponse.isActive = true;
                postResponse.date = DateTime.Now;
                db.PostResponse.Add(postResponse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postResponse);
        }

        // GET: PostResponses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostResponse postResponse = db.PostResponse.Find(id);
            int idUser = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            if (postResponse.idUser != idUser)
            {
                return RedirectToAction("Index", "Home");
            }
            if (postResponse == null)
            {
                return HttpNotFound();
            }
            return View(postResponse);
        }

        // POST: PostResponses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostResponse postResponse)
        {
            if (ModelState.IsValid)
            {
                postResponse.dateEdit = DateTime.Now;
                db.Entry(postResponse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postResponse);
        }

        // GET: PostResponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostResponse postResponse = db.PostResponse.Find(id);
            if (postResponse == null)
            {
                return HttpNotFound();
            }
            return View(postResponse);
        }

        // POST: PostResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostResponse postResponse = db.PostResponse.Find(id);
            db.PostResponse.Remove(postResponse);
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