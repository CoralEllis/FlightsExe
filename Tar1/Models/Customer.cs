using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;
namespace Tar1.Models
{
    public class Customer
    {
        string email;
        string fullnames;
        int agree;

        public string Email { get => email; set => email = value; }
        public string Fullnames { get => fullnames; set => fullnames = value; }
        public int Agree { get => agree; set => agree = value; }

        public Customer() { }
        public Customer(string _email, string _fullname, int _agree) {

            Email = _email;
            Fullnames = _fullname;
            Agree = _agree;
        
        }
        public int insert()
        {
            DBservices dbs = new DBservices();
           return dbs.insertcustomer(this);
        }



    }
}