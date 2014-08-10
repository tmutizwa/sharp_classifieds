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
        public int DealId { get; set; }
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
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
        public int Votes { get; set; }
        public Decimal TotalScore { get; set; }
        public int Hits { get; set; }
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