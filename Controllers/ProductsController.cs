using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stones.Models;

namespace Stones.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ProductsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.ProductCategory);
            return View(product.ToList());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ordina in maniera decrescente i post
            List<Post> posts = db.Post
         .Where(p => p.idProduct == id && p.isActive)
         .OrderByDescending(p => p.date)
         .ToList();

            Dictionary<int, List<PostResponse>> responses = new Dictionary<int, List<PostResponse>>();

            foreach (Post post in posts)
            {
                List<PostResponse> responsePosts = db.PostResponse
                             .Where(p => p.idPost == post.id && p.isActive)
                             .OrderByDescending(p => p.date)
                             .ToList();

                responses.Add(post.id, responsePosts);
            }

            // Crea un modello che includa sia il prodotto che i post e le risposte
            var productWithPosts = new _ProductWithPostsViewModel
            {
                Product = product,
                Posts = posts,
                Responses = responses
            };

            return View(productWithPosts);
            //return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.idCategory = new SelectList(db.ProductCategory.OrderBy(x => x.name), "id", "name");
            ViewBag.idSubject = new SelectList(db.ProductSubject.OrderBy(x => x.name), "id", "name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, HttpPostedFileBase photo5)
        {
            if (ModelState.IsValid)
            {
                string folder = Server.MapPath("~/Content/imgProducts");
                if (photo1 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo1, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo1 = resp.SuccessMessage; }
                }
                if (photo2 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo2, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo2 = resp.SuccessMessage; }
                }
                if (photo3 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo3, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo3 = resp.SuccessMessage; }
                }
                if (photo4 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo4, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo4 = resp.SuccessMessage; }
                }
                if (photo5 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo5, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo5 = resp.SuccessMessage; }
                }
                product.isAvaiable = true;
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategory = new SelectList(db.ProductCategory.OrderBy(x => x.name), "id", "name");
            ViewBag.idSubject = new SelectList(db.ProductSubject.OrderBy(x => x.name), "id", "name");

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatList = new SelectList(db.ProductCategory.OrderBy(x => x.name), "id", "name", product.idCategory);
            ViewBag.SubList = new SelectList(db.ProductSubject.OrderBy(x => x.name), "id", "name", product.idSubject);
            _Photos _photos = new _Photos(product.photo1, product.photo2, product.photo3, product.photo4, product.photo5);
            TempData["Images"] = _photos;
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, HttpPostedFileBase photo5)
        {
            if (ModelState.IsValid)
            {
                //aggiungere se non c'è nulla allora vecchio valore da TempData
                string folder = Server.MapPath("~/Content/imgProducts");
                if (photo1 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo1, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo1 = resp.SuccessMessage; }
                }
                else { _Photos _photos = TempData["Images"] as _Photos; product.photo1 = _photos.photo1; }
                if (photo2 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo2, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo2 = resp.SuccessMessage; }
                }
                else { _Photos _photos = TempData["Images"] as _Photos; product.photo2 = _photos.photo2; }

                if (photo3 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo3, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo3 = resp.SuccessMessage; }
                }
                else { _Photos _photos = TempData["Images"] as _Photos; product.photo3 = _photos.photo3; }

                if (photo4 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo4, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo4 = resp.SuccessMessage; }
                }
                else { _Photos _photos = TempData["Images"] as _Photos; product.photo4 = _photos.photo4; }

                if (photo5 != null)
                {
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(photo5, folder);
                    if (resp.ErrorMessage != null) { ViewBag.ErrorMessage = resp.ErrorMessage; }
                    else { product.photo5 = resp.SuccessMessage; }
                }
                else { _Photos _photos = TempData["Images"] as _Photos; product.photo5 = _photos.photo5; }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatList = new SelectList(db.ProductCategory, "id", "name", product.idCategory);
            ViewBag.SubList = new SelectList(db.ProductSubject, "id", "name", product.idSubject);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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

        //visualizza la galleria dei prodotti disponibili ed effettua eventuali filtri
        [AllowAnonymous]
        public ActionResult Gallery(FormCollection categories, FormCollection subjects)
        {
            List<string> selCat = categories.GetValues("category")?.ToList();
            List<string> selSub = subjects.GetValues("subject")?.ToList();
            List<Product> product = db.Product.Include(p => p.ProductCategory).ToList();

            if (selCat != null && selCat.Count > 0)
            {
                product = product.Where(x => selCat.Contains(x.idCategory.ToString())).ToList();
            }
            if (selSub != null && selSub.Count > 0)
            {
                product = product.Where(x => selSub.Contains(x.idSubject.ToString())).ToList();
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