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
    public class CellphonesSearchViewModel  : AListingSearchModel 
    {
        string _controller = "browse";
        string _action = "index";
        string _view = "~/Views/Browse/cellphones.cshtml";
        string _layout = "~/Views/Shared/Layouts/_BrowseCategory.cshtml";
        string _searchWidgetView = "~/Views/Shared/Widgets/Search/_CellphonesSearch.cshtml";
        public List<SelectListItem> conditions = new List<SelectListItem> {new SelectListItem { Text="Choose",Value=""},new SelectListItem { Text="Brand new boxed",Value="brand new boxed"},new SelectListItem { Text="Brand new",Value="brand new"},new SelectListItem{Text="As new",Value="as new"},new SelectListItem{Text="Good",Value="good"},new SelectListItem{Text="Fair",Value="fair"},new SelectListItem{Text="Used",Value="used"} };
        public List<SelectListItem> oses = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Windows", Value = "windows" }, new SelectListItem { Text = "Android", Value = "android" }, new SelectListItem { Text = "IOs / Iphone", Value = "ios" }, new SelectListItem { Text = "Blackberry", Value = "blackberry" }, new SelectListItem { Text = "Other", Value = "other" } };
        // public List<SelectListItem> warranties = new List<SelectListItem> { new SelectListItem { Text="Choose",Value=""},new SelectListItem { Text="Yes",Value=""},new SelectListItem { Text="Automatic",Value="Automatic"},new SelectListItem{Text="Manual",Value="Manual"}};
        public List<SelectListItem> networks = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "GSM", Value = "gsm" }, new SelectListItem { Text = "CDMA", Value = "cdma" }, new SelectListItem { Text = "Other", Value = "other" } };
        public List<SelectListItem> brands = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Samsung", Value = "samsung" }, new SelectListItem { Text = "HTC", Value = "htc" }, new SelectListItem { Text = "Sony", Value = "sony" }, new SelectListItem { Text = "Iphone", Value = "iphone" }, new SelectListItem { Text = "Nokia", Value = "nokia" }, new SelectListItem { Text = "LG", Value = "lg" }, new SelectListItem { Text = "Other", Value = "other" } };
        private PagedList.IPagedList<Cellphone> _foundListings = null;
        public CellphonesSearchViewModel() : base() {}
        public CellphonesSearchViewModel(Category ct) : base(ct) { }
        
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
            var listingsQ = from l in db.Cellphones.Include("Listing.Category").Include("Listing.images").Include("Listing.Owner")
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
           if (!String.IsNullOrEmpty(this.OS))
           {
               listingsQ = listingsQ.Where(c=>c.Listing.Category.Title.ToLower().Contains(this.OS.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Brand))
           {
               listingsQ = listingsQ.Where(c => c.Brand.ToLower().Contains(this.Brand.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.Condition))
           {
               listingsQ = listingsQ.Where(c => c.Condition.ToLower().Contains(this.Condition.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.CModel))
           {
               listingsQ = listingsQ.Where(c => c.CModel.ToLower().Contains(this.CModel.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.NetworkType))
           {
               listingsQ = listingsQ.Where(c => c.NetworkType.ToLower().Contains(this.NetworkType.ToLower()));
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
        public PagedList.IPagedList<Cellphone> foundListings { get { return this._foundListings; } set { this._foundListings = value; } }
        public string OS { get; set; }
        public string Brand { get; set; }
        [Display(Name="Model")]
        public string CModel { get; set; }
        public decimal ScreenSize { get; set; }
        [Display(Name = "Network")]
        public string NetworkType { get; set; }
        public string Condition { get; set; }
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