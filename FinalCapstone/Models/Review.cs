using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Review
    {
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject must be at least 2 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Must include a Message.")]
        public string Message { get; set; }

        public DateTime ReviewDate { get; set; }
        public string AttractionCode { get; set; }
        public int Rating { get; set; }
    }
}
