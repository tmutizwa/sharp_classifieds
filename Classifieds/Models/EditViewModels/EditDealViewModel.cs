using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Classifieds.Models.EditViewModels
{
    public class EditDealViewModel : IValidatableObject
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public EditDealViewModel(){}
        public EditDealViewModel(Deal deal)
        {
            this.DealId = deal.DealId;
            this.Listing = deal.Listing;
            this.ListingId = deal.ListingId;
            this.PriceScore = deal.PriceScore;
            this.OutreachScore = deal.OutreachScore;
            this.QualityScore = deal.QualityScore;
            this.Duration = deal.Duration;
            this.DurationScore = deal.DurationScore;
            this.BulkBuyingScore = deal.BulkBuyingScore;
            this.Starts = deal.Starts;
        }
        [Required]
        public int DealId {get; set;}
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
        [Required]
        [Range(1, 10)]
        public int PriceScore { get; set; }
        [Required]
        [Range(1, 10)]
        public int OutreachScore { get; set; }
        [Required]
        [Range(1, 10)]
        public int QualityScore { get; set; }
        [Required]
        [Range(1, 10)]
        public int DurationScore { get; set; }
        [Required]
        [Range(1, 10)]
        public int BulkBuyingScore { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-y H:mm}")]
        public DateTime? Starts { get; set; }
        [Range(1, 30)]
        public int Duration { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var now = DateTime.Now;
            var lQ = from l in db.Listings
                     where l.ListingId == ListingId && l.Status.ToLower() == "live" && l.Expires > now
                     select l;
            var ln = lQ.FirstOrDefault();
            if (ln == null)
            {
                yield return new ValidationResult("Listing with id " + ListingId + " either does not exist, is expired or has been suspended by user.", new string[] { "ListingId" });
            }
        }
    }
}