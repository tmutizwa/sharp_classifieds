using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Computer
    {   
        public int ComputerId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public string OS { get; set; }
        public string Brand { get; set; }
        public decimal HddSize { get; set; }
        public decimal Ram { get; set; }
        public decimal ScreenSize { get; set; }
        public string Processor { get; set; }
        public string Condition { get; set; }

    }
}