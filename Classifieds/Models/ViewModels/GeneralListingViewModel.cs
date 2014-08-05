using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.ViewModels
{
    public class GeneralListingViewModel : AListingViewModel
    {
        public List<SelectListItem> fuels = new List<SelectListItem> { new SelectListItem { Text = "Choose" }, new SelectListItem { Text = "Petrol", Value = "Petrol" }, new SelectListItem { Text = "Diesel", Value = "Diesel" }, new SelectListItem { Text = "Electric", Value = "Electric" } };
        public List<SelectListItem> transmissions = new List<SelectListItem> { new SelectListItem { Text = "Choose" }, new SelectListItem { Text = "Automatic", Value = "Automatic" }, new SelectListItem { Text = "Manual", Value = "Manual" } };
        public GeneralListingViewModel():base() { }
        public GeneralListingViewModel(Listing ln):base(ln)
        {
            var gQ = from l in db.GeneralListings.Include("Listing.Category")
                     where l.ListingId == ln.ListingId
                     select l;
            GeneralListing listing = gQ.FirstOrDefault();
            Brand = listing.Brand;
            Condition = listing.Condition;

        }
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "So so", Value = "so so" }, new SelectListItem { Text = "Old", Value = "old" } };
        string _view = "general.cshtml";
        public override string view { get { return this._view; } set { this._view = value; } }
        public string Brand { get; set; }
        public string Condition { get; set; }

    }
}