using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FinalCapstone.Integration.Tests
{
    [TestClass]
    public class NeighborhoodDALTest : DatabaseTest
    {
        private INeighborhoodDAL _neighborhoodDal;

        [TestInitialize]
        public void Setup()
        {
            _neighborhoodDal = new NeighborhoodDAL(CityToursDbConnectionString);
        }

        [TestClass]
        public class GetAllNeighborhoods : NeighborhoodDALTest
        {
            [TestMethod]
            public void GetAllNeighborhoodsTest()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                          @"INSERT attractions VALUES ('FUBO', 'Funny Bone','Bar/Nightclub','Comedy chain outpost hosting local & major talent, with the option of casual American dining.','21+','Depends on Show');
                          INSERT attractions VALUES('COSI', 'COSI', 'Activity Center', 'COSI is a science museum and research center located in Columbus, Ohio in the United States. Originally opened in 1964, COSI was relocated to a 320,000 square foot state-of-the-art facility in 1999.', 'family-friendly, all-ages, IMAX', 'Monday Closed, Tuesday Closed, Wednesday 10AM-5PM, Thursday 10AM-5PM, Friday 10AM-5PM, Saturday 10AM-5PM, Sunday 10AM-5PM');
                          INSERT attractions VALUES('IKEA', 'IKEA', 'Shopping', 'Scandinavian chain selling ready-to-assemble furniture, plus housewares, in a warehouse-like space.', 'child-care, restaraunt, cafe', 'Monday 10AM-9PM, Tuesday 10AM-9PM, Wednesday 10AM-9PM, Thursday 10AM-9PM, Friday 10AM-9PM, Saturday 10AM-9PM, Sunday 10AM-8PM');
                          INSERT Neighborhoods VALUES ('Easton', 'EA', 'EA-FUBO','FUBO','145 Easton Town Center, Columbus, OH 43219','40.050584','-82.916091','null');
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-COSI','COSI','333 W Broad St, Columbus, OH 43215','39.965359','-82.973778','null');
                          INSERT Neighborhoods VALUES ('Polaris/ North', 'PN', 'PM-IKEA','IKEA','1900 Ikea Way, Columbus, OH 43240','40.150137','-82.966790','null');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                IList<Neighborhood> neighborhoods = _neighborhoodDal.GetAllNeighborhoods();
                Assert.AreEqual(3, neighborhoods.Count);
            }
        }

        [TestClass]
        public class GetNeighborhoodNames : NeighborhoodDALTest
        {
            [TestMethod]
            public void GetNeighborhoodNamesTest()
            {
                using (var connection = new SqlConnection(CityToursDbConnectionString))
                {
                    const string sql =
                        @"INSERT attractions VALUES ('FUBO', 'Funny Bone','Bar/Nightclub','Comedy chain outpost hosting local & major talent, with the option of casual American dining.','21+','Depends on Show');
                          INSERT attractions VALUES('COSI', 'COSI', 'Activity Center', 'COSI is a science museum and research center located in Columbus, Ohio in the United States. Originally opened in 1964, COSI was relocated to a 320,000 square foot state-of-the-art facility in 1999.', 'family-friendly, all-ages, IMAX', 'Monday Closed, Tuesday Closed, Wednesday 10AM-5PM, Thursday 10AM-5PM, Friday 10AM-5PM, Saturday 10AM-5PM, Sunday 10AM-5PM');
                          INSERT attractions VALUES('IKEA', 'IKEA', 'Shopping', 'Scandinavian chain selling ready-to-assemble furniture, plus housewares, in a warehouse-like space.', 'child-care, restaraunt, cafe', 'Monday 10AM-9PM, Tuesday 10AM-9PM, Wednesday 10AM-9PM, Thursday 10AM-9PM, Friday 10AM-9PM, Saturday 10AM-9PM, Sunday 10AM-8PM');
                          INSERT Neighborhoods VALUES ('Easton', 'EA', 'EA-FUBO','FUBO','145 Easton Town Center, Columbus, OH 43219','40.050584','-82.916091','null');
                          INSERT Neighborhoods VALUES ('Downtown', 'DT', 'DT-COSI','COSI','333 W Broad St, Columbus, OH 43215','39.965359','-82.973778','null');
                          INSERT Neighborhoods VALUES ('Polaris/ North', 'PN', 'PM-IKEA','IKEA','1900 Ikea Way, Columbus, OH 43240','40.150137','-82.966790','null');
                          INSERT Neighborhood_blurb VALUES ('EA','Easton Town Center is an indoor and outdoor shopping complex in northeast Columbus. 
                            As a planned outdoor retail center, Easton has a uniform early 20th century design and is replete with beautiful fountains and an attractive “town square” that often hosts outdoor events. 
                            In 2000, Easton Town Center received the ICSC award for Innovative Design of a New Project. 
                            Originally envisioned as a Limited Brands Shopping Center, Easton has become a popular destination for shopping, dining, and family entertainment. 
                            The center hosts regular outdoor events such as the annual Christmas Tree Lighting, a weekly seasonal farmer’s market, and the family-friendly Superhero Day. 
                            Easton contains a balance of retail locations, eateries, bars, a comedy club, and an AMC movie theater.');
                          INSERT Neighborhood_blurb VALUES ('DT','Columbus is Ohio’s state capital and downtown Columbus is the heart of the city for entertainment, business, and culture. 
                            The Scioto River runs along downtown and by the Scioto Audubon Metro Park (rock-climbing, hiking, fishing, etc), eventually meeting up with the Ohio river. 
                            Also located by the river is the Center of Science and Industry, or COSI for short. 
                            COSI is a science museum and learning center with dynamic hands-on activities that is fun for all ages. 
                            The nightlife is vast with many bars and clubs downtown; 16-Bit, Elevator, Brothers, and more. 
                            Diverse and vibrant, there is always something to experience downtown.');
                          INSERT Neighborhood_blurb VALUES ('PN','Founded in 1987, Polaris is a mixed-use development covering almost 2,000 acres. 
                            Since the interchange opened in 1991, hundreds of buildings have been built totaling over 8 million square feet of space with over 180 companies currently located at Polaris. 
                            The main attraction is the Polaris Fashion Place. 
                            Opened in 2001, Polaris Fashion Place is a two level shopping mall with 8 anchor stores and locally-owned specialty shops serving more than two million locals and tourists alike. 
                            Other than the mall, there are many locally owned restaurants and stores in the area that people of all ages can enjoy.
                            A little further away in Powell is the Columbus Zoo and Aquarium, a non-profit zoo with a waterpark. 
                            Named the number one zoo in the United States in 2009 by the USA Travel Guide, the zoo is home to over 7,000 animals and operates its own conservation program.');";

                    var command = connection.CreateCommand();
                    command.CommandText = sql;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                IList<Neighborhood> neighborhoods = _neighborhoodDal.GetNeighborhoodNames();
                Assert.AreEqual(3, neighborhoods.Count);
            }

        }
    }
}
