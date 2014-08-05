using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateAutosViewModel : ACreateListingViewModel
    {
        public CreateAutosViewModel(int categoryId) : base(categoryId) { }
        public override int AddNewListing()
        {
            
            Mota car = new Mota();
            Listing listing = new Listing();
            //moths to expire this ad
            int months = (Category.ExpiresIn > 0) ? Category.ExpiresIn : 3;
            car.Listing = listing;
            car.Listing.Title = Title;
            car.Listing.CategoryId = CategoryId;
            car.Listing.Description =  Description;
            car.Listing.Price =  Price;
            car.Listing.Town =  Town;
            car.Listing.Created = DateTime.Now;
            car.Listing.Updated = DateTime.Now;
            car.Listing.Status  = "live";
            car.Listing.OwnerId = HttpContext.Current.User.Identity.GetUserId();
            car.Listing.Expires = DateTime.Now.AddMonths(months);
            car.BodyType = BodyType;
            car.CarModel = CarModel;
            car.Condition = Condition;
            car.EngineSize = EngineSize;
            car.FuelType = FuelType;
            car.Make = Make;
            car.Mileage = Mileage;
            car.Transmission = Transmission;
            car.Year = Year;
            db.Motas.Add(car);
            db.SaveChanges();
            return car.Listing.ListingId;
        }
        List<SelectListItem> _fuels = new List<SelectListItem> {new SelectListItem{Text="Choose",Value=""},new SelectListItem{Text="Petrol",Value="petrol"},new SelectListItem{Text="Diesel",Value="diesel"},new SelectListItem{Text="Electric",Value="electricity"},new SelectListItem{Text = "Other",Value="other"} };
        List<SelectListItem> _transmissions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Automatic", Value = "automatic" }, new SelectListItem { Text = "Manual", Value = "manual" }, new SelectListItem { Text = "Other", Value = "other" } };
        List<SelectListItem> _conditions = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "So so", Value = "so so" }, new SelectListItem { Text = "Old", Value = "old" } };
        string _view = "autos.cshtml";
        public override string view { get { return this._view; } set { this._view = value; } }
        [Required]
        public string Make { get; set; }
        [Required, Display(Name = "Model")]
        public string CarModel { get; set; }
        public string Year { get; set; }
        public decimal Mileage { get; set; }
        [Display(Name = "Fuel Type")]
        [Required]
        public string FuelType { get; set; }
        [Display(Name = "Transmission / Gearbox")]
        [Required]
        public string Transmission { get; set; }
        [Required]
        public string Condition { get; set; }
        [Display(Name = "Body Type")]
        public string BodyType { get; set; }
        public decimal EngineSize { get; set; }
        public List<SelectListItem> fuelTypes { get { return this._fuels; } set { this._fuels = value; } }
        public List<SelectListItem> transmissionTypes { get { return this._transmissions; } set { this._transmissions = value; } }
        public List<SelectListItem> conditionTypes { get { return this._conditions; } set { this._conditions = value; } }
    }
}