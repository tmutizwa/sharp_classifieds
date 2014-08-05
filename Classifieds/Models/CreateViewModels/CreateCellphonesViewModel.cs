using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateCellphonesViewModel : ACreateListingViewModel
    {
       
        public CreateCellphonesViewModel(int categoryId) : base(categoryId) { }
        public override int AddNewListing()
        {
            
            Cellphone cell = new Cellphone();
            Listing listing = new Listing();
            //moths to expire this ad
            int months = (Category.ExpiresIn > 0) ? Category.ExpiresIn : 3;
            cell.Listing = listing;
            cell.Listing.Title = Title;
            cell.Listing.CategoryId = CategoryId;
            cell.Listing.Description =  Description;
            cell.Listing.Price =  Price;
            cell.Listing.Town =  Town;
            cell.Listing.Created = DateTime.Now;
            cell.Listing.Updated = DateTime.Now;
            cell.Listing.Status  = "live";
            cell.Listing.OwnerId = HttpContext.Current.User.Identity.GetUserId();
            cell.Listing.Expires = DateTime.Now.AddMonths(months);
            cell.OS = OS;
            cell.Brand = Brand;
            cell.CModel = CModel;
            cell.ScreenSize = ScreenSize;
            cell.NetworkType = NetworkType;
            cell.Condition = Condition;
            db.Cellphones.Add(cell);
            db.SaveChanges();
            return cell.Listing.ListingId;
        }
        public List<SelectListItem> conditions = new List<SelectListItem> {new SelectListItem{Text="Choose",Value=""}, new SelectListItem { Text = "Brand new boxed", Value = "brand new boxed" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "As new", Value = "as new" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Fair", Value = "fair" }, new SelectListItem { Text = "Used", Value = "used" } };
        public List<SelectListItem> oses = new List<SelectListItem> { new SelectListItem{Text="Choose",Value=""},new SelectListItem { Text = "Windows", Value = "windows" }, new SelectListItem { Text = "Android", Value = "android" }, new SelectListItem { Text = "IOs / Iphone", Value = "ios" }, new SelectListItem { Text = "Blackberry", Value = "blackberry" }, new SelectListItem { Text = "Other", Value = "other" } };
        // public List<SelectListItem> warranties = new List<SelectListItem> { new SelectListItem{Text="Choose",Value=""},new SelectListItem { Text="Yes",Value=""},new SelectListItem { Text="Automatic",Value="Automatic"},new SelectListItem{Text="Manual",Value="Manual"}};
        public List<SelectListItem> networks = new List<SelectListItem> {new SelectListItem{Text="Choose",Value=""}, new SelectListItem { Text = "GSM", Value = "gsm" }, new SelectListItem { Text = "CDMA", Value = "cdma" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> brands = new List<SelectListItem> { new SelectListItem{Text="Choose",Value=""},new SelectListItem { Text = "Samsung", Value = "samsung" }, new SelectListItem { Text = "HTC", Value = "htc" }, new SelectListItem { Text = "Sony", Value = "sony" }, new SelectListItem { Text = "Iphone", Value = "iphone" }, new SelectListItem { Text = "Nokia", Value = "nokia" }, new SelectListItem { Text = "LG", Value = "lg" }, new SelectListItem { Text = "Other", Value = "other" } };
        string _view = "cellphones.cshtml";
        public override string view { get { return this._view; } set { this._view = value; } }
        public string OS { get; set; }
        [Required]
        public string Brand { get; set; }
        [Display(Name="Model")]
        public string CModel { get; set; }
        public decimal ScreenSize { get; set; }
        [Display(Name = "Sim/Network type")]
        public string NetworkType { get; set; }
        [Required]
        public string Condition { get; set; }
    }
}