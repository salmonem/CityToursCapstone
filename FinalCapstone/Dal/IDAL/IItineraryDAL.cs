using FinalCapstone.Models;
using System;
using System.Collections.Generic;

namespace FinalCapstone.Dal
{
    public interface IItineraryDAL
    {
        Landmark GetLandmark(string id);

        IList<Landmark> GetAllLandmarks();

        int SaveUserItinerary(Itinerary itinerary, string user);

        IList<Trip> GetUserItinerary(int id); //pass in itinerary id

        IList<Trip> GetAllUserItineraries(string user); //get current username and display date saved

        int DeleteItinerary(int id);
    }
}
