using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models
{
    public class GeneralListing
    {
        public int GeneralListingId { get; set; }
        
        [MaxLength(50)]
        public string Brand { get; set; }
        [MaxLength(50)]
        public string Condition { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
    }
}