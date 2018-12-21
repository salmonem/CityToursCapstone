using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Neighborhood
    {
        public string NeighborhoodName { get; set; }

        [Required(ErrorMessage = "Please select the neighborhood.")]
        [Display(Name = "Neighborhood")]
        public string NeighborhoodId { get; set; }

        public static List<SelectListItem> ListOfNeighborhoodIds = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Downtown", Value="DT"},
            new SelectListItem() { Text = "Short North", Value="SN"},
            new SelectListItem() { Text = "Polaris/North", Value="PN"},
            new SelectListItem() { Text = "Easton", Value="EA"},
            new SelectListItem() { Text = "Campus/Upper Arlington", Value="CA"},
        };

        public string AttractionNeighborhood { get; set; }
        public string AttractionCode { get; set; }

        [Required(ErrorMessage = "Please enter the address.")]
        public string AttractionAddress { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string GeoLoc { get; set; }

        IList<Neighborhood> landmarks { get; set; } = new List<Neighborhood>();

        public string Blurbs { get; set; }
    }
}
