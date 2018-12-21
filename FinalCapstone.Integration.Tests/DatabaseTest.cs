using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;

namespace FinalCapstone.Integration.Tests
{
    public abstract class DatabaseTest
    {
        private TransactionScope _transaction;

        [TestInitialize]
        public void DatabaseSetup()
        {
            _transaction = new TransactionScope();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            using (var connection = new SqlConnection(CityToursDbConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM Neighborhoods;
                      DELETE FROM Neighborhood_blurb;
                      DELETE FROM Review;
                      DELETE FROM Vote;
                      DELETE FROM Trips;
                      DELETE FROM Itineraries;
                      DELETE FROM Attractions;
                      DELETE FROM AspNetUserTokens;
                      DELETE FROM AspNetUsers;
                      DELETE FROM AspNetUserRoles;
                      DELETE FROM AspNetUserLogins;
                      DELETE FROM AspNetUserClaims;
                      DELETE FROM AspNetRoles;
                      DELETE FROM AspNetRoleClaims;";

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void DatabaseCleanup()
        {
            _transaction.Dispose();
        }

        protected IConfiguration Configuration { get; private set; }

        protected string CityToursDbConnectionString
        {
            get
            {
                return Configuration["ConnectionStrings:CityToursDb"];
            }
        }
    }
}
