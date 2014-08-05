using Classifieds.Library;
using Classifieds.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Motor
    {
        private Listing _listing = new Listing();
        public Motor(){ }
       
        public int MotorId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get { return this._listing; } set { this._listing = value; } }
        [Required]
        public string Make { get; set; }
        [Required]
        [Display(Name = "Model")]
        public string CarModel { get; set; }
        public string Year { get; set; }
        public decimal Mileage { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Condition { get; set; }
        public string BodyType { get; set; }
        public decimal EngineSize { get; set; }
        public string Steering { get; set; }
    }
}