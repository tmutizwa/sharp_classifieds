using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classifieds.Library;

namespace Classifieds.Areas.Admin.Models
{
    public class Category
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name="Category ID")]
        public int CategoryId { get; set; }
        //[Display(Name="Category name"),MaxLength(20),MinLength(3)]
        //[RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed in category name.")]
        //[Remote("IsCategoryNameAvailable", "Validation",ErrorMessage="Name already taken. Please try a different Category name.")]
        //[Editable(false)]
        //public string Name { get; set; }
        [Required,MaxLength(100),MinLength(3)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public string Controller { get; set; }
        [Display(Name="Months to expire")]
        public int ExpiresIn { get; set; }
        public string Tags { get; set; }
        public string Status { get; set; }
        public int Order { get; set; }
        
        public string SearchModel { get; set; }
        
        public string ViewModel { get; set; }
        public List<Category> getSubCategories()
        {
                CategoryHelper catHelper = new CategoryHelper();
                return catHelper.subCategories(this.CategoryId);
        }
    }
}