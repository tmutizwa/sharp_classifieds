using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.EditViewModels
{
    public class EditDatingViewModel : AEditListingViewModel
    {
        public EditDatingViewModel() : base() { showPrice = false; }
        
        public EditDatingViewModel(Listing l):base(l) {
            var lQ = from c in db.Dates.Include("Listing").Include("Listing.Category.Parent")
                     where c.ListingId == l.ListingId
                     select c;
            var ln = lQ.FirstOrDefault();
            if (ln.Listing.Category.Parent.Title.ToLower().Contains("service"))
                showCondition = false;
            Type = ln.Type;
            Age = ln.Age;
            Sex = ln.Sex;
            Drink = ln.Drink;
            Ethnicity =  ln.Ethnicity;
            Height = ln.Height;
            Interests = ln.Interests;
            Nationality = ln.Nationality;
            Occupation = ln.Occupation;
            Religion = ln.Religion;
            Smoke = ln.Smoke;

        }
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Love", Value = "love" }, new SelectListItem { Text = "Relationship", Value = "relationship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Friendship", Value = "friendship" }, new SelectListItem { Text = "Life partner", Value = "life-partner" }, new SelectListItem { Text = "Anything", Value = "anything" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> sexes = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Male", Value = "male" }, new SelectListItem { Text = "Female", Value = "female" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> yesno = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Yes", Value = "yes" }, new SelectListItem { Text = "No", Value = "no" }};
        string _view = "dating.cshtml";
        public override void EditListing()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();
            var lnQ = from c in db.Dates.Include("Listing")
                       where c.ListingId == this.ListingId && c.Listing.OwnerId == user
                       select c;
            Dating  ln = lnQ.FirstOrDefault();
            ln.Listing.Title = Title;
            ln.Listing.Description = Description;
            ln.Listing.Price = Price;
            ln.Listing.Town = Town;
            ln.Listing.Status = Status;
            ln.Listing.Updated = DateTime.Now;
            ln.Age = Age;
            ln.Drink = Drink;
            ln.Ethnicity = Ethnicity;
            ln.Height = Height;
            ln.Interests = Interests;
            ln.Nationality = Nationality;
            ln.Occupation = Occupation;
            ln.Religion = Religion;
            ln.Sex = Sex;
            ln.Smoke = Smoke;
            ln.Type = Type;
            
            // other changed properties
            db.SaveChanges();
            
        }
        public override string view { get { return this._view; } set { this._view = value; } }
        [Display(Name = "You're looking for")]
        [Required]
        public string Type { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Interests { get; set; }
        public string Religion { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public decimal Height { get; set; }
        public string Ethnicity { get; set; }
        [Display(Name = "Do you smoke?")]
        public string Smoke { get; set; }
        [Display(Name = "Do you drink?")]
        public string Drink { get; set; }
    }
}