using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.EmailViewModel
{
    public class GeneralEmailViewModel
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Subject { get; set; }
        [EmailAddress]
        public string EmailTo { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Your email")]
        public string EmailFrom { get; set; }
        [MaxLength(300),Required]
        [Display(Name="Your message")]
        public string Body { get; set; }
        public Boolean Copy { get; set; }
        [EmailAddress]
        [Display(Name="Copy email")]
        public string CopyEmail { get; set; }
    }
}