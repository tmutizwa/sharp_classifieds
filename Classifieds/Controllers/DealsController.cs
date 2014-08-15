using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models;
using PagedList;
using Classifieds.Models.SearchViewModels;
using Classifieds.Library;

namespace Classifieds.Controllers
{
    public class DealsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        int pageSize = 7;
        public ActionResult Index(DealSearchViewModel model,string order,int page = 1)
        {
            var now = DateTime.Now;
            var dealsQ = from d in db.Deals.Include("Listing").Include("Listing.images").Include("Listing.Owner").Include("Listing.Category")
                         where d.Starts < now && d.Ends > now
                         select d;
            if (ModelState.IsValid)
            {
                
                if (model.PriceFrom > 0)
                    dealsQ = dealsQ.Where(d => d.Listing.Price >= model.PriceFrom);
                if (model.PriceTo > 0)
                    dealsQ = dealsQ.Where(d => d.Listing.Price <= model.PriceTo);
                if (!String.IsNullOrEmpty(model.Location))
                    dealsQ = dealsQ.Where(d => d.Listing.Town.ToLower().Contains(model.Location.ToLower()));
                if (model.MinScore > 0)
                    dealsQ = dealsQ.Where(d => d.TotalScore <= model.MinScore);
                if (model.Category > 0)
                {
                    var catHelper = new CategoryHelper();
                    var cats = catHelper.allSubCategoryIds(model.Category);
                    dealsQ = dealsQ.Where(d=>cats.Contains(d.Listing.CategoryId));
                }
            }
            //ordering
            string priceOrder = "price";
            string dateOrder = "date";
            string categoryOrder = "category";
            string pointsOrder = "points";
            switch (order)
            {
                case "price":
                    dealsQ = dealsQ.OrderBy(c => c.Listing.Price);
                    priceOrder = "price_desc";
                    break;
                case "price_desc":
                    priceOrder = "price";
                    dealsQ = dealsQ.OrderByDescending(c=>c.Listing.Price);
                    break;
                case "date":
                    dateOrder = "date_desc";
                    dealsQ = dealsQ.OrderBy(c=>c.Starts);
                    break;
                case "date_desc":
                    dateOrder = "date";
                    dealsQ = dealsQ.OrderByDescending(c=>c.Starts);
                    break;
                case "category" :
                    categoryOrder = "category_desc";
                    dealsQ = dealsQ.OrderBy(c=>c.Listing.CategoryId);
                    break;
                case "category_desc":
                    categoryOrder = "category";
                    dealsQ = dealsQ.OrderByDescending(c=>c.Listing.CategoryId);
                    break;
                case "points":
                    pointsOrder = "points_desc";
                    dealsQ = dealsQ.OrderBy(c => c.TotalScore);
                    break;
                case "points_desc":
                    pointsOrder = "points";
                    dealsQ = dealsQ.OrderByDescending(c => c.TotalScore);
                    break;
                default:
                    dealsQ = dealsQ.OrderByDescending(c => c.Starts);
                    break;
            }
            var deals = dealsQ.ToPagedList(page, pageSize);
            ViewBag.dealSearchModel = model;
            ViewBag.priceOrder = priceOrder;
            ViewBag.dateOrder = dateOrder;
            ViewBag.categoryOrder = categoryOrder;
            ViewBag.pointsOrder = pointsOrder;
            return View(deals);
        }
    }
}