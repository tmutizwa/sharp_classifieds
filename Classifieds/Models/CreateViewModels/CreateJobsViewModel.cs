using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateJobsViewModel : ACreateListingViewModel
    {
        public CreateJobsViewModel(int catId) : base(catId) {
            showPrice = false; 
        }
        public override int AddNewListing()
        {
            
            Job ln = new Job();
            Listing l = new Listing();
            //moths to expire this ad
            int months = (Category.ExpiresIn > 0) ? Category.ExpiresIn : 3;
            ln.Listing = l;
            ln.Listing.Title = Title;
            ln.Listing.CategoryId = CategoryId;
            ln.Listing.Description =  Description;
            ln.Listing.Price =  Price;
            ln.Listing.Town =  Town;
            ln.Listing.Created = DateTime.Now;
            ln.Listing.Updated = DateTime.Now;
            ln.Listing.Status  = "live";
            ln.Listing.OwnerId = HttpContext.Current.User.Identity.GetUserId();
            ln.Listing.Expires = DateTime.Now.AddMonths(months);
            ln.MaxAge = MaxAge;
            ln.MinAge = MinAge;
            ln.Tags = Tags;
            ln.MinSalary = MinSalary;
            ln.MaxSalary = MaxSalary;
            ln.Type = Type;
            db.Jobs.Add(ln);
            db.SaveChanges();
            return ln.Listing.ListingId;
        }
        string _view = "jobs.cshtml";
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Full-time", Value = "full-time" }, new SelectListItem { Text = "Contract", Value = "contract" }, new SelectListItem { Text = "Part-time", Value = "part-time" }, new SelectListItem { Text = "Temporary", Value = "temporary" }, new SelectListItem { Text = "Graduate", Value = "graduate" }, new SelectListItem { Text = "Internship", Value = "internship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Voluntary", Value = "voluntary" } };
        public override string view { get { return this._view; } set { this._view = value; } }
        [Display(Name="Min. Age")]
        public int MinAge { get; set; }
        [Display(Name="Max. Age")]
        public int MaxAge { get; set; }
        public string Tags { get; set; }
        [Display(Name="Min. Salary")]
        public int MinSalary { get; set; }
        [Display(Name="Max. Salary")]
        public int MaxSalary { get; set; }
        public string Type { get; set; }
        
    }
}