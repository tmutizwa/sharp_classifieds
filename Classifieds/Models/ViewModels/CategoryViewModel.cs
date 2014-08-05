using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Classifieds.Library;

namespace Classifieds.Models.ViewModels
{
    public class CategoryViewModel 
    {
        ApplicationDbContext db = new ApplicationDbContext();
        CategoryHelper ch = new CategoryHelper();
        List<Category> _subCats = new List<Category>();
        public CategoryViewModel(Category model)
        {
            Title = model.Title;
            CategoryId = model.CategoryId;
            Controller = model.Controller;
            this._subCats = ch.subCategories(model.CategoryId);
                       
        }
        public List<Category> subCategories { get{return this._subCats;} set{ this._subCats = value;}}
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Controller { get; set; }
        public string Model { get; set; }

    }
}