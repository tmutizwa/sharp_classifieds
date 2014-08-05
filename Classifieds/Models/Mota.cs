using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Mota
    {
        public Mota() { }
        public int MotaId { get; set; }
        public Listing Listing { get; set; }
        public int ListingId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }
        [Required]
        [Display(Name = "Model")]
        [MaxLength(50)]
        public string CarModel { get; set; }
        [MaxLength(4)]
        public string Year { get; set; }
        public decimal Mileage { get; set; }
        [MaxLength(50)]
        public string FuelType { get; set; }
        [MaxLength(50)]
        public string Transmission { get; set; }
        [MaxLength(50)]
        public string Condition { get; set; }
        [MaxLength(50)]
        public string BodyType { get; set; }
        public decimal EngineSize { get; set; }
    }
}