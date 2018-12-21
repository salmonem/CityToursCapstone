using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FinalCapstone.Integration.Tests
{
    [TestClass]
    public class LandmarkDalTest : DatabaseTest
    {
        private ILandmarkDAL _landmarkDal;

        [TestInitialize]
        public void Setup()
        {
            _landmarkDal = new LandmarkDAL(CityToursDbConnectionString);
        }

        [TestClass]
        public class GetLandmark : LandmarkDalTest
        {
            [TestMethod]
            public void GetLandmarkTest()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                        @"INSERT attractions VALUES ('COSI', 'COSI', 'Activity Center','COSI is a science museum and research center located in Columbus, Ohio in the United States. Originally opened in 1964, COSI was relocated to a 320,000 square foot state-of-the-art facility in 1999.','family-friendly, all-ages, IMAX','Monday Closed, Tuesday Closed, Wednesday 10AM-5PM, Thursday 10AM-5PM, Friday 10AM-5PM, Saturday 10AM-5PM, Sunday 10AM-5PM');
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-COSI','COSI','333 W Broad St, Columbus, OH 43215','39.965359','-82.973778','null')";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                IList<Landmark> landmark = _landmarkDal.GetLandmark("COSI");
                Assert.AreEqual(1, landmark.Count);
            }
        }

        [TestClass]
        public class GetNeighborhoodLandmarks : LandmarkDalTest
        {
            [TestMethod]
            public void GetNeighborhoodLandmark()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                        @"INSERT Attractions VALUES ('16BT','16 Bit Bar and Arcade','Bar','16-Bit is a retro watering hole offering classic arcade gAMes, old-school cocktails and an awesome beer selection.','21+, video games','Monday 4PM-2:30AM, Tuesday 4PM-2:30AM, Wednesday 4PM-2:30AM, Thursday 4PM-2:30AM, Friday 4PM-2:30AM, Saturday 12PM-2:30AM, Sunday 12PM-2:30AM');
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-16BT','16BT','254 S 4th St, Columbus, OH 43215','39.957401','-82.995049','null');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                var landmarks = _landmarkDal.GetNeighborhoodLandmarks("DT");
                Assert.AreEqual(1, landmarks.Count);
            }
        }

        [TestClass]
        public class SaveLandmark : LandmarkDalTest
        {
            [TestMethod]
            public void SaveLandmarkTest()
            {
                int saveLandmark = 0;

                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                        @"INSERT Attractions VALUES ('16BT','16 Bit Bar and Arcade','Bar','16-Bit is a retro watering hole offering classic arcade games, old-school cocktails and an awesome beer selection.','21+, video games','Monday 4PM-2:30AM, Tuesday 4PM-2:30AM, Wednesday 4PM-2:30AM, Thursday 4PM-2:30AM, Friday 4PM-2:30AM, Saturday 12PM-2:30AM, Sunday 12PM-2:30AM');";
                    //INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-16BT','16BT','254 S 4th St, Columbus, OH 43215','39.957401','-82.995049','null');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    saveLandmark = command.ExecuteNonQuery();
                }
                Assert.AreEqual(1, saveLandmark);
            }
        }

        [TestClass]
        public class DeleteLandmark : LandmarkDalTest
        {
            [TestMethod]
            public void DeleteLandmarkTest()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {

                    const string sql =
                        @"INSERT Attractions VALUES ('16BT','16 Bit Bar and Arcade','Bar','16-Bit is a retro watering hole offering classic arcade gAMes, old-school cocktails and an awesome beer selection.','21+, video games','Monday 4PM-2:30AM, Tuesday 4PM-2:30AM, Wednesday 4PM-2:30AM, Thursday 4PM-2:30AM, Friday 4PM-2:30AM, Saturday 12PM-2:30AM, Sunday 12PM-2:30AM');
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-16BT','16BT','254 S 4th St, Columbus, OH 43215','39.957401','-82.995049','null');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();

                    const string delete = @"ALTER TABLE[dbo].[Neighborhoods] nocheck constraint all DELETE FROM[dbo].[Attractions] WHERE code = '16BT' ALTER TABLE[dbo].[Neighborhoods] nocheck constraint all;";             
                    command.CommandText = delete;
                    command.ExecuteNonQuery();
                }

                var landmarks = _landmarkDal.GetNeighborhoodLandmarks("DT");
                Assert.AreEqual(0, landmarks.Count);

            }
        }
    }
}


