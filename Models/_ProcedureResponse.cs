using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
	//response to Custom procedure
	public class _ProcedureResponse
	{
		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
		public bool IsSuccess { get; set; }
	}
}