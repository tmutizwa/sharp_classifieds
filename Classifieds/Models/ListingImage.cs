using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Classifieds.Library;
using System.ComponentModel.DataAnnotations;
using Classifieds.Models.ViewModels;

namespace Classifieds.Models
{
    public class ListingImage
    {
        public ListingImage()  { }
        public ListingImage(ListingImage l)
        {
            ListingImageId = l.ListingImageId;
            ListingId = l.ListingId;
            Name = l.Name;
            SizeGroup = l.SizeGroup;
            DisplayOrder = l.DisplayOrder;
        }
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
        public DateTime Created { get; set;}
         
    }
}