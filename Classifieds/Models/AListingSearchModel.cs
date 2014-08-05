using Classifieds.Areas.Admin.Models;
using Classifieds.Models.ViewModels;
using Classifieds.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classifieds.Library;

namespace Classifieds.Models
{
    public abstract class AListingSearchModel :IValidatableObject
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        int _page = 0;
        int _pagesize = 20;
        public CategoryHelper catHelper = new CategoryHelper();
        private List<Category> _subcats = new List<Category>();
        
        public AListingSearchModel()
        {
            categoriesSelectList = new List<SelectListItem>() {new SelectListItem { Value = "0", Text = "Choose category" } };
            this.topCategories = catHelper.subCategories(0);
            if (topCategories.Count > 0)
            {
                foreach (var catgry in topCategories)
                {
                    categoriesSelectList.Add(new SelectListItem { Text = catgry.Title, Value = "" + catgry.CategoryId });
                }
            }
            this.subCategories = catHelper.subCategories(this.cat);
        }
        public AListingSearchModel(Category category)
        {
            this.Category = category;
            categoriesSelectList = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "Choose category" } };
            if(category != null)
              categoriesSelectList.Add(new SelectListItem { Value = "" +Category.CategoryId, Text = Category.Title, Selected = true });
            this.topCategories = catHelper.subCategories(0);
            if (topCategories.Count > 0)
            {
                foreach (var catgry in topCategories)
                {
                    categoriesSelectList.Add(new SelectListItem { Text = catgry.Title, Value = "" + catgry.CategoryId });
                    if (Category != null && Category.CategoryId == catgry.CategoryId)
                        categoriesSelectList.Remove(new SelectListItem { Text = catgry.Title, Value = "" + catgry.CategoryId  });
                }
                
            }
            this.subCategories = catHelper.subCategories(this.cat);
        }
    
        public abstract void findListings();
        public Category Category { get; set; }
       
        public List<SelectListItem> categoriesSelectList { get; set; }
        public List<Category> topCategories { get; set; }
        public List<Category> subCategories { get { return this._subcats; } set { this._subcats = value; } }
        [Display(Name = "Category")]
        public int cat { get; set; }
        [MaxLength(30, ErrorMessage = "Search text too long, max 30 characters."), MinLength(3, ErrorMessage = "Search text too short, minimum 3 characters required.")]
        //[Required(ErrorMessage="Please enter the search text.")]
        public string text { get; set; }
        [MinLength(3, ErrorMessage = "Town/City too short."), MaxLength(20, ErrorMessage = "Town/City too long, max 20 characters.")]
        [Display(Name = "Town/City")]
        public string location { get; set; }
        [Display(Name = "Min price")]
        public decimal? from { get; set; }
        [Display(Name = "Max price")]
        public decimal? to { get; set; }
        public string order { get; set; }
        //order by location/town
        public string lorder { get;  set; }
        public string porder { get; set; }
        public int page { get { return _page; } set { this._page = value; } }
        public int pagesize { get { return this._pagesize; } set { this._pagesize = value; } }
        public abstract string action { get; set; }
        public abstract string controller { get; set; }
        public string partialSearchView { get; set; }
        public abstract string layout { get; set; }
        public abstract string view { get; set; }
        public abstract string searchWidgetView { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (from > to)
                yield return new ValidationResult("Min Price must be less than Max Price.");
        }

    }
}