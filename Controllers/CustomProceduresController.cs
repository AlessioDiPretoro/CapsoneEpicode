using Stones.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stones.Controllers
{
    public class CustomProceduresController : Controller
    {
        public static List<string> acceptedFile
        {
            get
            {
                List<string> strings = new List<string>
                {
                    ".jpeg",
                    ".jpg",
                    ".png"
                };

                return strings;
            }
        }

        public static _ProcedureResponse SavePhoto(HttpPostedFileBase f, string folder)
        {
            _ProcedureResponse response = new _ProcedureResponse();
            string fileName = f.FileName;
            string ext = Path.GetExtension(f.FileName);
            fileName = fileName.Replace(ext, "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("-", "") + DateTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(" ", "").Replace(":", "") + ext;

            if (acceptedFile.Contains(ext))
            {
                string pathToSave = Path.Combine(folder, fileName);
                f.SaveAs(pathToSave);
                response.SuccessMessage = fileName;
            }
            else
            {
                response.ErrorMessage = "File immagine non supportato";
            }
            return response;
        }
    }
}