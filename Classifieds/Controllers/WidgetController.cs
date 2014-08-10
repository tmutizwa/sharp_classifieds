using Classifieds.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Controllers
{
    public class WidgetController : Controller
    {
        // GET: Widget
        public ActionResult HotDeals()
        {
            return PartialView("~/Views/Shared/Widgets/_HotDeals.cshtml");
        }
        public ActionResult EmailSubscriber()
        {
            var model = new EmailSubscriptionViewModel();
            return PartialView("~/Views/Shared/Widgets/_EmailSubscriber.cshtml",model);
        }
    }
}