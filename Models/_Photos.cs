using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
	//used fot temp data of product picture
	public class _Photos
	{
		public string photo1 { get; set; }
		public string photo2 { get; set; }
		public string photo3 { get; set; }
		public string photo4 { get; set; }
		public string photo5 { get; set; }

		public _Photos(string photo1, string photo2, string photo3, string photo4, string photo5)
		{
			this.photo1 = photo1;
			this.photo2 = photo2;
			this.photo3 = photo3;
			this.photo4 = photo4;
			this.photo5 = photo5;
		}
	}
}