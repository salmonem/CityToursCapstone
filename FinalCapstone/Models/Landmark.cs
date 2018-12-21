using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Landmark : Neighborhood
    {
        [Required(ErrorMessage = "Please enter the code.")]
        [StringLength(4, ErrorMessage = "Code cannot be more or less than 4 characters", MinimumLength = 4)]
        [Display(Name = "Attraction Code:")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [Display(Name = "Attraction Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a type.")]
        [RegularExpression(@"(Restaurant|Park|Arena|Activity Center)", ErrorMessage = "Code must be Restaurant, Park, Arena, or Activity Center")]
        [Display(Name = "Attraction Type:")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(800, ErrorMessage = "Description cannot be more than 800 characters")]
        [Display(Name = "Attraction Description:")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the hours.")]
        [Display(Name = "Attraction Hours:")]
        public string Hours { get; set; }

        [Required(ErrorMessage = "Please enter feature(s).")]
        [Display(Name = "Attraction Features:")]

        public string Features { get; set; }

        public string AttractionNeighborhoodCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
