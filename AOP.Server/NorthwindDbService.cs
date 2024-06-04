using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AOP.Server
{
    public class NorthwindDbService
    {
        private readonly IConfiguration _configuration;
        

        public NorthwindDbService(IConfiguration configuration) {
            _configuration = configuration;
        }

        protected string GetConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("Northwind");
            if (connectionString == null)
            {
                throw new Exception("Connectionstring missin");
            }
            return connectionString;
        }


        /// <summary>
        /// gets list from all countries
        /// </summary>
        /// <returns>List Models.Country</returns>
        public List<Models.Country> GetCountries()
        {
            List<Models.Country> countries = new List<Models.Country>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()) )
            {
                connection.Open();

                string query = "SELECT DISTINCT [ShipCountry] AS Name FROM Orders ORDER BY [ShipCountry]";

                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if ( !reader.IsDBNull(reader.GetOrdinal("Name")) )
                        {
                            countries.Add(item: new Models.Country() { Name = reader.GetString(reader.GetOrdinal("Name")) });
                        }
                    }
                }

            }
            return countries;
        }

        /// <summary>
        /// calculate aop for all coiuntries or selected country
        /// </summary>
        /// <param name="Country"></param>
        /// <returns>List Models.Aop</returns>
        public List<Models.Aop> GetAop(string Country = "") 
        {
            List<Models.Aop> aops = new List<Models.Aop>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string query = @"IF OBJECT_ID('tempdb..#OAVG') IS NOT NULL
                                    DROP TABLE #OAVG
	
                                SELECT 
	                                o.ShipCountry
	                                ,SUM ( (od.UnitPrice - (od.UnitPrice * od.Discount)) * od.Quantity) AS AvgOrderPrice
	                                ,o.Freight AS AvgFreight
	                                ,count(o.OrderID) AS CNT
                                INTO #OAVG
                                FROM Orders o
                                JOIN [Order Details] od ON od.OrderID = o.OrderID ";
                                if (Country != string.Empty)
                                {
                                    query += "WHERE o.ShipCountry = @ShipCountry ";
                                }
                query += @"GROUP BY o.OrderID, o.ShipCountry, o.Freight
                                ORDER BY ShipCountry 


                                SELECT 
	                                ShipCountry AS Country
	                                ,AVG(AvgOrderPrice) AS AvgOrder
	                                ,AVG(AvgFreight) AS AvgFreight
	                                ,COUNT(CNT) as OrderCount
                                FROM #OAVG ";
                                if (Country != string.Empty)
                                {
                                    query += "WHERE ShipCountry = @ShipCountry ";
                                }
                query +=        @"GROUP BY ShipCountry
                                ORDER BY ShipCountry

                                DROP TABLE #OAVG";

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@ShipCountry", Country);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Country")))
                        {
                            aops.Add(item: new Models.Aop() {
                                Country = reader["Country"].ToString(),
                                AvgOrder = Convert.ToDecimal(reader["AvgOrder"]),
                                AvgFreight = Convert.ToDecimal(reader["AvgFreight"]),
                                OrderCount = Convert.ToInt32(reader["OrderCount"]),
                            });
                        }
                    }
                }

            }
            return aops;

        }
    }
}
