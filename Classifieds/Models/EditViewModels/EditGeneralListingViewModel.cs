using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.EditViewModels
{
    public class EditGeneralListingViewModel : AEditListingViewModel
    {
        public EditGeneralListingViewModel():base() { }
        
        public EditGeneralListingViewModel(Listing l):base(l) {
            var lQ = from c in db.GeneralListings.Include("Listing").Include("Listing.Category.Parent")
                     where c.ListingId == l.ListingId
                     select c;
            var ln = lQ.FirstOrDefault();
            if (ln.Listing.Category.Parent.Title.ToLower().Contains("service"))
                showCondition = false;
            Condition = ln.Condition;
            Brand = ln.Brand;
        }
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "So so", Value = "so so" }, new SelectListItem { Text = "Old", Value = "old" } };
        
        string _view = "general.cshtml";
        public override void EditListing()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();
            var lnQ = from c in db.GeneralListings.Include("Listing")
                       where c.ListingId == this.ListingId && c.Listing.OwnerId == user
                       select c;
            GeneralListing  ln = lnQ.FirstOrDefault();
            ln.Listing.Title = Title;
            ln.Listing.Description = Description;
            ln.Listing.Price = Price;
            ln.Listing.Town = Town;
            ln.Listing.Status = Status;
            ln.Listing.Updated = DateTime.Now;
            ln.Brand = Brand;
            ln.Condition = Condition;
            // other changed properties
            db.SaveChanges();
            
        }
        public override string view { get { return this._view; } set { this._view = value; } }
        public string Brand { get; set; }
        public string Condition { get; set; }

    }
}