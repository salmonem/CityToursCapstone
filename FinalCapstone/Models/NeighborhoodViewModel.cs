using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class NeighborhoodViewModel
    {
        public Neighborhood Neighborhood { get; set; }
        public IList<Neighborhood> Neighborhoods { get; set; }
        public string NewReviewResult { get; set; }
        public string AttractionCode { get; set; }
    }
}
