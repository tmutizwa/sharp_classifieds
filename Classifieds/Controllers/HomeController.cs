using Classifieds.Models;
using Classifieds.Models.SearchViewModels;
using Classifieds.Models.ViewModels;
using Classifieds.Models.EmailViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Classifieds.Library;
using Microsoft.AspNet.Identity;

namespace Classifieds.Controllers
{
   // [RequireHttps]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        CategoryHelper catHelper = new CategoryHelper();
        EmailSubscriptionListViewModel subscriBerWidget = new EmailSubscriptionListViewModel();

        public HomeController() {}

        public ActionResult Index()
        {
            AListingSearchModel listingSearchModel = new ListingSearchViewModel();
            ViewBag.searchViewModel = listingSearchModel;
            return View(listingSearchModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FAQs()
        {
            return View();
        }

       
    }
}