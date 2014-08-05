using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class ComputersViewModel: AListingViewModel
    {
        public ComputersViewModel():base() { }
        public ComputersViewModel(Listing ln) : base(ln) {
            var compQ = from l in db.Computers.Include("Listing.Category")
                        where l.ListingId == ln.ListingId
                        select l;
            Computer comp = compQ.FirstOrDefault();
            OS = comp.OS;
            Brand = comp.Brand;
            HddSize = comp.HddSize;
            ScreenSize = comp.ScreenSize;
            Condition = comp.Condition;
            Ram = comp.Ram;
            ScreenSize = comp.ScreenSize;
            Processor = comp.Processor;
        }
        private string _view = "computers.cshtml";
        public override string view { get { return this._view; } set{} }
        public string OS { get; set; }
        public string Brand { get; set; }
        public decimal HddSize { get; set; }
        public decimal Ram { get; set; }
        public decimal ScreenSize { get; set; }
        public string Processor { get; set; }
        public string Condition { get; set; }

    }
}