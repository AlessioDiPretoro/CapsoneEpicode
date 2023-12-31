﻿using Stones.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stones.Controllers
{
    //procedure che uso in più zone del codice
    public class CustomProceduresController : Controller
    {
        //lista formati di file supportati per il caricamento delle foto prodotto e profilo
        public static List<string> AcceptedFile
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

        //funzione di caricamento foto, con verifica del formato e diversa cartella di salvataggio
        public static _ProcedureResponse SavePhoto(HttpPostedFileBase f, string folder)
        {
            _ProcedureResponse response = new _ProcedureResponse();
            string fileName = f.FileName;
            string ext = Path.GetExtension(f.FileName);
            fileName = fileName.Replace(ext, "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("-", "") + DateTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(" ", "").Replace(":", "") + ext;

            if (AcceptedFile.Contains(ext))
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