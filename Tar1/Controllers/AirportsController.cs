using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Tar1.Models;

namespace Tar1.Controllers
{
    public class AirportsController : ApiController
    {
        // GET api/<controller>
        public List<Airport> Get()
        {
            Airport a1 = new Airport();
        
            return a1.getairports1();
        }




        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody] Airport[] airport)
        {
           // return airport.insert();
             Airport airpo = new Airport();
            return airpo.insert(airport);

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