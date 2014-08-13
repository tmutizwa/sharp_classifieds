using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.EmailViewModel
{
    public class ListingEmailViewModel
    {
        public ListingEmailViewModel(Listing ln)
        {
            this.Listing = ln;
            this.ListingId = ln.ListingId;
        }
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        [MaxLength(100)]
        public string Subject { get; set; }
        [Required]
        [EmailAddress]
        public string EmailTo { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name="Your Email")]
        public string EmailFrom { get; set; }
        [MaxLength(300)]
        public string Body { get; set; }
        public Boolean Copy { get; set; }
        [EmailAddress]
        public string CopyEmail { get; set; }
    }
}