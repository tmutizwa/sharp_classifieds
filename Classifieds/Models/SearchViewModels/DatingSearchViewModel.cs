using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Classifieds.Models.ViewModels;
using Classifieds.Models.SearchViewModels;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;
using Classifieds.Areas.Admin.Models;

namespace Classifieds.Models.SearchViewModels
{
    public class DatingSearchViewModel  : AListingSearchModel 
    {
        string _controller = "browse";
        string _action = "index";
        string _view = "~/Views/Browse/dating.cshtml";
        string _layout = "~/Views/Shared/Layouts/_BrowseCategory.cshtml";
        string _searchWidgetView = "~/Views/Shared/Widgets/Search/_DatingSearch.cshtml";
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Love", Value = "love" }, new SelectListItem { Text = "Relationship", Value = "relationship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Friendship", Value = "friendship" }, new SelectListItem { Text = "Life partner", Value = "life-partner" }, new SelectListItem { Text = "Anything", Value = "anything" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> sexes = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Male", Value = "male" }, new SelectListItem { Text = "Female", Value = "female" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> yesno = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Yes", Value = "yes" }, new SelectListItem { Text = "No", Value = "no" } };
        private PagedList.IPagedList<Dating> _foundListings = null;
        public DatingSearchViewModel() : base() { }
        public DatingSearchViewModel(Category ct) : base(ct) { }
        
        public override void findListings()
        {
            //Category not being set by model binder so makign sure here it is
            this.Category = db.Categories.Find(cat);
            if (this.subCategories == null || cat == 0)
            {
                //they are searching all listings, no category(cat) specified or cat == 0
                this.subCategories = catHelper.subCategories(0);
            }
            else
            {
                this.subCategories = catHelper.subCategories(cat);
            }
            int[] subCategoryIds = new int[subCategories.Count() + 1];
            //include the current searched category id
            subCategoryIds[0] = cat;
            int i = 0;
            foreach (var item in subCategories)
            {
                subCategoryIds[i] = item.CategoryId;
                i++;
            }
            string text = "";
            List<Listing> listings = new List<Listing>();
            if (!String.IsNullOrEmpty(this.text))
                text = this.text.ToLower().Trim();
            var listingsQ = from l in db.Dates.Include("Listing.Category").Include("Listing.images").Include("Listing.Owner")
                            where  l.Listing.Title.ToLower().Contains(text) || l.Listing.Description.ToLower().Contains(text) || l.Listing.Category.Description.ToLower().Contains(text) || l.Listing.Category.Title.ToLower().Contains(text) || l.Listing.Category.Tags.ToLower().Contains(text)
                            select l;
            listingsQ = listingsQ.Where(c => c.Listing.Expires > DateTime.Now && c.Listing.Status == "live" && subCategoryIds.Contains(c.Listing.CategoryId));
           if (!String.IsNullOrEmpty(this.location))
           {
               listingsQ = listingsQ.Where(c=>c.Listing.Town.ToLower().Contains(this.location.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Religion))
           {
               listingsQ = listingsQ.Where(c=>c.Religion.ToLower().Contains(this.Religion.ToLower()));
           }
           if (MinAge > 0)
           {
               listingsQ = listingsQ.Where(c => c.Age >= MinAge);
           }
           if (MaxAge > 0)
           {
               listingsQ = listingsQ.Where(c => c.Age <= MaxAge);
           }
           if (MinHeight > 0)
           {
               listingsQ = listingsQ.Where(c => c.Height >= MinHeight);
           }
           if (!String.IsNullOrEmpty(this.Sex))
           {
               listingsQ = listingsQ.Where(c => c.Sex.ToLower().Contains(this.Sex.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Smoke))
           {
               listingsQ = listingsQ.Where(c => c.Smoke.ToLower().Contains(this.Smoke.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Drink))
           {
               listingsQ = listingsQ.Where(c => c.Drink.ToLower().Contains(this.Drink.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Ethnicity))
           {
               listingsQ = listingsQ.Where(c => c.Ethnicity.ToLower().Contains(this.Ethnicity.ToLower()));
           }
            //set defaults
           this.lorder = "loc";
           this.porder = "price";
           switch(this.order){
               case "loc":
                   listingsQ = listingsQ.OrderBy(c=>c.Listing.Town);
                   this.lorder = "loc_desc";
                   break;
               case "loc_desc":
                   listingsQ = listingsQ.OrderByDescending(c=>c.Listing.Town);
                   this.lorder = "loc";
                   break;
              
               default :
                    listingsQ = listingsQ.OrderByDescending(c=>c.Listing.Updated);
                    this.porder = "price";
                    this.lorder = "loc";
                   break;
           }
           
           int page = 1;
           if (this.page > 0)
               page = this.page;
           this.foundListings = listingsQ.ToPagedList(page, this.pagesize);
        }
        public PagedList.IPagedList<Dating> foundListings { get { return this._foundListings; } set { this._foundListings = value; } }
        [Display(Name="Rel. Type")]
        public string Type { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Sex { get; set; }
        public string Ethnicity { get; set; }
        public string Religion { get; set; }
        [Display(Name="Min height")]
        public decimal MinHeight { get; set; }
        [Display(Name="Person smokes?")]
        public string Smoke { get; set; }
        [Display(Name="Person drinks?")]
        public string Drink { get; set; }
        public override string controller { get { return this._controller; } set { this._controller = value; } }
        public override string action { get { return this._action; } set { this._action = value; } }
        public override string view
        {
            get
            {
                return this._view;
            }
            set
            {
                this._view = value;
            }
        }
        public override string layout
        {
            get
            {
                return this._layout;
            }
            set
            {
                this._layout = value;
            }
        }
        public override string searchWidgetView
        {
            get
            {
                return this._searchWidgetView;
            }
            set
            {
                this._searchWidgetView = value;
            }
        }
        }
}