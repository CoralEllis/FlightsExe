using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tar1.Models;
namespace Tar1.Controllers
{
    public class UsermanagersController : ApiController
    {
        // GET api/<controller>
        public List <UserManager> Get()
        {
            UserManager u = new UserManager();
            return u.check();
        }

        // GET api/<controller>/5
        public void Get(int id)
        {

           
        }

        // POST api/<controller>
        public void Post([FromBody]UserManager manager)
        {
            manager.post();
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            UserManager u = new UserManager();
            u.Delete(id);

        }
    }
}