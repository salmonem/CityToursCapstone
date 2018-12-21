using FinalCapstone.Models;
using System.Collections.Generic;

namespace FinalCapstone.Dal.IDAL
{
    public interface ILocationDAL
    {
        IList<Location> LocationsInRadius(string name, double latitude, double longitude);
    }
}
