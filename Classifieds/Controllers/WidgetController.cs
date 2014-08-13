using Classifieds.Models;
using Classifieds.Models.SearchViewModels;
using Classifieds.Models.ViewModels;
using Classifieds.Models.WidgetViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Controllers
{
    public class WidgetController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult HotDeals()
        {
            
            return PartialView("~/Views/Shared/Widgets/_HotDeals.cshtml",new HotDealsViewModel());
        }
        public ActionResult EmailSubscriber()
        {
            var model = new EmailSubscriptionViewModel();
            return PartialView("~/Views/Shared/Widgets/_EmailSubscriber.cshtml",model);
        }
        public ActionResult DealSearch()
        {
            var model = new DealSearchViewModel();
            return PartialView("~/Views/Shared/Widgets/Search/_DealSearch.cshtml",model);
        }
        public ActionResult MainSearch()
        {
            var model = new ListingSearchViewModel();
            return PartialView("~/Views/Shared/Widgets/Search/_MainSearch.cshtml",model);
        }
    }
}