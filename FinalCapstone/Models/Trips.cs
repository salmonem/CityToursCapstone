using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string AttractionCode { get; set; }
        public DateTime DateSaved { get; set; }
        public IList<Trip> Trips { get; set; }

        public string AttractionNeighborhood { get; set; }
        public string AttractionAddress { get; set; }
        public string AttractionName { get; set; }
        public string AttractionNeighborhoodCode { get; set; }
    }
}
