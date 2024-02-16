using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
	public class _ProductWithPostsViewModel
	{
		//incorporates the 2 product and post models to be able to send them in the same view details in product
		public Product Product { get; set; }

		public List<Post> Posts { get; set; }

		public Dictionary<int, List<PostResponse>> Responses { get; set; }
	}
}