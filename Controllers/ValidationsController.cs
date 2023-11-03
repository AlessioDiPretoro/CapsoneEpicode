using Stones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stones.Controllers
{
    public class ValidationsController : Controller
    {
        //disabilitati lato model, danno problemi in caso di modifica dei dati...
        //aggiunte verifiche "manuali" durante la creazione dell'user
        private static ModelDbContext db = new ModelDbContext();

        public ActionResult IsUsernameValid(string username)
        {
            bool isValid = db.Users.All(x => x.username != username);

            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsEmailValid(string email)
        {
            bool isValid = db.Users.All(x => x.email != email);

            return Json(isValid, JsonRequestBehavior.AllowGet);
        }
    }
}