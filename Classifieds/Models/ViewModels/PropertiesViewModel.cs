using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class PropertiesViewModel: AListingViewModel
    {
        public PropertiesViewModel():base() { }
        public PropertiesViewModel(Listing ln) : base(ln) {
            var compQ = from l in db.Properties.Include("Listing.Category")
                        where l.ListingId == ln.ListingId
                        select l;
            Property prop = compQ.FirstOrDefault();
            if (prop != null && ln.BulkUploaded != 1)
            {
                Bedrooms = prop.Bedrooms;
                Bathrooms = prop.Bathrooms;
                Garages = prop.Garages;
                Power = prop.Power;
                LandArea = prop.LandArea;
                BuildingArea = prop.BuildingArea;
                Toilets = prop.Toilets;
                Suburb = prop.Suburb;
                Boreholes = prop.Boreholes;
            }

        }
        private string _view = "properties.cshtml";
        public override string view { get { return this._view; } set{} }
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