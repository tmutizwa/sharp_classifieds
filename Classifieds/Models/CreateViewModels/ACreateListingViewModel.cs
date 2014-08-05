using Classifieds.Areas.Admin.Models;
using Classifieds.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Classifieds.Models.CreateViewModels
{
    public abstract class ACreateListingViewModel
    {
        public readonly ApplicationDbContext db = new ApplicationDbContext();
        readonly string _viewFolder = "~/Views/Listing/Create/";
        private Category _category = new Category();
        private readonly int _categoryId;
        public ACreateListingViewModel(int categoryId) {
            if (categoryId > 0)
            {
                this._categoryId = categoryId;
                var ctQ = from l in db.Categories.Include("Parent")
                          where l.CategoryId == categoryId
                          select l;
                _category = ctQ.FirstOrDefault();
                if(_category == null)
                    throw new Exception("Invalid category specified.");
                if (_category.Parent.Title.ToLower().Contains("service"))
                {
                    showCondition = false;
                }
            }
            else
            {
                throw new Exception("Invalid category specified.");
            }
           
        }
        public abstract int AddNewListing();
        //public abstract void updateModel(int ListingId,AListing model);
        [Required]
        [Display(Name = "Category ID")]
        public int CategoryId { get { return this._categoryId; } set { } }
        public Category Category { get { return this._category; } set { } }
        [Required]
        [MaxLength(100,ErrorMessage="Title cannot be more than 100 characters long."), MinLength(3,ErrorMessage="Title cannot be less than 3 characters long.")]
        public string Title { get; set; }
        [MaxLength(500,ErrorMessage="Description cannot be more than 500 characters long."),Display(Name="Description")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        public Decimal Price { get; set; }
        [MaxLength(30,ErrorMessage = "Town cannot be more than 30 characters long.")]
        [Display(Name="City / Town")]
        [Required]
        public string Town { get; set; }
        public string Status { get; set; }
        public string viewFolder { get { return this._viewFolder; } set { } }
        public abstract string view { get; set; }
        Boolean _showPrice = true;
        public Boolean showPrice { get { return this._showPrice; } set { this._showPrice = value; } }
        Boolean _showCondition = true;
        public Boolean showCondition { get { return this._showCondition; } set { this._showCondition = value; } }
    }
}