using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stones.Models
{
    public class _Cart
    {
        public List<_Cart> cartList = new List<_Cart>();

        public int idProduct { get; set; }
    }
}