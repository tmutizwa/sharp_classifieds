using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.Models.CreateViewModels
{
    public class CreateDatingViewModel : ACreateListingViewModel
    {
        public CreateDatingViewModel(int catId) : base(catId) { showPrice = false; }
        public override int AddNewListing()
        {
            
            Dating ln = new Dating();
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

            ln.Age = Age;
            ln.Sex = Sex;
            ln.Drink = Drink;
            ln.Ethnicity = Ethnicity;
            ln.Height = Height;
            ln.Interests = Interests;
            ln.Nationality = Nationality;
            ln.Occupation = Occupation;
            ln.Religion = Religion;
            ln.Smoke = Smoke;
            ln.Type = Type;
            
            db.Dates.Add(ln);
            db.SaveChanges();
            return ln.Listing.ListingId;
        }
        string _view = "dating.cshtml";
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Love", Value = "love" }, new SelectListItem { Text = "Relationship", Value = "relationship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Friendship", Value = "friendship" }, new SelectListItem { Text = "Life partner", Value = "life-partner" }, new SelectListItem { Text = "Anything", Value = "anything" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> sexes = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Male", Value = "male" }, new SelectListItem { Text = "Female", Value = "female" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> yesno = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Yes", Value = "yes" }, new SelectListItem { Text = "No", Value = "no" }};
        public override string view { get { return this._view; } set { this._view = value; } }
        [Display(Name="You're looking for")]
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
        [Display(Name="Do you smoke?")]
        public string Smoke { get; set; }
        [Display(Name="Do you drink?")]
        public string Drink { get; set; }
    }
}