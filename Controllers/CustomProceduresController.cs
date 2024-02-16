using Stones.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stones.Controllers
{
	//procedures used in multiple areas of the code
	public class CustomProceduresController : Controller
	{
		//list of supported file formats for uploading product and profile photos
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

		//photo loading function, with format verification and different saving folder
		public static _ProcedureResponse SavePhoto(HttpPostedFileBase f, string folder)
		{
			_ProcedureResponse response = new _ProcedureResponse();
			string fileName = f.FileName;
			string ext = Path.GetExtension(f.FileName);
			fileName = fileName.Replace(ext, "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace(".", "").Replace("-", "") + DateTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(" ", "").Replace(":", "") + ext;

			if (AcceptedFile.Contains(ext))
			{
				string pathToSave = Path.Combine(folder, fileName);

				if (!Directory.Exists(pathToSave))
				{
					Directory.CreateDirectory(pathToSave);
				}

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