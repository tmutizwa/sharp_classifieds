using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class ListingImageViewModel 
    {
        public int ListingImageId { get; set; }
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string SizeGroup { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
        public int DisplayOrder { get; set; }
    }
}