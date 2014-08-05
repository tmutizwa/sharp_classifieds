using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateGeneralListingViewModel : ACreateListingViewModel
    {
        public CreateGeneralListingViewModel(int catId) : base(catId) { }
        public override int AddNewListing()
        {
            
            GeneralListing ln = new GeneralListing();
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
            ln.Brand = Brand;
            db.GeneralListings.Add(ln);
            db.SaveChanges();
            return ln.Listing.ListingId;
        }
        string _view = "general.cshtml";
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "So so", Value = "so so" }, new SelectListItem { Text = "Old", Value = "old" } };
        public override string view { get { return this._view; } set { this._view = value; } }
        public string Brand { get; set; }
        public string Condition { get; set; }
    }
}