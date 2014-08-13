using Classifieds.Areas.Admin.Models;
using Classifieds.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Library
{
    public class CategoryHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        //get subCategoryIds of this category
        public SortedDictionary<string, int> categoryParents(int? catId)
        {
            
            SortedDictionary<string, int> crumbs = new SortedDictionary<string, int>();
            int? parentId = catId;
            do
            {
                var parenting = from cat in db.Categories
                                where (cat.CategoryId == parentId)
                                select cat;
                Category parent = parenting.FirstOrDefault();
                if (parent != null)
                {
                    crumbs.Add(parent.Title, parent.CategoryId);
                    parentId = parent.ParentId;
                }

            }
            while (parentId != null && parentId > 0);
            
            return crumbs;
        }
        public List<Category> subCategories(int cat)
        {
            
            List<Category> cats = new List<Category>();
            var catQ = from c in db.Categories
                       where c.Status.ToLower() == "live"
                       select c;
            catQ = catQ.OrderBy(c => c.Order).ThenBy(c=>c.Title);
            if (cat <=0 )
            {
                catQ = catQ.Where(l =>l.ParentId == null);
            }
            else
            {
                catQ = catQ.Where(c => c.ParentId == cat);
            }
            var categories = catQ.ToList();
            if (categories != null)
                cats = categories;
            return cats;
        }
        public Boolean isParentCategory(int c)
        {
            Boolean res = false;
            var cats = db.Categories.Where(t => t.ParentId == c);
            if (cats.ToList().Count() > 0)
                res = true;
            return res;
        }
        public List<Category> allSubCategories(int cat)
        {
            List<Category> kids = new List<Category>();
            Stack<Category> nods = new Stack<Category>();
            var rootNod = db.Categories.Find(cat);
            if(rootNod == null){
                return kids;
            }
            nods.Push(rootNod);
            do
            {
                Category currentNode = nods.Pop();
                var cats = from c in db.Categories
                           where c.ParentId == currentNode.CategoryId
                           select c;
                List<Category> categories = cats.ToList();
                if (categories.Count() == 0)
                {
                    //this is a leaf node so add it to kids list
                    kids.Add(currentNode);
                }
                else
                {
                    //add these to nodes stack for future consideration
                    foreach (var nodcat in categories)
                    {
                        nods.Push(nodcat);
                    }
                }
                
            } while (nods.Count > 0);
            
            return kids;
        }

        public int[] allSubCategoryIds(int cat)
        {
            List<Category> kids = new List<Category>();
            Stack<Category> nods = new Stack<Category>();
            var rootNod = db.Categories.Find(cat);
            if(rootNod == null){
                return new int[]{};
            }
            nods.Push(rootNod);
            do
            {
                Category currentNode = nods.Pop();
                var cats = from c in db.Categories
                           where c.ParentId == currentNode.CategoryId
                           select c;
                List<Category> categories = cats.ToList();
                if (categories.Count() == 0)
                {
                    //this is a leaf node so add it to kids list
                    kids.Add(currentNode);
                }
                else
                {
                    //add these to nodes stack for future consideration
                    foreach (var nodcat in categories)
                    {
                        nods.Push(nodcat);
                    }
                }

            } while (nods.Count > 0);
            int[] catIds = new int[kids.Count()];
            int i= 0;
            foreach (var c in kids)
            {
                catIds[i] = c.CategoryId;
                i++;
            }
                return catIds;
        }
        public static List<SelectListItem> topCategoriesSelectList(){
            ApplicationDbContext db = new ApplicationDbContext();
            List<SelectListItem> list = new List<SelectListItem>();
            var q = from c in db.Categories
                    where c.ParentId == null && c.Status == "live"
                    orderby c.Order
                    select c;
            foreach (var cat in q)
            {
                list.Add(new SelectListItem { Text = cat.Title, Value = ""+cat.CategoryId });
            }
            return list;
        }
    }
}