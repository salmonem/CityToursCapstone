using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Location
    {
        public string Name { get; internal set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string GeoLoc { get; set; }

        IList<Location> landmarks { get; set; } = new List<Location>();
    }
}
