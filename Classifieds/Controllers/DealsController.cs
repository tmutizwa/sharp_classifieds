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
        int pageSize = 10;
        public ActionResult Index(DealSearchViewModel model)
        {
            var now = DateTime.Now;
            var dealsQ = from d in db.Deals.Include("Listing").Include("Listing.images").Include("Listing.Owner")
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
            var deals = dealsQ.OrderByDescending(c => c.Starts).ToPagedList(1, pageSize);
            ViewBag.dealSearchModel = model;
            return View(deals);
        }
    }
}