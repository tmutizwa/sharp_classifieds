using Classifieds.Areas.Admin.Models;
using Classifieds.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;

namespace Classifieds.Models.SearchViewModels
{
    public class DealSearchViewModel 
    {
        public DealSearchViewModel() {
            var catHelper = new CategoryHelper();
            cats = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "Any category" } };
            var topCategories = catHelper.subCategories(0);
            if (topCategories.Count > 0)
            {
                foreach (var catgry in topCategories)
                {
                    cats.Add(new SelectListItem { Text = catgry.Title, Value = "" + catgry.CategoryId });
                }
            }
            
        }
        public List<SelectListItem> types = new List<SelectListItem> { new SelectListItem { Text = "Choose", Value = "" }, new SelectListItem { Text = "Love", Value = "love" }, new SelectListItem { Text = "Relationship", Value = "relationship" }, new SelectListItem { Text = "Casual", Value = "casual" }, new SelectListItem { Text = "Friendship", Value = "friendship" }, new SelectListItem { Text = "Life partner", Value = "life-partner" }, new SelectListItem { Text = "Anything", Value = "anything" }, new SelectListItem { Text = "Other", Value = "other" } };
        public int ListingId { get; set; }
        [Display(Name="Min. Price")]
        public decimal PriceFrom { get; set; }
        [Display(Name="Max. Price")]
        public decimal PriceTo {get; set;}
        public string Location { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        [Display(Name="Min. Points")]
        public int MinScore { get; set; }
        public List<SelectListItem> cats { get; set; }

    }
}