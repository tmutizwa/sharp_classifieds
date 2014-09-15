using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class Deal
    {
        [Key,ForeignKey("Listing")]
        public int ListingId { get; set; }
        public virtual Listing Listing { get; set; }
        [Required]
        public int PriceScore { get; set; }
        [Required]
        public int OutreachScore { get; set; }
        [Required]
        public int QualityScore { get; set; }
        [Required]
        public int DurationScore { get; set; }
        [Required]
        public int BulkBuyingScore { get; set; }
        public string UpdaterId { get; set; }
        public ApplicationUser Updater { get; set; }
        public decimal TotalScore
        {
            get
            {
                return this.BulkBuyingScore + this.DurationScore + this.OutreachScore + this.PriceScore + this.QualityScore;
            }
            set
            {

            }
        }
        public int Votes { get; set; }
        public int Duration { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Starts { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Ends { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Updated { get; set; }
    }
}