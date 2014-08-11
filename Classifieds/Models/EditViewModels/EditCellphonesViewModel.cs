using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
namespace Classifieds.Models.EditViewModels
{
    public class EditCellphonesViewModel : AEditListingViewModel
    {
        public EditCellphonesViewModel():base() { }
        string user = HttpContext.Current.User.Identity.GetUserId();
        public EditCellphonesViewModel(Listing l):base(l) {
            var lQ = from c in db.Cellphones.Include("Listing")
                     where c.ListingId == l.ListingId && c.Listing.OwnerId == user
                     select c;
            var ln = lQ.FirstOrDefault();
            OS = ln.OS;
            Brand = ln.Brand;
            CModel = ln.CModel;
            ScreenSize = ln.ScreenSize;
            NetworkType = ln.NetworkType;
            Condition = ln.Condition;

        }
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Brand new boxed", Value = "brand new boxed" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "As new", Value = "as new" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Fair", Value = "fair" }, new SelectListItem { Text = "Used", Value = "used" } };
        public List<SelectListItem> oses = new List<SelectListItem> { new SelectListItem { Text = "Windows", Value = "windows" }, new SelectListItem { Text = "Android", Value = "android" }, new SelectListItem { Text = "IOs / Iphone", Value = "ios" }, new SelectListItem { Text = "Blackberry", Value = "blackberry" }, new SelectListItem { Text = "Other", Value = "other" } };
        // public List<SelectListItem> warranties = new List<SelectListItem> { new SelectListItem { Text="Yes",Value=""},new SelectListItem { Text="Automatic",Value="Automatic"},new SelectListItem{Text="Manual",Value="Manual"}};
        public List<SelectListItem> networks = new List<SelectListItem> { new SelectListItem { Text = "GSM", Value = "gsm" }, new SelectListItem { Text = "CDMA", Value = "cdma" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> brands = new List<SelectListItem> { new SelectListItem { Text = "Samsung", Value = "samsung" }, new SelectListItem { Text = "HTC", Value = "htc" }, new SelectListItem { Text = "Sony", Value = "sony" }, new SelectListItem { Text = "Iphone", Value = "iphone" }, new SelectListItem { Text = "Nokia", Value = "nokia" }, new SelectListItem { Text = "LG", Value = "lg" }, new SelectListItem { Text = "Other", Value = "other" } };
        string _view = "cellphones.cshtml";
        public override void EditListing()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();
            var carQ = from c in db.Cellphones.Include("Listing")
                       where c.ListingId == this.ListingId && c.Listing.OwnerId == user
                       select c;
            Cellphone cell = carQ.FirstOrDefault();
            cell.Listing.Title = Title;
            cell.Listing.Description =  Description;
            cell.Listing.Price =  Price;
            cell.Listing.Town =  Town;
            cell.Listing.Updated = DateTime.Now;
            cell.Listing.Status = Status;
            cell.OS = OS;
            cell.Brand = Brand;
            cell.CModel = CModel;
            cell.ScreenSize = ScreenSize;
            cell.NetworkType = NetworkType;
            cell.Condition = Condition;
            db.SaveChanges();
        }
        public override string view { get { return this._view; } set { this._view = value; } }
        public string OS { get; set; }
        public string Brand { get; set; }
        [Display(Name="Model")]
        public string CModel { get; set; }
        public decimal ScreenSize { get; set; }
        [Display(Name = "Network type")]
        public string NetworkType { get; set; }
        public string Condition { get; set; }

    }
}