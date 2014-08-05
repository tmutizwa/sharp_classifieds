using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models.ViewModels
{
    public class ListingViewModel 
    {
        public ListingViewModel()
        {

        }
        public List<SelectListItem> statuses = new List<SelectListItem> { new SelectListItem{ Text = "Live",Value="live"},new SelectListItem{Text="Suspended", Value="suspended"}};
        public ListingViewModel(Category cat)
        {
            Category = cat;
            CategoryId = cat.CategoryId;
        }
        public ListingViewModel(Listing listing)
        {
            ListingId = listing.ListingId;
            Title = listing.Title;
            Description = listing.Description;
            Price = listing.Price;
            Town = listing.Town;
            CategoryId = listing.CategoryId;
            Category = listing.Category;
            Status = listing.Status;
            Images = listing.images;
            Expires = listing.Expires;
            Updated = listing.Updated;
        }
        public int ListingId{get; set;}
        [Required]
        [MaxLength(100,ErrorMessage="Title cannot be more than 100 characters long."), MinLength(3,ErrorMessage="Title cannot be less than 3 characters long.")]
        public string Title { get; set; }
        [MaxLength(500,ErrorMessage="Description cannot be more than 500 characters long."),Display(Name="Short Description")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }
        [MaxLength(30,ErrorMessage="Town cannot be more than 30 characters long.")]
        [Display(Name="City / Town")]
        public string Town { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Status { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Updated { get; set; }
        public List<ListingImage> Images { get; set; }
    }
}