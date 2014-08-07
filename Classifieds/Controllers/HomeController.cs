using Classifieds.Models;
using Classifieds.Models.SearchViewModels;
using Classifieds.Models.ViewModels;
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
    [RequireHttps]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        CategoryHelper catHelper = new CategoryHelper();

        public HomeController() { }

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
        public ActionResult Email()
        {
            string id = User.Identity.GetUserId();
            var model = new EmailSubscriptionViewModel();
            var cts = catHelper.subCategories(0);

            return View(model);
        }
        [HttpPost]
        public ActionResult Email(EmailSubscriptionViewModel model)
        {
            string id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                foreach (var grp in model.Groups)
                {
                    //check it's not a blank radio value/row
                    if (!String.IsNullOrEmpty(grp.Cycle))
                    {

                        string[] GroupCycle = grp.Cycle.Split(new char[] { '_' });
                        int groupId = Convert.ToInt32(GroupCycle[1]);
                        string cycle = GroupCycle[0];
                        var subQ = from s in db.EmailSubscriptions
                                   where s.Email.ToLower() == model.Email.ToLower() && s.CategoryId == groupId
                                   select s;
                        var emlSub = subQ.FirstOrDefault();
                        if (emlSub == null)
                        {
                                emlSub = new EmailSubscriptions();
                                emlSub.Email = model.Email;
                                emlSub.CategoryId = groupId;
                                emlSub.Name = model.Name;
                                emlSub.Period = cycle;
                                if (!String.IsNullOrEmpty(id))
                                    emlSub.UserId = id;
                                //emlSub.Started = DateTime.Now;
                                db.EmailSubscriptions.Add(emlSub);
                                db.SaveChanges();
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(id))
                                emlSub.UserId = id;
                            if (model.subscriptionType == "subscribe")
                            {
                            emlSub.Name = model.Name;
                            emlSub.Period = cycle;
                            //emlSub.Updated = DateTime.Now;
                            db.SaveChanges();
                            }
                            else if (model.subscriptionType == "unsubscribe")
                            {
                                db.EmailSubscriptions.Remove(emlSub);
                                db.SaveChanges();
                            }
                        }
                        ViewBag.Message = "Your subscription has been updated successfully.";
                    }
                }
            }
            ViewBag.cats = catHelper.subCategories(0);
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}