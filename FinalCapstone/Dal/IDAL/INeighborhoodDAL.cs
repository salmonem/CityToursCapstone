using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public interface INeighborhoodDAL
    {
        IList<Neighborhood> GetAllNeighborhoods();

        IList<Neighborhood> GetNeighborhoodNames();
    }
}
