using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class DisplayReviewsViewModel
    {
        public int Value { get; set; }
        public string Username { get; set; }
        public string AttractionCode { get; set; }
        public string NewReviewResult { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<Landmark> Landmarks { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
