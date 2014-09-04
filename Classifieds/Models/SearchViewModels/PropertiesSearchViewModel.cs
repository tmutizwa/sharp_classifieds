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
    public class PropertiesSearchViewModel  : AListingSearchModel 
    {
        public PropertiesSearchViewModel() : base() {}
        public PropertiesSearchViewModel(Category ct) : base(ct) { }
        string _controller = "browse";
        string _action = "index";
        string _view = "~/Views/Browse/properties.cshtml";
        string _layout = "~/Views/Shared/Layouts/_BrowseCategory.cshtml";
        string _searchWidgetView = "~/Views/Shared/Widgets/Search/_PropertiesSearch.cshtml";
        public List<SelectListItem> powers = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "ZESA - reliable", Value = "zesa-reliable" }, new SelectListItem { Text = "ZESA - unreliable", Value = "zesa-unreliable" }, new SelectListItem { Text = "Solar", Value = "solar" }, new SelectListItem { Text = "Other", Value = "other" } };
        private PagedList.IPagedList<Property> _foundListings = null;
        
        
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
            var listingsQ = from l in db.Properties.Include("Listing.Category").Include("Listing.images").Include("Listing.Owner")
                            where  l.Listing.Title.ToLower().Contains(text) || l.Listing.Description.ToLower().Contains(text) || l.Listing.Category.Description.ToLower().Contains(text) || l.Listing.Category.Title.ToLower().Contains(text) || l.Listing.Category.Tags.ToLower().Contains(text)
                            select l;
            listingsQ = listingsQ.Where(c => c.Listing.Expires > DateTime.Now && c.Listing.Status == "live" && subCategoryIds.Contains(c.Listing.CategoryId));
           if (this.from > 0){
               listingsQ = listingsQ.Where(l=>l.Listing.Price >= this.from);
           }
           if (this.to > 0)
           {
               listingsQ = listingsQ.Where(l=>l.Listing.Price <= this.to);
           }
           if (!String.IsNullOrEmpty(this.location))
           {
               listingsQ = listingsQ.Where(c=>c.Listing.Town.ToLower().Contains(this.location.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Power))
           {
               listingsQ = listingsQ.Where(c=>c.Power.ToLower().Contains(this.Power.ToLower()));
           }
           if (this.MinBedrooms > 0)
           {
               listingsQ = listingsQ.Where(c => c.Bedrooms >= MinBedrooms);
           }
           if (this.MinBathrooms > 0)
           {
               listingsQ = listingsQ.Where(c => c.Bathrooms >= this.MinBathrooms );
           }
           if (this.MinLandArea > 0)
           {
               listingsQ = listingsQ.Where(c => c.LandArea >= this.MinLandArea);
           }
           if (this.MinBuildingArea > 0)
           {
               listingsQ = listingsQ.Where(c => c.LandArea >= this.MinBuildingArea);
           }
           if (!String.IsNullOrEmpty(this.Suburb))
           {
               listingsQ = listingsQ.Where(c => c.Suburb.ToLower().Contains(this.Suburb.ToLower()));
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
               case "price":
                   listingsQ = listingsQ.OrderBy(c=>c.Listing.Price);
                   this.porder = "price_desc";
                   break;
               case "price_desc":
                   listingsQ = listingsQ.OrderByDescending(c=>c.Listing.Price);
                   this.porder = "price";
                   break;
               default :
                    listingsQ = listingsQ.OrderByDescending(c=>c.Listing.Updated).ThenByDescending(c=>c.Listing.images.Count);
                    this.porder = "price";
                    this.lorder = "loc";
                   break;
           }
           
           int page = 1;
           if (this.page > 0)
               page = this.page;
           this.foundListings = listingsQ.Take(200).ToPagedList(page, this.pagesize);
        }
        public PagedList.IPagedList<Property> foundListings { get { return this._foundListings; } set { this._foundListings = value; } }
        public string Suburb { get; set; }
        [Display(Name = "Min. Bedrooms")]
        public int MinBedrooms { get; set; }
        [Display(Name = "Min. Bathrooms")]
        public int MinBathrooms { get; set; }
        [Display(Name = "Min. Car ports")]
        public int MinGarages { get; set; }
        public string Power { get; set; }
        [Display(Name="Min. Land area")]
        public decimal MinLandArea { get; set; }
        [Display(Name="Min. Living area")]
        public decimal MinBuildingArea { get; set; }
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