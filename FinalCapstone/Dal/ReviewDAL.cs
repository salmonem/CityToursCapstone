using FinalCapstone.Dal.IDAL;
using FinalCapstone.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class ReviewDAL : IReviewDAL
    {
        private readonly string _connectionString;

        public ReviewDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Review> GetAllReviews(string id)
        {
            IList<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Review JOIN Attractions ON review.attraction_code = Attractions.code WHERE attraction_code = @id ORDER BY date_posted DESC", conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Review newReview = new Review
                        {
                            AttractionCode = Convert.ToString(reader["attraction_code"]),
                            Username = Convert.ToString(reader["username"]),
                            Subject = Convert.ToString(reader["subject"]),
                            Message = Convert.ToString(reader["message"]),
                            Rating = Convert.ToInt32(reader["rating"]),
                            ReviewDate = Convert.ToDateTime(reader["date_posted"])
                        };
                        reviews.Add(newReview);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return reviews;
        }

        public bool SaveNewReview(Review newReview)
        {
            bool saved = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO review(attraction_code, username, subject, message, rating, date_posted) VALUES(@attraction_code,@username, @subject, @message,@rating, @date_posted)", conn);
                    cmd.Parameters.AddWithValue("@attraction_code", newReview.AttractionCode);
                    cmd.Parameters.AddWithValue("@username", newReview.Username);
                    cmd.Parameters.AddWithValue("@subject", newReview.Subject);
                    cmd.Parameters.AddWithValue("@message", newReview.Message);
                    cmd.Parameters.AddWithValue("@rating", newReview.Rating);
                    cmd.Parameters.AddWithValue("@date_posted", DateTime.UtcNow);

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
