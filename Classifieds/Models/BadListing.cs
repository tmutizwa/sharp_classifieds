using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class BadListing
    {
        public int BadListingId { get; set; }
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public string Reason { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Created { get; set; }
        public string UserId { get; set; }
    }
}