using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace Tar1.Models
{
    public class Order
    {
        string id;
        string origin;
        string destination;
        string startDate;
        string endDate;
        double price;
        string route;
        string airlinecompany;
        string email;

        public string Origin { get => origin; set => origin = value; }
        public string Destination { get => destination; set => destination = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public double Price { get => price; set => price = value; }
        public string Route { get => route; set => route = value; }
        public string Airlinecompany { get => airlinecompany; set => airlinecompany = value; }
        public string Email { get => email; set => email = value; }
        public string Id { get => id; set => id = value; }

        public Order(string _id, string _origin, string _destination, string _startDate, string _endDate, float _price, string _route, string _airlinecompany, string _email)
        {
            Id = _id;
            Origin = _origin;
            Destination = _destination;
            StartDate = _startDate;
            EndDate = _endDate;
            Price = _price;
            Route = _route;
            Airlinecompany = _airlinecompany;
            Email = _email;
        }

        public Order() { }

        public void insert()
        {
            DBservices dbs = new DBservices();
            dbs.insertOrder(this);
        }

        public List<Order> getid()
        {
            DBservices dbs = new DBservices();
            return dbs.getid();

        }

        public List<Order> getorder()
        {
            DBservices dbs = new DBservices();
            return dbs.getorder();
        }

    }
}