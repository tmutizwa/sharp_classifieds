using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateMessageViewModel
    {
        public CreateMessageViewModel() { }
        [Display(Name="Name")]
        public string SenderName { get; set; }
        [Required,MaxLength(500,ErrorMessage="Your message too long, max 500 characters.")]
        public string Detail { get; set; }
        [Required, EmailAddress,Display(Name="Email")]
        public string SenderEmail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public int ListingId { get; set; }
    }
}