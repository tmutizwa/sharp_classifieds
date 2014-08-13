using Classifieds.Areas.Admin.Models;
using Classifieds.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Classifieds.Models.SearchViewModels
{
    public class DealSearchViewModel 
    {
        
        public DealSearchViewModel() {
            var catHelper = new CategoryHelper();
            var topCategories = catHelper.subCategories(0);
            if (topCategories.Count > 0)
            {
                foreach (var catgry in topCategories)
                {
                    cats.Add(new SelectListItem { Text = catgry.Title, Value = "" + catgry.CategoryId });
                }
            }
            
        }
        public List<SelectListItem> cats = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "Any category" } };
        public int ListingId { get; set; }
        [Display(Name="Min. Price")]
        public decimal PriceFrom { get; set; }
        [Display(Name="Max. Price")]
        public decimal PriceTo {get; set;}
        public string Location { get; set; }
        public int Category { get; set; }
        [Display(Name="Min. Points")]
        public int MinScore { get; set; }

    }
}