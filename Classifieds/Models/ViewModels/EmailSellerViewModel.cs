using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class EmailSellerViewModel
    {
        [Required,MaxLength(20)]
        [Display(Name="Your name")]
        public string Name { get; set; }
        [Display(Name="Your phone")]
        public string Phone { get; set; }
        [EmailAddress,Required]
        [Display(Name="Your email")]
        public string EmailFrom { get; set; }
        [MaxLength(500),Required]
        public string Message { get; set; }
        [EmailAddress,Display(Name="Copy email")]
        public string CopyEmail { get; set; }
    }
}