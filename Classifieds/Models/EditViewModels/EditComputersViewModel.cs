using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.EditViewModels
{
    public class EditComputersViewModel : AEditListingViewModel
    {
        public EditComputersViewModel():base() { }
        
        public EditComputersViewModel(Listing l):base(l) {
            var lQ = from c in db.Computers.Include("Listing")
                     where c.ListingId == l.ListingId
                     select c;
            var ln = lQ.FirstOrDefault();
            Condition = ln.Condition;
            OS = ln.OS;
            Brand = ln.Brand;
            HddSize = ln.HddSize;
            Ram = ln.Ram;
            ScreenSize = ln.ScreenSize;
            Processor = ln.Processor;
            Condition = ln.Condition;
        }
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "Used", Value = "used" } };
        public List<SelectListItem> brands = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "HP", Value = "hp" }, new SelectListItem { Text = "Toshiba", Value = "toshiba" }, new SelectListItem { Text = "Dell", Value = "dell" }, new SelectListItem { Text = "Lenovo", Value = "lenovo" }, new SelectListItem { Text = "Asus", Value = "asus" }, new SelectListItem { Text = "Acer", Value = "acer" }, new SelectListItem { Text = "Sony", Value = "sony" }, new SelectListItem { Text = "Alienware", Value = "alienware" }, new SelectListItem { Text = "Samsung", Value = "samsung" } };
        public List<SelectListItem> oses = new List<SelectListItem> {new SelectListItem { Text = "Mac", Value = "mac" }, new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Windows", Value = "windows" }, new SelectListItem { Text = "Linux", Value = "linux" }, new SelectListItem { Text = "Nix", Value = "nix" }, new SelectListItem { Text = "Other", Value = "other" } };
        string _view = "computers.cshtml";
        public override void EditListing()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();
            var lnQ = from c in db.Computers.Include("Listing")
                       where c.ListingId == this.ListingId && c.Listing.OwnerId == user
                       select c;
            Computer  ln = lnQ.FirstOrDefault();
            ln.Listing.Title = Title;
            ln.Listing.Description = Description;
            ln.Listing.Price = Price;
            ln.Listing.Town = Town;
            ln.Listing.Status = Status;
            ln.Listing.Updated = DateTime.Now;
            ln.Condition = Condition;
            ln.Brand = Brand;
            ln.HddSize = HddSize;
            ln.OS = OS;
            ln.Processor = Processor;
            ln.Ram = Ram;
            ln.ScreenSize = ScreenSize;
            // other changed properties
            db.SaveChanges();
            
        }
        public override string view { get { return this._view; } set { this._view = value; } }
        public string OS { get; set; }
        public string Brand { get; set; }
        public decimal HddSize { get; set; }
        public decimal Ram { get; set; }
        public decimal ScreenSize { get; set; }
        public string Processor { get; set; }
        public string Condition { get; set; }
    }
}