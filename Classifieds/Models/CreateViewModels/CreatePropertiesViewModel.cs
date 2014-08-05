using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Classifieds.Models.CreateViewModels
{
    public class CreatePropertiesViewModel : ACreateListingViewModel
    {
        public CreatePropertiesViewModel(int catId) : base(catId) { }
        public override int AddNewListing()
        {
            
            Property ln = new Property();
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
            ln.Suburb = Suburb;
            ln.Bedrooms = Bedrooms;
            ln.Bathrooms = Bathrooms;
            ln.Toilets = Toilets;
            ln.Garages = Garages;
            ln.BuildingArea = BuildingArea;
            ln.LandArea = LandArea;
            ln.Power = Power;
            ln.Boreholes = Boreholes;
            db.Properties.Add(ln);
            db.SaveChanges();
            return ln.Listing.ListingId;
        }
        string _view = "properties.cshtml";
        public List<SelectListItem> powers = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "ZESA - reliable", Value = "zesa-reliable" }, new SelectListItem { Text = "ZESA - unreliable", Value = "zesa-unreliable" }, new SelectListItem { Text = "Part-time", Value = "part-time" }, new SelectListItem { Text = "Solar", Value = "solar" }, new SelectListItem { Text = "Other", Value = "other" } };
        public override string view { get { return this._view; } set { this._view = value; } }
        public string Suburb { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Toilets { get; set; }
        public int Garages { get; set; }
        public int Boreholes { get; set; }
        public string Power { get; set; }
        public decimal LandArea { get; set; }
        public decimal BuildingArea { get; set; }
    }
}