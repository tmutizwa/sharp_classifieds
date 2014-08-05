using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models.EditViewModels
{
    public abstract class AEditListingViewModel
    {
         public readonly ApplicationDbContext db = new ApplicationDbContext();
        readonly string _viewFolder = "~/Views/Listing/Edit/";
        public AEditListingViewModel() { }
        public AEditListingViewModel(Listing listing) {
            
            ListingId = listing.ListingId;
            CategoryId = listing.Category.CategoryId;
            Category = listing.Category;
            Title = listing.Title;
            Description = listing.Description;
            Price = listing.Price;
            Town = listing.Town;
            Status = listing.Status;

        }
        Boolean _showCondition = true;
        public Boolean showCondition { get { return this._showCondition; } set { this._showCondition = value; } }
        Boolean _showPrice = true;
        public Boolean showPrice { get { return this._showPrice; } set { this._showPrice = value; } }
        public List<SelectListItem> statuses = new List<SelectListItem> { new SelectListItem { Text = "Live", Value = "live" }, new SelectListItem { Text = "Suspended", Value = "suspended" } };
        public abstract void EditListing();
        //public abstract void updateModel(int ListingId,AListing model);
        public int ListingId { get; set; }
        [Required]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage="Title cannot be more than 100 characters long."), MinLength(3,ErrorMessage="Title cannot be less than 3 characters long.")]
        public string Title { get; set; }
        [MaxLength(500,ErrorMessage="Description cannot be more than 500 characters long."),Display(Name="Short Description")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }
        [MaxLength(30,ErrorMessage = "Town cannot be more than 30 characters long.")]
        [Display(Name="City / Town")]
        public string Town { get; set; }
        public string Status { get; set; }
        public string viewFolder { get { return this._viewFolder; } set { } }
        public abstract string view { get; set; } 
    }
}