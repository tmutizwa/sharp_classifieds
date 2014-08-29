using Classifieds.Library;
using Classifieds.Models.EmailViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Classifieds.Models.CreateViewModels;
using Classifieds.Models;
using System.Net;


namespace Classifieds.Controllers
{
    public class FeedbackController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Bugs()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = new CreateBugReportViewModel();
            model.UserId = userId;
            model.Email = user.Email;
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Bugs(CreateBugReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.UserId);
                if (user == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var bug = new Bug();
                bug.Created = DateTime.Now;
                bug.Detail = model.Detail;
                bug.Email = model.Email;
                bug.Title = model.Title;
                bug.UserId = model.UserId;
                db.Bugs.Add(bug);
                db.SaveChanges();
                ViewBag.Message = "Bug submitted successfully. Thank you for your feedback.";
            }
            return View(model);
        }
        [Authorize]
        public ActionResult features()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = new CreateFeatureRequestViewModel();
            model.Email = user.Email;
            model.UserId = userId;
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult features(CreateFeatureRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                if (user == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var fr = new FeatureRequest();
                fr.Created = DateTime.Now;
                fr.Detail = model.Detail;
                fr.Email = model.Email;
                fr.Title = model.Title;
                fr.UserId = userId;
                db.FeatureRequests.Add(fr);
                db.SaveChanges();
                ViewBag.Message = "Feature request submitted successfully. Thank you for your feedback.";
            }
            return View(model);
        }
        public ActionResult Contact()
        {
            var model = new GeneralEmailViewModel();
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                var user = db.Users.Find(userId);
                if(user != null){
                    model.EmailFrom = user.Email;
                    model.Name = user.Alias;
                }
                
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(GeneralEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.EmailTo = "terencemutizwa@gmail.com";
                TMSendEmail.send(model.EmailFrom,model.Name, model.EmailTo, model.Subject, "<strong>Sender : "+model.Name+" ("+model.EmailFrom+")</strong> <br/><br/>"+model.Body,model.CopyEmail);
                ViewBag.Message = "Message sent successfully. Thank you for your message.";
            }
            return View(model);
        }
        public ActionResult reportabuse(int id)
        {
            var listing = db.Listings.Find(id);
            if (listing == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            BadListing model = new BadListing();
            model.Listing = listing;
            model.ListingId = listing.ListingId;
            return View(model);
        }
        [HttpPost]
        public ActionResult reportabuse([Bind(Include = "ListingId,Reason")]BadListing model)
        {
            var listing = db.Listings.Find(model.ListingId);
            model.Listing = listing;
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                if (String.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError("", "You need to log in to report abuse.");
                    return View(model);
                }
                model.Created = DateTime.Now;
                model.UserId = userId;
                db.BadListings.Add(model);
                db.SaveChanges();
                ViewBag.Message = "Report sent successfully. We will review it and take appropriate action.";
                
            }
            return View(model);
        }
    }
}