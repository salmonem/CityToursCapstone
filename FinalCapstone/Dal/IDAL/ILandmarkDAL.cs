using FinalCapstone.Models;
using System;
using System.Collections.Generic;
namespace FinalCapstone.Dal
{
    public interface ILandmarkDAL
    {
        IList<Landmark> GetLandmark(string id);

        IList<Landmark> GetNeighborhoodLandmarks(string id);

        int SaveLandmark(Landmark newLandmark);

        void DeleteLandmark(string id);
    }
}
