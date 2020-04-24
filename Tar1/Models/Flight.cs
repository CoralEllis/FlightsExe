using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace Tar1.Models
{
    public class Flight
    {
        string idflight;
        string origin;
        string destination;
        string startDate;
        string endDate;
        double price;
        string route;
        string airlinecompany;
        int count;

     
        public string Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
            }
        }

        public string Destination
        {
            get
            {
                return destination;
            }

            set
            {
                destination = value;
            }
        }

        public string StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public string Route
        {
            get
            {
                return route;
            }

            set
            {
                route = value;
            }
        }

        public string Idflight { get => idflight; set => idflight = value; }
        public string Airlinecompany { get => airlinecompany; set => airlinecompany = value; }
        public int Count { get => count; set => count = value; }

        public Flight(string _id,string _origin, string _destination, string _startDate, string _endDate, float _price, string _route, string _airline,int _count)
        {
            Idflight = _id;
            Origin = _origin;
            Destination = _destination;
            StartDate = _startDate;
            EndDate = _endDate;
            Price = _price;
            Route = _route;
            Airlinecompany = _airline;
            Count = _count;
        }

        public Flight() { }

       
        public void insert()
        {
            DBservices dbs = new DBservices();

            dbs.insert(this);
            



        }
        public List<Flight> getflights()
            {
            DBservices dbs = new DBservices();
            return dbs.getflights();
                }

    }
}