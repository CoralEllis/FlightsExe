using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace Tar1.Models
{
    public class UserManager
    {
        string username;
        string password;
      

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }



        public UserManager(string _username, string _password)
        {
            Username = _username;
            Password = _password;
           
        }

        public UserManager() { }
        public List<UserManager> check()
        {
            DBservices dbs = new DBservices();

            return dbs.check();
                  
        }

        public void post()
        {
            DBservices dbs = new DBservices();

             dbs.postman(this);

        }
        public void Delete(string email)
        {
            DBservices dbs = new DBservices();

            dbs.DeleteMan(email);

        }

    }
}