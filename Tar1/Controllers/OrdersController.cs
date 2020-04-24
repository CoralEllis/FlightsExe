using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tar1.Models;

namespace Tar1.Controllers
{
    public class OrdersController : ApiController
    {
        // GET api/<controller>
        public List<Order> Get()
        {
            Order o = new Order();
            return o.getid();

        }

        // GET api/<controller>/5
        public void Get(string origin)
        {
           
        }

        // POST api/<controller>
        public void Post([FromBody] Order order)
        {
             order.insert();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}