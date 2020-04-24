using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models.DAL;

namespace Tar1.Models
{
    public class Airport
    {
        string code;
        float lat;
        float lon;
        string name;

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public float Lat
        {
            get
            {
                return lat;
            }

            set
            {
                lat = value;
            }
        }

        public float Lon
        {
            get
            {
                return lon;
            }

            set
            {
                lon = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public static List<Airport> AirportList
        {
            get
            {
                return airportList;
            }

            set
            {
                airportList = value;
            }
        }

        public static List<Airport> airportList = new List<Airport>();


        public Airport(string _code, float _lat, float _lon, string _name)
        {
            Code = _code;
            Lat = _lat;
            Lon = _lon;
            Name = _name;
           
        }

        public Airport() { }

        public List<Airport> getairport()
        {
            return airportList;
        }

        public int insert (Airport[] arr)
        {
            DBservices dbs = new DBservices();
          
                int numEffected = dbs.insert(arr);
                return numEffected;
         


        }
        public List<Airport> getairports1()
        {
            DBservices dbs = new DBservices();

            return dbs.getairports();
           
        }


    }
}