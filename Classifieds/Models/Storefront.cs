using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Storefront
    {
        public int StorefrontId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public ApplicationUser Owner { get; set; }
        [Display(Name = "Owner ID")]
        public string OwnerId { get; set; }
        public string UpdaterId { get; set; }
        public string Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        [Column(TypeName = "datetime2")]
        public DateTime? Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        [Column(TypeName = "datetime2")]
        public DateTime? Updated { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        [Column(TypeName = "datetime2")]
        public DateTime? Expires { get; set; }
    }
}