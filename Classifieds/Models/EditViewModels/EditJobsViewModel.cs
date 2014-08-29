using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.EditViewModels
{
    public class EditJobsViewModel : AEditListingViewModel
    {
        public EditJobsViewModel():base() { }
        
        public EditJobsViewModel(Listing l):base(l) {
            var lQ = from c in db.Jobs.Include("Listing")
                     where c.ListingId == l.ListingId
                     select c;
            var ln = lQ.FirstOrDefault();
            MinAge = ln.MinAge;
            MaxAge = ln.MaxAge;
            MinSalary = ln.MinSalary;
            MaxSalary = ln.MaxSalary;
            Type = ln.Type;
            Tags = ln.Tags;
        }
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Full-time", Value = "full-time" }, new SelectListItem { Text = "Contract", Value = "contract" }, new SelectListItem { Text = "Part-time", Value = "part-time" }, new SelectListItem { Text = "Temporary", Value = "temporary" }, new SelectListItem { Text = "Graduate", Value = "graduate" }, new SelectListItem { Text = "Internship", Value = "internship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Voluntary", Value = "voluntary" } };
        
        string _view = "jobs.cshtml";
        public override void EditListing()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();
            var lnQ = from c in db.Jobs.Include("Listing")
                       where c.ListingId == this.ListingId && c.Listing.OwnerId == user
                       select c;
            Job  ln = lnQ.FirstOrDefault();
            ln.Listing.Title = Title;
            ln.Listing.Description = Description;
            ln.Listing.Price = Price;
            ln.Listing.Town = Town;
            ln.Listing.Status = Status;
            ln.Listing.Updated = DateTime.Now;
            ln.MinAge = MinAge;
            ln.MaxAge = MaxAge;
            ln.MinSalary = MinSalary;
            ln.MaxSalary = MaxSalary;
            ln.Tags = Tags;
            ln.Type = Type;
            // other changed properties
            db.SaveChanges();
            
        }
        public override string view { get { return this._view; } set { this._view = value; } }
        [Display(Name = "Min. Age")]
        public int MinAge { get; set; }
        [Display(Name = "Max. Age")]
        public int MaxAge { get; set; }
        public string Tags { get; set; }
        [Display(Name = "Min. Salary")]
        public int MinSalary { get; set; }
        [Display(Name = "Max. Salary")]
        public int MaxSalary { get; set; }
        public string Type { get; set; }

    }
}