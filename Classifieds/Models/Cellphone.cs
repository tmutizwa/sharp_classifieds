using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Cellphone
    {
        public Cellphone() { }
        public int CellphoneId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        [MinLength(3), MaxLength(100)]
        public string OS { get; set; }
        public string Brand { get; set; }
        public string CModel { get; set; }
        public decimal ScreenSize { get; set; }
        [Display(Name="Sim/Network type")]
        public string NetworkType { get; set; }
        public string Condition { get; set; }
    }
}