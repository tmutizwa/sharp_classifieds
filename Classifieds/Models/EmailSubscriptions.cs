using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class EmailSubscriptions
    {
        [Key]
        public int EmailSubscriptionId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Period { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Started { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Updated { get; set; }
    }
}