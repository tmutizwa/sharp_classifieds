using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateBugReportViewModel
    {
        public CreateBugReportViewModel() { }
        [Required]
        public string UserId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Title { get; set; }
        [Display(Name="Bug details"),MinLength(50)]
        [Required]
        public string Detail { get; set; }
    }
}