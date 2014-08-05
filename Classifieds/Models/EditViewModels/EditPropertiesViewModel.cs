using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.EditViewModels
{
    public class EditPropertiesViewModel : AEditListingViewModel
    {
        public EditPropertiesViewModel():base() { }
        
        public EditPropertiesViewModel(Listing l):base(l) {
            var lQ = from c in db.Properties.Include("Listing")
                     where c.ListingId == l.ListingId
                     select c;
            var ln = lQ.FirstOrDefault();
            Bedrooms = ln.Bedrooms;
            Bathrooms = ln.Bathrooms;
            Toilets = ln.Toilets;
            Garages = ln.Garages;
            LandArea = ln.LandArea;
            BuildingArea = BuildingArea;
            Power = ln.Power;
            Suburb = ln.Suburb;
            Boreholes = ln.Boreholes;
        }
        public List<SelectListItem> powers = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "ZESA - reliable", Value = "zesa-reliable" }, new SelectListItem { Text = "ZESA - unreliable", Value = "zesa-unreliable" }, new SelectListItem { Text = "Part-time", Value = "part-time" }, new SelectListItem { Text = "Solar", Value = "solar" }, new SelectListItem { Text = "Other", Value = "other" } };
        
        public override void EditListing()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();
            var lnQ = from c in db.Properties.Include("Listing")
                       where c.ListingId == this.ListingId && c.Listing.OwnerId == user
                       select c;
            Property  ln = lnQ.FirstOrDefault();
            ln.Listing.Title = Title;
            ln.Listing.Description = Description;
            ln.Listing.Price = Price;
            ln.Listing.Town = Town;
            ln.Listing.Status = Status;
            ln.Listing.Updated = DateTime.Now;
            ln.Bedrooms = Bedrooms;
            ln.Bathrooms = Bathrooms;
            ln.Suburb = Suburb;
            ln.Power = Power;
            ln.BuildingArea = BuildingArea;
            ln.LandArea = LandArea;
            ln.Boreholes = Boreholes;
            ln.Garages = Garages;
            // other changed properties
            db.SaveChanges();
            
        }
        string _view = "properties.cshtml";
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