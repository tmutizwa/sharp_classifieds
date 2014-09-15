using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class AutosViewModel : AListingViewModel
    {
        public AutosViewModel():base() { }
        public AutosViewModel(Listing ln) : base(ln) {
            var motaQ = from l in db.Motas.Include("Listing.Category")
                        where l.ListingId == ln.ListingId
                        select l;
            Mota car = motaQ.FirstOrDefault();
            if (car != null && ln.BulkUploaded != 1)
            {
                Make = car.Make;
                CarModel = car.CarModel;
                Year = car.Year;
                Mileage = car.Mileage;
                FuelType = car.FuelType;
                Transmission = car.Transmission;
                Condition = car.Condition;
                BodyType = car.BodyType;
                EngineSize = car.EngineSize;
            }
        }
        private string _view = "autos.cshtml";
        public override string view { get { return this._view; } set{} }
        [Required]
        public string Make { get; set; }
        [Required, Display(Name = "Model")]
        public string CarModel { get; set; }
        public string Year { get; set; }
        public decimal Mileage { get; set; }
        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }
        [Display(Name = "Transmission / Gearbox")]
        public string Transmission { get; set; }
        public string Condition { get; set; }
        [Display(Name = "Body Type")]
        public string BodyType { get; set; }
        public decimal EngineSize { get; set; }

    }
}