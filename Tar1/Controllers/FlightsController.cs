using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tar1.Models;

namespace Tar1.Controllers
{
    public class FlightsController : ApiController
    {
        // GET api/<controller>
        public List<Flight> Get()
        {
            Flight flight = new Flight();
            return flight.getflights();
          
        }

        // GET api/<controller>/5
        public IEnumerable<Flight> Get(string connection)
        {
            return null;
        }

        // POST api/<controller>
        public void Post([FromBody] Flight flight)
        {
          
             flight.insert();
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