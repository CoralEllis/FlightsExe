using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;
namespace Tar1.Models
{
    public class Discount
    {
        string airline;
        string origin;
        string destination;
        string startdate;
        string enddate;
        int discount1;
        int id;
        public string Airline { get => airline; set => airline = value; }
        public string Origin { get => origin; set => origin = value; }
        public string Destination { get => destination; set => destination = value; }
        public string Startdate { get => startdate; set => startdate = value; }
        public string Enddate { get => enddate; set => enddate = value; }
        public int Discount1 { get => discount1; set => discount1 = value; }
        public int Id { get => id; set => id = value; }

        public Discount() { }
        public Discount(string _airline, string _origin, string _dest, string _start, string _end, int _discount, int _id)
        {
            Airline = _airline;
            Origin = _origin;
            Destination = _dest;
            Startdate = _start;
            Enddate = _end;
            Discount1 = _discount;

        }

        public void insert()
        {
            DBservices dbs = new DBservices();
            dbs.insert(this);

        }
        public List<Discount> check()
        {
            DBservices dbs = new DBservices();
           return dbs.checkdisc();
        }

        public void delete(int id)
        {
            DBservices dbs = new DBservices();
             dbs.deletedisc(id);
        }
        public void update(int id)
        {
            DBservices dbs = new DBservices();
            dbs.updatedisc(id, this);

        }
    }
}