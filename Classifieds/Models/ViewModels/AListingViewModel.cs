using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models.ViewModels
{
    public abstract class AListingViewModel
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public AListingViewModel(){}
        public AListingViewModel(Listing listing) {
            ListingId = listing.ListingId;
            CategoryId = listing.Category.CategoryId;
            Category = listing.Category;
            Title = listing.Title;
            Description = listing.Description;
            Price = listing.Price;
            Town = listing.Town;
            Status = listing.Status;
            Expires = listing.Expires;
            Updated = listing.Updated;
            Created = listing.Created;
            Images = listing.images;
            Owner = listing.Owner;

        }
        readonly string _viewFolder = "~/Views/Listing/View/";
        readonly string _deleteFolder = "~/Views/Listing/Delete/";
        readonly string _detailsFolder = "~/Views/Listing/Details/";
        public List<SelectListItem> statuses = new List<SelectListItem> { new SelectListItem { Text = "Live", Value = "live" }, new SelectListItem { Text = "Suspended", Value = "suspended" } };
        //public abstract void updateModel(int ListingId,AListing model);
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        public Deal Deal { get; set; }
        public int DealId { get; set; }
        public ApplicationUser Owner { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Expires { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Updated { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Created { get; set; }
        public List<ListingImage> Images { get; set; }
        public string viewFolder { get { return this._viewFolder; } set { } }
        public string deleteFolder { get { return this._deleteFolder; } set { } }
        public string detailsFolder { get { return this._detailsFolder; } set { } }
        public abstract string view { get; set; } 
        
    }
}