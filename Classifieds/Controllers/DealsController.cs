using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models;
using PagedList;
using Classifieds.Models.SearchViewModels;

namespace Classifieds.Controllers
{
    public class DealsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        int pageSize = 10;
        public ActionResult Index()
        {
            var now  = DateTime.Now;
            var dealsQ = from d in db.Deals.Include("Listing").Include("Listing.images").Include("Listing.Owner")
                        where d.Starts < now && d.Ends > now
                        select d;
            var deals = dealsQ.OrderByDescending(c=>c.Starts).ToPagedList(1,pageSize);
            return View(deals);
        }
    }
}