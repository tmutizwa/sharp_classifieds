using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Classifieds.Models.ViewModels;
using Classifieds.Models.SearchViewModels;

namespace Classifieds.Models.SearchViewModels
{
    public class ListingSearchViewModel : AListingSearchModel 
    {
        string _layout = "~/Views/Shared/Layouts/_BrowseCategory.cshtml";
        string _view = "~/Views/Browse/index.cshtml";
        string _controller = "browse";
        string _action = "index";
        string _searchWidgetView = "~/Views/Shared/Widgets/Search/_BrowseCategorySearch.cshtml";
        public ListingSearchViewModel() : base() {  }
        public ListingSearchViewModel(Category ct) : base(ct) { }
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
            var allSubs = catHelper.allSubCategories(cat);
            int[] allSubCategoryIds = new int[allSubs.Count()+1];
            //include the current searched category id
            allSubCategoryIds[0] = cat;
            int i = 0;
            foreach (var item in allSubs )
            {
                allSubCategoryIds[i] = item.CategoryId;
                i++;
            }
            //js incase it's not submitted
            string text = "";
            List<Listing> listings = new List<Listing>();
            if(!String.IsNullOrEmpty(this.text))
               text = this.text.ToLower().Trim();

            var listingsQ = from l in db.Listings.Include("Category").Include("images").Include("Owner")
                            where l.Title.ToLower().Contains(text) || l.Description.ToLower().Contains(text) || l.Category.Description.ToLower().Contains(text) || l.Category.Title.ToLower().Contains(text) || l.Category.Tags.ToLower().Contains(text)
                            select l;
           listingsQ = listingsQ.Where(c=>c.Expires > DateTime.Now && c.Status=="live");
           if (cat > 0)
               listingsQ = listingsQ.Where(c => allSubCategoryIds.Contains(c.CategoryId));
           if (this.from > 0)
           {
               listingsQ = listingsQ.Where(l=>l.Price >= this.from && l.Price > 0);
           }
           if (this.to > 0)
           {
               listingsQ = listingsQ.Where(l=>l.Price <= this.to && l.Price > 0);
           }
           if (!String.IsNullOrEmpty(this.location))
           {
               listingsQ = listingsQ.Where(c=>c.Town.ToLower().Contains(this.location.ToLower()));
           }
            //set defaults
           this.lorder = "loc";
           this.porder = "price";
           switch(this.order){
               case "loc":
                   listingsQ = listingsQ.OrderBy(c=>c.Town);
                   this.lorder = "loc_desc";
                   break;
               case "loc_desc":
                   listingsQ = listingsQ.OrderByDescending(c=>c.Town);
                   this.lorder = "loc";
                   break;
               case "price":
                   listingsQ = listingsQ.Where(c=>c.Price > 0).OrderBy(c=>c.Price);
                   this.porder = "price_desc";
                   break;
               case "price_desc":
                   listingsQ = listingsQ.Where(c => c.Price > 0).OrderByDescending(c => c.Price);
                   this.porder = "price";
                   break;
               default :
                    listingsQ = listingsQ.OrderByDescending(c=>c.Updated);
                    this.porder = "price";
                    this.lorder = "loc";
                   break;
           }
           
           int page = 1;
           if (this.page > 0)
               page = this.page;
           this.foundListings = listingsQ.ToPagedList(page, this.pagesize);
           
        }
        public PagedList.IPagedList<Listing> foundListings { get; set; }

        public override string controller { get { return _controller; } set { this._controller = value; } }
        public override string action { get { return this._action; } set { this._action = value; } }
        public override string layout
        {
            get
            {
                return _layout;
            }
            set
            {
                this._layout = value;
            }
        }
        public override string view
        {
            get
            {
                return _view;
            }
            set
            {
                this._view = value;
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