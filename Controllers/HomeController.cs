using Stones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stones.Controllers
{
    public class HomeController : Controller
    {
        private readonly _Mailer _mailer;

        public HomeController()
        {
            _mailer = new _Mailer(); // Inizializzazione di Mailer
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //link prova per inviare mail XD
        public ActionResult SendEmail()
        {
            _mailer.SendEmail("alessiodipretoro@hotmail.it", "prova", "daje co sta mail");
            return View("Index");
        }
    }
}