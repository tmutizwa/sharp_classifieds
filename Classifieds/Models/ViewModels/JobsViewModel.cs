using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classifieds.Models.ViewModels
{
    public class JobsViewModel : AListingViewModel
    {
        public JobsViewModel():base() { }
        public JobsViewModel(Listing ln) : base(ln) {
            var jobQ = from l in db.Jobs.Include("Listing.Category")
                        where l.ListingId == ln.ListingId
                        select l;
            Job job = jobQ.FirstOrDefault();
            MinAge = job.MinAge;
            MaxAge = job.MaxAge;
            MinSalary = job.MinSalary;
            MaxSalary = job.MaxSalary;
            Tags = job.Tags;
            Type = job.Type;
        }
        private string _view = "jobs.cshtml";
        public override string view { get { return this._view; } set{} }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Tags { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string Type { get; set; }

    }
}