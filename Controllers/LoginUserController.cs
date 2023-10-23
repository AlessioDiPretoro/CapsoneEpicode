using Stones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Stones.Controllers
{
    public class LoginUserController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: LoginUser
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(_LoginUser u)
        {
            if (ModelState.IsValid)
            {
                Users user = null;
                if (u.UserName.Contains("@"))
                {
                    user = db.Users.FirstOrDefault(x => x.email == u.UserName && x.password == u.Password);
                    u.UserName = user.username;
                }
                else
                {
                    user = db.Users.FirstOrDefault(x => x.username == u.UserName && x.password == u.Password);
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(u.UserName, true);
                    //cookie per id user? troppo facile modificarlo
                    //HttpCookie idCookie = new HttpCookie(user.id.ToString());
                    //Response.Cookies.Add(idCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LogError = "Utente non trovato, verificare i dati di accesso";
                    return View("Login", u);
                }
            }

            return View("Login", u);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}