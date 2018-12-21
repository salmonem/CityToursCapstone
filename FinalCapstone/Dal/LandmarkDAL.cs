using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace FinalCapstone.Dal
{
    public class LandmarkDAL : ILandmarkDAL
    {
        public string sql = @"SELECT * FROM Attractions JOIN Neighborhoods ON Neighborhoods.attraction_code = Attractions.code WHERE Neighborhoods.attraction_code = @code;";
        public string neighbohoodLandmarkSql = @"SELECT * FROM Neighborhoods JOIN Attractions ON Neighborhoods.attraction_code = Attractions.code WHERE Neighborhoods.neighborhood_id = @id;";
        public string delete = "ALTER TABLE[dbo].[Neighborhoods] nocheck constraint all DELETE FROM[dbo].[Attractions] WHERE code = @id ALTER TABLE[dbo].[Neighborhoods] nocheck constraint all;";


        private readonly string connectionString;

        public LandmarkDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Landmark> GetLandmark(string id)
        {
            IList<Landmark> getLandmark = new List<Landmark>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@code", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Landmark landmark = new Landmark
                        {


                            Name = Convert.ToString(reader["name"]),
                            Type = Convert.ToString(reader["type"]),
                            Description = Convert.ToString(reader["description"]),
                            Hours = Convert.ToString(reader["hours"]),
                            AttractionAddress = Convert.ToString(reader["attraction_address"]),
                            NeighborhoodId = Convert.ToString(reader["neighborhood_id"]),
                            Code = Convert.ToString(reader["attraction_code"]),
                            Features = Convert.ToString(reader["features"]),
                            NeighborhoodName = Convert.ToString(reader["neighborhood_name"])                          
                        };

                        getLandmark.Add(landmark);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return getLandmark;
        }

        public IList<Landmark> GetNeighborhoodLandmarks(string id)
        {
            IList<Landmark> landmarks = new List<Landmark>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(neighbohoodLandmarkSql, conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Landmark landmark = new Landmark
                        {
                            Name = Convert.ToString(reader["name"]),
                            Type = Convert.ToString(reader["type"]),
                            Description = Convert.ToString(reader["description"]),
                            Hours = Convert.ToString(reader["hours"]),
                            NeighborhoodName = Convert.ToString(reader["neighborhood_name"]),
                            AttractionAddress = Convert.ToString(reader["attraction_address"]),
                            NeighborhoodId = Convert.ToString(reader["neighborhood_id"]),
                            Code = Convert.ToString(reader["attraction_code"]),
                            Features = Convert.ToString(reader["features"])

                        };

                        landmarks.Add(landmark);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return landmarks;
        }

        public int SaveLandmark(Landmark newLandmark)
        {
            int saveLandmark = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO Attractions VALUES (@code, @name, @type, @description, @features, @hours);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", newLandmark.Code.ToUpper());
                    cmd.Parameters.AddWithValue("@name", newLandmark.Name);
                    cmd.Parameters.AddWithValue("@type", newLandmark.Type);
                    cmd.Parameters.AddWithValue("@description", newLandmark.Description);
                    cmd.Parameters.AddWithValue("@features", newLandmark.Features);
                    cmd.Parameters.AddWithValue("@hours", newLandmark.Hours);

                    saveLandmark = cmd.ExecuteNonQuery();
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql2 = "INSERT INTO [dbo].[Neighborhoods]([neighborhood_name],[neighborhood_id],[attraction_neighborhood],[attraction_code],[attraction_address]) VALUES (@neighname, @neighId, @AttNeigh, @code, @address);";

                    if (newLandmark.NeighborhoodId == "DT")
                    {
                        newLandmark.NeighborhoodName = "Downtown";
                    }
                    if (newLandmark.NeighborhoodId == "SN")
                    {
                        newLandmark.NeighborhoodName = "Short North";
                    }
                    if (newLandmark.NeighborhoodId == "PN")
                    {
                        newLandmark.NeighborhoodName = "Polaris/ North";
                    }
                    if (newLandmark.NeighborhoodId == "EA")
                    {
                        newLandmark.NeighborhoodName = "Easton";
                    }
                    if (newLandmark.NeighborhoodId == "CA")
                    {
                        newLandmark.NeighborhoodName = "Campus/ Upper Arlington";
                    }

                    string combine = newLandmark.NeighborhoodId + "-" + newLandmark.Code.ToUpper();

                    SqlCommand cmd = new SqlCommand(sql2, conn);
                    cmd.Parameters.AddWithValue("@neighname", newLandmark.NeighborhoodName);
                    cmd.Parameters.AddWithValue("@neighId", newLandmark.NeighborhoodId);
                    cmd.Parameters.AddWithValue("@attNeigh", combine);
                    cmd.Parameters.AddWithValue("@code", newLandmark.Code.ToUpper());
                    cmd.Parameters.AddWithValue("@address", newLandmark.AttractionAddress);

                    saveLandmark += cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return saveLandmark;
        }

        public void DeleteLandmark(string id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(delete, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteReader();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}


