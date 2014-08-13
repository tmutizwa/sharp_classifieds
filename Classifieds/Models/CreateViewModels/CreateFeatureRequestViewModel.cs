using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateFeatureRequestViewModel
    {
        public CreateFeatureRequestViewModel() { }
        [Required]
        public string UserId { get; set; }
        public string Title { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Detail { get; set; }

    }
}