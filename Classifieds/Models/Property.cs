using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public string Suburb { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Toilets {get; set;}
        public int Garages { get; set; }
        public int Boreholes { get; set; }
        public string Power { get; set; }
        public decimal LandArea { get; set; }
        public decimal BuildingArea { get; set; }
    }
}