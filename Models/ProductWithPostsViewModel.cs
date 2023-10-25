using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
    public class ProductWithPostsViewModel
    {
        public Product Product { get; set; }
        public List<Post> Posts { get; set; }

        //public List<Post> Response { get; set; }
        public Dictionary<int, List<Post>> Responses { get; set; }
    }
}