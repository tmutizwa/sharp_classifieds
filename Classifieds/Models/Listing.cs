using Classifieds.Areas.Admin.Models;
using Classifieds.Library;
using Classifieds.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models
{
    public class Listing
    {
        public Listing() { }
       
        public List<SelectListItem> Statuses = new List<SelectListItem> { new SelectListItem { Text = "Live", Value = "live" }, new SelectListItem { Text = "Suspended", Value = "suspended" } };
        [Display(Name = "Listing ID")]
        public int ListingId { get; set; }
        [Required]
        [MaxLength(100),MinLength(3)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }
        public int Hits { get; set; }
        [Required, Display(Name="Category ID")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Display(Name="Owner ID")]
        public string OwnerId { get; set; }
        [MaxLength(30)]
        public string Town { get; set; }
        public ApplicationUser Owner { get; set; }
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
        public List<ListingImage> images { get; set; }
        public string getIntro()
        {
            string st = this.Description;
            if(!String.IsNullOrEmpty(this.Description) && this.Description.Count() > 160)
                st =  this.Description.Substring(0, 160)+"...";
            return st;
        }
        
        public void mapModel (ListingViewModel list)
        {
            ListingId = list.ListingId;
            CategoryId = list.CategoryId;
            Title = list.Title;
            Description = list.Description;
            Price = list.Price;
            Town = list.Town;
            Status = list.Status;
        }

    }
}