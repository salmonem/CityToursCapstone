using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace FinalCapstone.Dal
{
    public class ItineraryDAL : IItineraryDAL
    {
        string sql = @"SELECT * FROM Attractions JOIN Neighborhoods ON Neighborhoods.attraction_code = Attractions.code;";
        string tripsSql = @"SELECT itineraryId, date_saved FROM trips JOIN itineraries ON itineraries.id = trips.itineraryId WHERE username = @user GROUP BY itineraryId, date_saved";
        string oneTripSql = @"SELECT * FROM trips JOIN itineraries ON itineraries.id = trips.itineraryId JOIN Attractions ON Attractions.code = trips.attraction_code JOIN Neighborhoods on Neighborhoods.attraction_code = Attractions.code WHERE itineraryId = @id;";
        string deleteTripSql = @"DELETE FROM Trips WHERE itineraryId = @id; DELETE FROM Itineraries WHERE Id = @id;";

        private readonly string connectionString;

        public ItineraryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Landmark GetLandmark(string id)
        {
            return GetAllLandmarks().FirstOrDefault(p => p.Code == id);
        }

        public IList<Landmark> GetLandmarks()
        {
            return GetAllLandmarks();
        }

        public IList<Landmark> GetAllLandmarks()
        {
            IList<Landmark> getLandmarks = new List<Landmark>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Landmark landmark = new Landmark
                    {
                        Code = Convert.ToString(reader["code"]),
                        Name = Convert.ToString(reader["name"]),
                        Type = Convert.ToString(reader["type"]),
                        Description = Convert.ToString(reader["description"]),
                        Features = Convert.ToString(reader["features"]),
                        Hours = Convert.ToString(reader["hours"]),
                        NeighborhoodId = Convert.ToString(reader["neighborhood_id"]),
                        NeighborhoodName = Convert.ToString(reader["neighborhood_name"]),
                        AttractionAddress = Convert.ToString(reader["attraction_address"]),
                        AttractionNeighborhoodCode = Convert.ToString(reader["attraction_neighborhood"]),
                        //Latitude = Convert.ToDecimal("latitude"),
                        //Longitude = Convert.ToDecimal(reader["longitude"])

                    };

                    getLandmarks.Add(landmark);
                }
            }
            return getLandmarks;
        }

        public int SaveUserItinerary(Itinerary itinerary, string user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO itineraries output INSERTED.id VALUES(@username, @date_saved)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user);
                    cmd.Parameters.AddWithValue("@date_saved", DateTime.Now);

                    conn.Open();

                    int modified = (int)cmd.ExecuteScalar();

                    for (int i = 0; i < itinerary.Items.Count(); i++)
                    {
                        using (SqlCommand cmd1 = new SqlCommand("INSERT INTO trips VALUES" +
                                    "(@attraction_code,@itinerary_id)", conn))
                        {
                            cmd1.Parameters.AddWithValue("@itinerary_id", modified);
                            cmd1.Parameters.AddWithValue("@attraction_code", itinerary.Items[i].Landmark.Code);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    return modified;
                }
            }
        }

        public IList<Trip> GetAllUserItineraries(string user)
        {
            IList<Trip> getItineraries = new List<Trip>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(tripsSql, conn);

                cmd.Parameters.AddWithValue("@user", user);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trip trip = new Trip
                    {
                        Id = Convert.ToInt32(reader["itineraryId"]),
                        DateSaved = Convert.ToDateTime(reader["date_saved"])
                    };
                    getItineraries.Add(trip);
                }
            }
            return getItineraries;
        }

        public IList<Trip> GetUserItinerary(int id)
        {
            IList<Trip> getItinerary = new List<Trip>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(oneTripSql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trip trip = new Trip
                    {
                        Id = Convert.ToInt32(reader["itineraryId"]),
                        DateSaved = Convert.ToDateTime(reader["date_saved"]),
                        AttractionCode = Convert.ToString(reader["attraction_code"]),
                        AttractionAddress = Convert.ToString(reader["attraction_address"]),
                        AttractionName = Convert.ToString(reader["name"]),
                        AttractionNeighborhoodCode = Convert.ToString(reader["attraction_neighborhood"])
                    };
                    getItinerary.Add(trip);
                }
            }
            return getItinerary;
        }

        public int DeleteItinerary(int id)
        {
            int deleteItinerary = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(deleteTripSql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return deleteItinerary;
        }
    }
}
