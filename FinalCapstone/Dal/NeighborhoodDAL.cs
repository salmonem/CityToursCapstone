using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace FinalCapstone.Dal
{
    public class NeighborhoodDAL : INeighborhoodDAL
    {
        string sql = "SELECT * FROM Neighborhoods;";

        string names = "SELECT Neighborhoods.neighborhood_name, Neighborhoods.neighborhood_id, blurb from Neighborhoods join Neighborhood_blurb on Neighborhoods.neighborhood_id = Neighborhood_blurb.neighborhood_id GROUP BY Neighborhoods.neighborhood_name, Neighborhoods.neighborhood_id, blurb;";

        private readonly string connectionString;

        public NeighborhoodDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Neighborhood> GetAllNeighborhoods()
        {
            IList<Neighborhood> neighborhoods = new List<Neighborhood>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Neighborhood neighborhood = new Neighborhood
                    {
                        NeighborhoodName = Convert.ToString(reader["neighborhood_name"]),
                        AttractionCode = Convert.ToString(reader["attraction_code"]),
                        AttractionAddress = Convert.ToString(reader["attraction_address"])
                    };

                    neighborhoods.Add(neighborhood);
                }
            }
            return neighborhoods;
        }

        public IList<Neighborhood> GetNeighborhoodNames()
        {
            IList<Neighborhood> neighborhoods = new List<Neighborhood>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(names, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Neighborhood neighborhood = new Neighborhood
                    {
                        NeighborhoodName = Convert.ToString(reader["neighborhood_name"]),
                        NeighborhoodId = Convert.ToString(reader["neighborhood_id"]),
                        Blurbs = Convert.ToString(reader["blurb"])
                    };

                    neighborhoods.Add(neighborhood);
                }
            }
            return neighborhoods;
        }
    }
}
