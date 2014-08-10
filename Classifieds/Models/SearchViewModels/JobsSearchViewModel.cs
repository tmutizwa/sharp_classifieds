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
    public class JobsSearchViewModel : AListingSearchModel 
    {
        string _layout = "~/Views/Shared/Layouts/_BrowseCategory.cshtml";
        string _view = "~/Views/Browse/jobs.cshtml";
        string _controller = "browse";
        string _action = "index";
        string _searchWidgetView = "~/Views/Shared/Widgets/Search/_JobsSearch.cshtml";
        public JobsSearchViewModel() : base() {  }
        public JobsSearchViewModel(Category ct) : base(ct) { }
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Full-time", Value = "full-time" }, new SelectListItem { Text = "Contract", Value = "contract" }, new SelectListItem { Text = "Part-time", Value = "part-time" }, new SelectListItem { Text = "Temporary", Value = "temporary" }, new SelectListItem { Text = "Graduate", Value = "graduate" }, new SelectListItem { Text = "Internship", Value = "internship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Voluntary", Value = "voluntary" } };
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
            var listingsQ = from l in db.Jobs.Include("Listing.Category").Include("Listing.images").Include("Listing.Owner")
                            where l.Listing.Title.ToLower().Contains(text) || l.Listing.Description.ToLower().Contains(text) || l.Listing.Category.Description.ToLower().Contains(text) || l.Listing.Category.Title.ToLower().Contains(text) || l.Listing.Category.Tags.ToLower().Contains(text)
                            select l;
            listingsQ = listingsQ.Where(c => c.Listing.Expires > DateTime.Now && c.Listing.Status == "live" && subCategoryIds.Contains(c.Listing.CategoryId));
            if (this.from > 0)
            {
                listingsQ = listingsQ.Where(l => l.Listing.Price >= this.from);
            }
            if (this.to > 0)
            {
                listingsQ = listingsQ.Where(l => l.Listing.Price <= this.to);
            }
            if (!String.IsNullOrEmpty(this.location))
            {
                listingsQ = listingsQ.Where(c => c.Listing.Town.ToLower().Contains(this.location.ToLower()));
            }
            if (this.MinAge > 0)
            {
                listingsQ = listingsQ.Where(c => c.MinAge >= MinAge);
            }
            if (this.MaxAge > 0)
            {
                listingsQ = listingsQ.Where(c => c.MaxAge <= MaxAge);
            }
            if (this.MinSalary > 0)
            {
                listingsQ = listingsQ.Where(c => c.MinSalary >= MinSalary);
            }
            if (this.MaxSalary > 0)
            {
                listingsQ = listingsQ.Where(c => c.MaxSalary <= MaxSalary);
            }
            if (!String.IsNullOrEmpty(this.Tags))
            {
                listingsQ = listingsQ.Where(c => c.Tags.ToLower().Contains(this.Tags.ToLower()));
            }
            if (!String.IsNullOrEmpty(this.Type))
            {
                listingsQ = listingsQ.Where(c => c.Type.ToLower().Contains(this.Type.ToLower()));
            }
            //set defaults
            this.lorder = "loc";
            this.porder = "price";
            switch (this.order)
            {
                case "loc":
                    listingsQ = listingsQ.OrderBy(c => c.Listing.Town);
                    this.lorder = "loc_desc";
                    break;
                case "loc_desc":
                    listingsQ = listingsQ.OrderByDescending(c => c.Listing.Town);
                    this.lorder = "loc";
                    break;
                case "price":
                    listingsQ = listingsQ.OrderBy(c => c.Listing.Price);
                    this.porder = "price_desc";
                    break;
                case "price_desc":
                    listingsQ = listingsQ.OrderByDescending(c => c.Listing.Price);
                    this.porder = "price";
                    break;
                default:
                    listingsQ = listingsQ.OrderByDescending(c => c.Listing.Updated).ThenByDescending(c => c.Listing.images.Count);
                    this.porder = "price";
                    this.lorder = "loc";
                    break;
            }

            int page = 1;
            if (this.page > 0)
                page = this.page;
            this.foundListings = listingsQ.ToPagedList(page, this.pagesize);
        }
        public PagedList.IPagedList<Job> foundListings { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Tags { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string Type { get; set; }
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