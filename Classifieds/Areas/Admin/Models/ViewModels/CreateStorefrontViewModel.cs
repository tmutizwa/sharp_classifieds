using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Areas.Admin.Models.ViewModels
{
    public class CreateStorefrontViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [Display(Name="Store title")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        public string Logo { get; set; }
    }
}