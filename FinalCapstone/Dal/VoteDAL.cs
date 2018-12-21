using FinalCapstone.Dal.IDAL;
using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class VoteDAL : IVoteDAL
    {
        private readonly string _connectionString;

        public VoteDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool SaveVote(Vote newVote)
        {
            bool saved = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO vote(attraction_code, username, value) VALUES(@attraction_code,@username, @value)", conn);
                    cmd.Parameters.AddWithValue("@attraction_code", newVote.AttractionCode);
                    cmd.Parameters.AddWithValue("@username", newVote.Username);
                    cmd.Parameters.AddWithValue("@value", newVote.Value);

                    cmd.ExecuteNonQuery();
                }
                saved = true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return saved;
        }
    }
}
