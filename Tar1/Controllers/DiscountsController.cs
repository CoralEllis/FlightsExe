using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tar1.Models;

namespace Tar1.Controllers
{
    public class DiscountsController : ApiController
    {
        // GET api/<controller>
        public List<Discount> Get()
        {
            Discount discount = new Discount();
            return discount.check();
        }

        // GET api/<controller>/5
        public void Get(string origin)
        {
           
        }

        // POST api/<controller>
        public void Post([FromBody] Discount discount)

        {
            discount.insert();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Discount discount)
        {
            discount.update(id);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            Discount discount = new Discount();
            discount.delete(id);
        }
    }
}