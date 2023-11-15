using Stones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Stones.Controllers
{
    public class ValuesController : ApiController
    {
        private ModelDbContext db = new ModelDbContext();
        //crea ordine tramite API

        //[HttpPost]
        //[Route("api/auctionWin/{i}")]
        //public ActionResult AddOrderWinner(int i)
        //{
        //    return RedirectToAction("Create", "Orders", new { order = order });

        //}

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}