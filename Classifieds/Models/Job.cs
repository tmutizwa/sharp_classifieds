using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Tags { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string Type { get; set; }
    }
}