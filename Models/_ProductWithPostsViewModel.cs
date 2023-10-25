using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
    public class _ProductWithPostsViewModel
    {
        public Product Product { get; set; }
        public List<Post> Posts { get; set; }

        //public List<PostResponse> Responses { get; set; }
        public Dictionary<int, List<PostResponse>> Responses { get; set; }
    }
}