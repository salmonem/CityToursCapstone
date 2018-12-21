using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FinalCapstone.Integration.Tests
{
    [TestClass]
    public class ItineraryDalTest : DatabaseTest
    {
        private IItineraryDAL _itineraryDal;

        [TestInitialize]
        public void Setup()
        {
            _itineraryDal = new ItineraryDAL(CityToursDbConnectionString);
        }

        [TestClass]
        public class GetAllLandmarks : ItineraryDalTest
        {
            [TestMethod]
            public void GetAllLandmarkTest()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                        @"INSERT attractions VALUES ('COSI', 'COSI', 'Activity Center','COSI is a science museum and research center located in Columbus, Ohio in the United States. Originally opened in 1964, COSI was relocated to a 320,000 square foot state-of-the-art facility in 1999.','family-friendly, all-ages, IMAX','Monday Closed, Tuesday Closed, Wednesday 10AM-5PM, Thursday 10AM-5PM, Friday 10AM-5PM, Saturday 10AM-5PM, Sunday 10AM-5PM');
                          INSERT attractions VALUES ('ARTM', 'Columbus Art Museum', 'Museum','The Columbus Museum of Art is an art museum located in downtown Columbus, Ohio. Formed in 1878 as the Columbus Gallery of Fine Arts, it was the first art museum to register its charter with the state of Ohio. ','family-friendly, cafe','Monday Closed, Tuesday - Sunday 10:00AM - 5:00PM');                          
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-COSI','COSI','333 W Broad St, Columbus, OH 43215','39.965359','-82.973778','null');
                          INSERT Neighborhoods VALUES('Downtown', 'DT', 'DT-ARTM', 'ARTM', '480 E Broad St, Columbus, OH 43215','39.964821','-82.978752','null');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                IList<Landmark> landmarks = _itineraryDal.GetAllLandmarks();
                Assert.AreEqual(2, landmarks.Count);
            }
        }

        [TestClass]
        public class SaveUserItinerary : ItineraryDalTest
        {
            [TestMethod]
            public void SaveUserItineraryTest()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                        //INSERT AspNetUsers VALUES ('4b4839a0-b5cd-4320-af29-e13d815528bd','admin@admin.com','ADMIN@ADMIN.COM','admin@admin.com','ADMIN@ADMIN.COM','0','AQAAAAEAACcQAAAAELPw0mCt9zitoF9iocLKITeird8UM6PlmW2PSJRr3/j/BnB90s/SqLtXCn4UPWPkyg==','5B2FMSX6GTUDBQ324SVWCP7IPBTVBZ6V','0f761cce-49c4-49bb-a31f-0bdaf3770b77','NULL','0','0','NULL','1','0');
                          @"INSERT attractions VALUES ('COSI', 'COSI', 'Activity Center','COSI is a science museum and research center located in Columbus, Ohio in the United States. Originally opened in 1964, COSI was relocated to a 320,000 square foot state-of-the-art facility in 1999.','family-friendly, all-ages, IMAX','Monday Closed, Tuesday Closed, Wednesday 10AM-5PM, Thursday 10AM-5PM, Friday 10AM-5PM, Saturday 10AM-5PM, Sunday 10AM-5PM');
                          INSERT attractions VALUES ('ARTM', 'Columbus Art Museum', 'Museum','The Columbus Museum of Art is an art museum located in downtown Columbus, Ohio. Formed in 1878 as the Columbus Gallery of Fine Arts, it was the first art museum to register its charter with the state of Ohio. ','family-friendly, cafe','Monday Closed, Tuesday - Sunday 10:00AM - 5:00PM');                          
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-COSI','COSI','333 W Broad St, Columbus, OH 43215','39.965359','-82.973778','null');
                          INSERT Neighborhoods VALUES('Downtown', 'DT', 'DT-ARTM', 'ARTM', '480 E Broad St, Columbus, OH 43215','39.964821','-82.978752','null');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                Itinerary itinerary = new Itinerary();
                int modified = _itineraryDal.SaveUserItinerary(itinerary, "admin@admin.com");
                Assert.IsNotNull(modified);
            }
        }

        [TestClass]
        public class GetAllUserItineraries : ItineraryDalTest
        {
            [TestMethod]
            public void GetAllUserItinerariesTest()
            {
            }
        }

        [TestClass]
        public class GetUserItinerary : ItineraryDalTest
        {
            [TestMethod]
            public void GetUserItineraryTest()
            {

            }
        }

        [TestClass]
        public class DeleteItinerary : ItineraryDalTest
        {
            [TestMethod]
            public void DeleteUserItineraryTest()
            {

            }
        }
    }
}
