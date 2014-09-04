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
    public class AutosSearchViewModel  : AListingSearchModel 
    {
        string _controller = "browse";
        string _action = "index";
        string _view = "~/Views/Browse/autos.cshtml";
        string _layout = "~/Views/Shared/Layouts/_BrowseCategory.cshtml";
        string _searchWidgetView = "~/Views/Shared/Widgets/Search/_AutosSearch.cshtml";
        public List<SelectListItem> fuelTypes = new List<SelectListItem> {new SelectListItem { Text="Any",Value=""},new SelectListItem { Text="Petrol",Value="Petrol"},new SelectListItem{Text="Diesel",Value="Diesel"},new SelectListItem{Text="Electric",Value="Electric"} };
        public List<SelectListItem> transmissions = new List<SelectListItem> { new SelectListItem { Text="Any",Value=""},new SelectListItem { Text="Automatic",Value="Automatic"},new SelectListItem{Text="Manual",Value="Manual"}};
        public List<SelectListItem> conditions = new List<SelectListItem> { new SelectListItem { Text = "Any", Value = "" }, new SelectListItem { Text = "Brand new", Value = "brand new" }, new SelectListItem { Text = "New", Value = "new" }, new SelectListItem { Text = "Very good", Value = "very good" }, new SelectListItem { Text = "Good", Value = "good" }, new SelectListItem { Text = "Neat", Value = "neat" }, new SelectListItem { Text = "Used", Value = "used" }, new SelectListItem { Text = "So so", Value = "so so" }, new SelectListItem { Text = "Old", Value = "old" } };
        private PagedList.IPagedList<Mota> _foundListings = null;
        public AutosSearchViewModel() : base() {}
        public AutosSearchViewModel(Category ct) : base(ct) { }
        
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
            var listingsQ = from l in db.Motas.Include("Listing.Category").Include("Listing.images").Include("Listing.Owner")
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
           if (!String.IsNullOrEmpty(this.make))
           {
               listingsQ = listingsQ.Where(c=>c.Make.ToLower().Contains(this.make.ToLower()));
               //listingsQ = listingsQ.Join(db.Motas,
               //    l => l.ListingId,
               //    m => m.ListingId,
               //    (l, m) => new { l,m})
               //    .Where(z=>z.m.Make.ToLower().Contains(this.make.ToLower()))
               //    .Select(z=>z.l);
           }
           if (!String.IsNullOrEmpty(this.cmodel))
           {
               listingsQ = listingsQ.Where(c => c.CarModel.ToLower().Contains(this.cmodel.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.fuelType))
           {
               listingsQ = listingsQ.Where(c => c.FuelType.ToLower().Contains(this.fuelType.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.transmission))
           {
               listingsQ = listingsQ.Where(c => c.Transmission.ToLower().Contains(this.transmission.ToLower()));
           }
           if (!String.IsNullOrEmpty(this.condition))
           {
               listingsQ = listingsQ.Where(c => c.Condition.ToLower().Contains(this.condition.ToLower()));
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
        public PagedList.IPagedList<Mota> foundListings { get { return this._foundListings; } set { this._foundListings = value; } }
        [Display(Name = "Make")]
        public string make { get; set; }
        [Display(Name = "Model")]
        public string cmodel { get; set; }
        public string year { get; set; }
        [Display(Name="Mileage from")]
        public decimal mileage { get; set; }
        [Display(Name="Fuel type")]
        public string fuelType { get; set; }
        [Display(Name="Gearbox")]
        public string transmission { get; set; }
        [Display(Name="Condition")]
        public string condition { get; set; }
        [Display(Name = "Engine Size")]
        public decimal engineSize { get; set; }
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