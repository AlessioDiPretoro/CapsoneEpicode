using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Stones.Models;

namespace Stones.Controllers
{
    public class UsersController : Controller
    {
        private string pattern = "[@\\-/]";

        private List<string> notAcceptedString
        {
            get
            {
                List<string> strings = new List<string>
                {
                    "@",
                    "/"
                };

                return strings;
            }
        }

        private ModelDbContext db = new ModelDbContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                id = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,email,password,confirmPassword,name,surname,address,cap,city,prov,phone,imgProfile")] Users users, HttpPostedFileBase imgProfile)
        {
            if (ModelState.IsValid)
            {
                bool isUserValid = db.Users.All(x => x.username != users.username);
                if (!isUserValid)
                {
                    ViewBag.ErrorMessage = "Username già presente, sceglierne uno differente.";
                    return View(users);
                }
                bool isEmailValid = db.Users.All(x => x.email != users.email);
                if (!isEmailValid)
                {
                    ViewBag.ErrorMessage = "Email già presente, controllare o usarne una differente.";
                    return View(users);
                }
                Match match = Regex.Match(users.username, pattern);
                if (match.Success)
                {
                    ViewBag.ErrorMessage = "Username non corretto, contiene caratteri speciali.";
                    return View(users);
                }

                string folder = Server.MapPath("~/Content/imgProfiles");
                if (imgProfile != null)
                {
                    string nomeFile = imgProfile.FileName;
                    users.imgProfile = nomeFile;
                    _ProcedureResponse resp = CustomProceduresController.SavePhoto(imgProfile, folder);
                    if (resp.ErrorMessage != null)
                    {
                        ViewBag.ErrorMessage = resp.ErrorMessage;
                        return View(users);
                    }
                    else
                    {
                        users.imgProfile = resp.SuccessMessage;
                    }
                }
                else
                {
                    users.imgProfile = "placeholder.png";
                }
                users.role = "User";
                db.Users.Add(users);
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(users.username, true);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(users);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                id = db.Users.Where(x => x.username == User.Identity.Name).FirstOrDefault().id;
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            TempData["profileImg"] = users.imgProfile;
            return View(users);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users users, HttpPostedFileBase imgProfile)
        {
            if (ModelState.IsValid)
            {
                bool verified = false;
                if (users.username.Contains("@"))
                {
                verified = db.Users.Any(x => x.email == users.username && x.password == users.password);
                }
                else
                {
                    verified = db.Users.Any(x => x.username == users.username && x.password == users.password);
                }

                if (verified)
                {
                    string folder = Server.MapPath("~/Content/imgProfiles");
                    if (imgProfile != null)
                    {
                        string nomeFile = imgProfile.FileName;
                        users.imgProfile = nomeFile;
                        _ProcedureResponse resp = CustomProceduresController.SavePhoto(imgProfile, folder);
                        if (resp.ErrorMessage != null)
                        {
                            ViewBag.ErrorMessage = resp.ErrorMessage;
                            return View(users);
                        }
                        else
                        {
                            users.imgProfile = resp.SuccessMessage;
                        }
                    }
                    else
                    {
                        users.imgProfile = TempData["profileImg"].ToString();
                    }

                    db.Entry(users).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
                else
                {
                    ViewBag.ErrorMessage = "Password errata";
                    return View(users);
                }   
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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