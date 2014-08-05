using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Dating
    {
        public int DatingId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Interests { get; set; }
        public string Religion { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Ethnicity { get; set; }
        public string Smoke { get; set; }
        public string Drink { get; set; }
    }
}