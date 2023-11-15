using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
    public class _ProductWithPostsViewModel
    {
        //ingloba i 2 modelli prodotto e post per poterli inviare nella stessa view details in product
        public Product Product { get; set; }

        public List<Post> Posts { get; set; }

        //public List<PostResponse> Responses { get; set; }
        public Dictionary<int, List<PostResponse>> Responses { get; set; }
    }
}