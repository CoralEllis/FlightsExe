using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tar1.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;

namespace Tar1.Models.DAL
{
    public class DBservices
    {
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 20;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        public int insert(Airport[] airports)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            int numEffected = 0;
            try
            {
                con = connect("DBConnectionString");
                foreach (var item in airports)
                {
                    String cStr = BuildInsertCommand(item);
                    cmd = CreateCommand(cStr, con);
                    try
                    {
                        numEffected = cmd.ExecuteNonQuery(); // execute the command
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return numEffected;
        }
        private String BuildInsertCommand(Airport airport)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}','{2}', '{3}')", airport.Code, airport.Lat, airport.Lon, airport.Name);
            String prefix = "INSERT INTO Airport " + "(APcode,Lat,Lon,Name) ";
            command = prefix + sb.ToString();

            return command;
        }
        public List<Airport> getairports()
        {
            List<Airport> airportsList = new List<Airport>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Airport";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Airport A = new Airport();
                    A.Code = (string)dr["APcode"];
                    airportsList.Add(A);
                }
                return airportsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void insert(Flight flight)
        {
            SqlConnection con = null;
            int counter = 0;
            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Favorite1_2020";
                SqlCommand cmd1 = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    if (dr["FlightId"].ToString() == flight.Idflight)
                    {
                        counter = Convert.ToInt32(dr["Counterofave"]) + 1;
                        insertcounter(counter, flight.Idflight);
                    }
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            if (counter == 0)
                insertflight(flight);
        }
        private int insertflight(Flight flight)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(flight);

            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(Flight flight)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            string p = flight.Price.ToString();
            sb.AppendFormat("Values('{0}', '{1}','{2}', '{3}','{4}','{5}','{6}','{7}','{8}')", flight.Idflight, flight.Origin, flight.Destination, flight.StartDate, flight.EndDate, p, flight.Route, flight.Airlinecompany, "1");
            String prefix = "INSERT INTO Favorite1_2020" + "(FlightId,Origin,Destination,Startdate,Endate,Price,Connections,Airlinecompany,Counterofave)";
            command = prefix + sb.ToString();

            return command;
        }
        public List<Flight> getflights()
        {
            List<Flight> flightsList = new List<Flight>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Favorite1_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Flight F = new Flight();

                    F.Origin = (string)dr["Origin"];
                    F.Destination = (string)dr["Destination"];
                    F.StartDate = (string)dr["Startdate"];
                    F.EndDate = (string)dr["Endate"];
                    F.Price = Convert.ToDouble(dr["Price"]);
                    F.Airlinecompany = (string)dr["Airlinecompany"];
                    F.Count = Convert.ToInt32(dr["Counterofave"]);
                    if (dr["Connections"] != null)
                    {
                        F.Route = (string)dr["Connections"];
                    }

                    flightsList.Add(F);
                }

                return flightsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        private void insertcounter(int counter, string id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(counter, id);
            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(int counter, string id)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            string p = counter.ToString();
            command = "UPDATE Favorite1_2020 SET Counterofave =" + p + "WHERE FlightId ='" + id + "'";

            return command;
        }

        public int insertcustomer(Customer customer)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(customer);

            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(Customer customer)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            string p = customer.Agree.ToString();
            sb.AppendFormat("Values('{0}','{1}','{2}')", customer.Email, customer.Fullnames, p);
            String prefix = "INSERT INTO Customer1_2020" + "(Email,FullNames,Agree)";
            command = prefix + sb.ToString();

            return command;
        }

        public int insertOrder(Order order)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(order);

            cmd = CreateCommand(cStr, con);
            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(Order order)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            string p = order.Price.ToString();
            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", order.Origin, order.Destination, order.StartDate, order.EndDate, p, order.Route, order.Airlinecompany, order.Email, order.Id);
            String prefix = "INSERT INTO Order1_2020" + "(Origin,Destination,StartDate,Endate,Price,Connections,Airlinecompany,Email,id)";
            command = prefix + sb.ToString();

            return command;
        }

        public List<UserManager> check()
        {
            List<UserManager> u = new List<UserManager>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM managersusers";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserManager us = new UserManager(dr["Username"].ToString(), dr["passwords"].ToString());
                    u.Add(us);
                }
                return u;
            }
           catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            { 
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void insert(Discount discount)
        {

            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(discount);

            cmd = CreateCommand(cStr, con);
            try
            {
                cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommand(Discount discount)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            string p = discount.Discount1.ToString();
            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}')", discount.Airline, discount.Origin, discount.Destination, discount.Startdate, discount.Enddate, p);
            String prefix = "INSERT INTO Discount_2020" + "(Airlinecompany,Origin,Destination,Startdate,Endate,Dicount)";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Discount> checkdisc()
        {
            List<Discount> discList = new List<Discount>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select * from Discount_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Discount d = new Discount();
                    d.Airline = (string)dr["Airlinecompany"];
                    d.Origin = (string)dr["Origin"];
                    d.Destination = (string)dr["Destination"];
                    d.Startdate = (string)dr["Startdate"];
                    d.Enddate = (string)dr["Endate"];
                    d.Discount1 = Convert.ToInt32(dr["Dicount"]);
                    d.Id = Convert.ToInt32(dr["discountid"]);
                    discList.Add(d);
                }

                return discList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }

        public List<Order> getid()
        {
            List<Order> orderList = new List<Order>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Order1_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Order d = new Order();
                    d.Origin = (string)dr["Origin"];
                    d.Destination = (string)dr["Destination"];
                    d.StartDate = (string)dr["Startdate"];
                    d.EndDate = (string)dr["Endate"];
                    d.Price = Convert.ToInt32(dr["Price"]);
                    d.Route = (string)dr["Connections"];
                    d.Airlinecompany = (string)dr["Airlinecompany"];
                    d.Id = (string)dr["id"];

                    orderList.Add(d);
                }

                return orderList;
            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Order> getorder()
        {

            List<Order> orderList = new List<Order>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Order1_2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Order d = new Order();
                    d.Origin = (string)dr["Origin"];
                    d.Destination = (string)dr["Destination"];
                    d.StartDate = (string)dr["Startdate"];
                    d.EndDate = (string)dr["Endate"];
                    d.Price = Convert.ToInt32(dr["Price"]);

                    orderList.Add(d);
                }

                return orderList;
            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        public void deletedisc(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            string x = id.ToString();
            String cStr = "DELETE FROM Discount_2020 WHERE discountid = '" + x + "'";

            cmd = CreateCommand(cStr, con);
            try
            {
                cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void updatedisc(int id, Discount d)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
           
            String cStr = BuildInsertCommand(id, d);
            cmd = CreateCommand(cStr, con);
            try
            {
                cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(int id, Discount d)
        {
            StringBuilder sb = new StringBuilder();
            string p = d.Discount1.ToString();
            string Id = id.ToString();
            String prefix = "UPDATE Discount_2020 ";
            prefix += "SET Airlinecompany = '" + d.Airline + "', Origin = '" + d.Origin + "', Destination = '" + d.Destination + "', Startdate = '" + d.Startdate + "', Endate = '" + d.Enddate + "', Dicount = '" + p + "' ";
            prefix +=" WHERE discountid = '"+ Id + "';";
             return prefix;
        }

        public void postman(UserManager newuser)
        {

            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(newuser);

            cmd = CreateCommand(cStr, con);
            try
            {
                cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(UserManager newuser)
        {
            string command;
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values('{0}', '{1}')", newuser.Username, newuser.Password);
            String prefix = "INSERT INTO managersusers " + "(Username,passwords) ";
            return command = prefix + sb.ToString();
        }
        public void DeleteMan(string email)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
           
            String cStr = "delete from managersusers where Username='"+email+"'";

            cmd = CreateCommand(cStr, con);
            try
            {
                cmd.ExecuteNonQuery(); // execute the command

            }
            catch (Exception ex)
            {

                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

    }

}