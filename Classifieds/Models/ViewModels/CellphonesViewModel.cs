using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class CellphonesViewModel: AListingViewModel
    {
        public CellphonesViewModel():base() { }
        public CellphonesViewModel(Listing ln) : base(ln) {
            var cellQ = from l in db.Cellphones.Include("Listing.Category")
                        where l.ListingId == ln.ListingId
                        select l;
            Cellphone cell = cellQ.FirstOrDefault();
            OS = cell.OS;
            Brand = cell.Brand;
            CModel = cell.CModel;
            ScreenSize = cell.ScreenSize;
            NetworkType = cell.NetworkType;
            Condition = cell.Condition;
        }
        private string _view = "cellphones.cshtml";
        public override string view { get { return this._view; } set{} }
        public string OS { get; set; }
        public string Brand { get; set; }
        [Display(Name="Model")]
        public string CModel { get; set; }
        public decimal ScreenSize { get; set; }
        [Display(Name = "Sim/Network type")]
        public string NetworkType { get; set; }
        public string Condition { get; set; }

    }
}