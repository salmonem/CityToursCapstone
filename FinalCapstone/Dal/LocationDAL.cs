using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FinalCapstone.Dal.IDAL;
using Microsoft.SqlServer.Server;

namespace FinalCapstone.Dal
{
    public class LocationDAL : ILocationDAL
    {
        string radius = @"DECLARE @miles int = 3 / 0.0006213712; 
                          DECLARE @userloc geography = geography::Point(@latitude, @longitude, 4326).STBuffer(@miles);
            SELECT Neighborhoods.attraction_code, Neighborhoods.latitude, Neighborhoods.longitude, Attractions.name FROM Neighborhoods LEFT JOIN Attractions ON Neighborhoods.attraction_code = Attractions.code WHERE @userloc.STIntersects(GeoLoc) = 1;";

        string test = "SELECT Neighborhoods.attraction_code, Neighborhoods.latitude, Neighborhoods.longitude from Neighborhoods;";

        private readonly string connectionString;

        public LocationDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Location> LocationsInRadius(string startName, double startLatitude, double startLongitude)
        {
            IList<Location> locationList = new List<Location>();

            Location startLocation = new Location
            {
                Name = startName,
                Latitude = Convert.ToDecimal(startLatitude),
                Longitude = Convert.ToDecimal(startLongitude)
            };

            locationList.Add(startLocation);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(radius, conn);

                    cmd.Parameters.AddWithValue("@latitude", startLatitude);
                    cmd.Parameters.AddWithValue("@longitude", startLongitude);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Location radiusResult = new Location
                        {
                            Name = Convert.ToString(reader["name"]),
                            Latitude = Convert.ToDecimal(reader["latitude"]),
                            Longitude = Convert.ToDecimal(reader["longitude"])
                        };

                        locationList.Add(radiusResult);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return locationList;
        }
    }
}

