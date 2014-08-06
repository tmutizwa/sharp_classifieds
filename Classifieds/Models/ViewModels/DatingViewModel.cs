using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class DatingViewModel: AListingViewModel
    {
        public DatingViewModel():base() { }
        public DatingViewModel(Listing ls) : base(ls) {
            var compQ = from l in db.Dates.Include("Listing.Category")
                        where l.ListingId == ls.ListingId
                        select l;
            Dating ln = compQ.FirstOrDefault();
            Type = ln.Type;
            Age = ln.Age;
            Sex = ln.Sex;
            Interests = ln.Interests;
            Religion = ln.Religion;
            Occupation = ln.Occupation;
            Nationality = ln.Nationality;
            Weight = ln.Weight;
            Height = ln.Height;
            Ethnicity = ln.Ethnicity;
            Smoke = ln.Smoke;
            Drink = ln.Drink;
        }
        private string _view = "dating.cshtml";
        public override string view { get { return this._view; } set{} }
        [Display(Name="Rel. Type")]
        public string Type { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Interests { get; set; }
        public string Religion { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Ethnicity { get; set; }
        public string Smoke { get; set; }
        public string Drink { get; set; }
    }
}