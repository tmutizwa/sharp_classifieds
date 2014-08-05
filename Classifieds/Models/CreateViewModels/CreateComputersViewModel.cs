using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateComputersViewModel : ACreateListingViewModel
    {
        public CreateComputersViewModel(int catId) : base(catId) { }
        public override int AddNewListing()
        {
            
            Computer ln = new Computer();
            Listing l = new Listing();
            //moths to expire this ad
            int months = (Category.ExpiresIn > 0) ? Category.ExpiresIn : 3;
            ln.Listing = l;
            ln.Listing.Title = Title;
            ln.Listing.CategoryId = CategoryId;
            ln.Listing.Description =  Description;
            ln.Listing.Price =  Price;
            ln.Listing.Town =  Town;
            ln.Listing.Created = DateTime.Now;
            ln.Listing.Updated = DateTime.Now;
            ln.Listing.Status  = "live";
            ln.Listing.OwnerId = HttpContext.Current.User.Identity.GetUserId();
            ln.Listing.Expires = DateTime.Now.AddMonths(months);
            ln.Condition = Condition;
            ln.HddSize = HddSize;
            ln.Brand = Brand;
            ln.OS = OS;
            ln.Processor = Processor;
            ln.Ram = Ram;
            ln.ScreenSize = ScreenSize;
            
            db.Computers.Add(ln);
            db.SaveChanges();
            return ln.Listing.ListingId;
        }
        string _view = "computers.cshtml";
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "Used", Value = "used" }};
        public List<SelectListItem> brands = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "HP", Value = "hp" }, new SelectListItem { Text = "Toshiba", Value = "toshiba" }, new SelectListItem { Text = "Dell", Value = "dell" }, new SelectListItem { Text = "Lenovo", Value = "lenovo" }, new SelectListItem { Text = "Asus", Value = "asus" }, new SelectListItem { Text = "Acer", Value = "acer" }, new SelectListItem { Text = "Sony", Value = "sony" }, new SelectListItem { Text = "Alienware", Value = "alienware" }, new SelectListItem { Text = "Samsung", Value = "samsung" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> oses = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" },new SelectListItem { Text = "Mac", Value = "mac" }, new SelectListItem { Text = "Windows", Value = "windows" }, new SelectListItem { Text = "Linux", Value = "linux" }, new SelectListItem { Text = "Nix", Value = "nix" }, new SelectListItem { Text = "Other", Value = "other" } };

        public override string view { get { return this._view; } set { this._view = value; } }
        public string OS { get; set; }
        public string Brand { get; set; }
        [Display(Name="HDD Size")]
        public decimal HddSize { get; set; }
        public decimal Ram { get; set; }
        [Display(Name="Screen size")]
        public decimal ScreenSize { get; set; }
        public string Processor { get; set; }
        public string Condition { get; set; }
    }
}