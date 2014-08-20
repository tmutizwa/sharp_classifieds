using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Classifieds.Models.ViewModels;
using Classifieds.Models;
using Classifieds.Library;
using Classifieds.Models.CreateViewModels;
using System.Net;

namespace Classifieds.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        CategoryHelper catHelper = new CategoryHelper();
        EmailSubscriptionListViewModel subscriBerWidget = new EmailSubscriptionListViewModel();
        // GET: Email
        public ActionResult subscribe(string s)
        {
            if (!String.IsNullOrEmpty(s) && s == "unsubscribe")
                ViewBag.Message = "Successfully unsubscribed from all lists.";
            return View(new EmailSubscriptionListViewModel());
        }
        [HttpPost]
        public ActionResult subscribe(EmailSubscriptionListViewModel model)
        {
            string id = User.Identity.GetUserId();
            ViewBag.cats = catHelper.subCategories(0);
            if (ModelState.IsValid)
            {
                //are they unsubscribing from all lists?
                if (model.subscriptionType == "unsubscribe")
                {
                    var csubQ = from s in db.EmailSubscriptions
                                where s.Email == model.Email
                                select s;
                    if (!String.IsNullOrEmpty(id))
                        csubQ = csubQ.Where(s=>s.UserId == id);
                    var csub = csubQ.ToList();
                    if (csub != null)
                    {
                        foreach (var sb in csub)
                        {
                            db.EmailSubscriptions.Remove(sb);
                        }
                        db.SaveChanges();
                        return RedirectToAction("subscribe", new { s="unsubscribe"});
                    }

                }
                else
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
                                emlSub.Started = DateTime.Now;
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
                                    emlSub.Updated = DateTime.Now;
                                    db.SaveChanges();
                                }
                            }
                            ViewBag.Message = "Your subscription has been updated successfully.";
                        }
                    }
                }
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult categorysubscriber(EmailSubscriptionViewModel model){
            if (ModelState.IsValid)
            {
                string id = User.Identity.GetUserId();
                var subQ = from s in db.EmailSubscriptions
                           where s.Email.ToLower() == model.Email.ToLower() && s.CategoryId == model.GroupId
                           select s;
                var sub = subQ.FirstOrDefault();
                if (sub != null)
                {
                    if (!String.IsNullOrEmpty(model.Choice) && model.Choice.ToLower() == "unsubscribe")
                    {
                        db.EmailSubscriptions.Remove(sub);
                        db.SaveChanges();
                        ViewBag.Message = "Successfully updated your subsciption.";
                    }
                    else
                    {
                        sub.Period = model.Cycle;
                        sub.Name = model.Name;
                        if (!String.IsNullOrEmpty(id))
                            sub.UserId = id;
                        sub.Updated = DateTime.Now;
                        db.SaveChanges();
                        ViewBag.Message = "Successfully updated your subsciption.";
                    }
                }
                else
                {
                    sub = new EmailSubscriptions();
                    if (!String.IsNullOrEmpty(id))
                        sub.UserId = id;
                    sub.Email = model.Email;
                    sub.CategoryId = model.GroupId;
                    sub.Name = model.Name;
                    sub.Period = model.Cycle;
                    sub.Started = DateTime.Now;
                    db.EmailSubscriptions.Add(sub);
                    db.SaveChanges();
                    ViewBag.Message = "Successfully updated your subsciption.";
                }
                
            }
            return View(model);
        }

        public ActionResult contactseller(int id)
        {
            var listing = db.Listings.Find(id);
            var userId = User.Identity.GetUserId();
            var model = new CreateMessageViewModel();
            if (listing == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = db.Users.Find(userId);
            if (user != null)
            {
                model.Phone = user.ClassifiedsPhone;
                model.SenderEmail = user.Email;
                model.SenderName = user.Alias ?? user.FullName;
            }
            ViewBag.Listing = listing;
            model.ListingId = listing.ListingId;
            model.Subject = "About listing #" + listing.ListingId + " - " + listing.Title;
            return View(model);
        }
        [HttpPost]
        public ActionResult contactseller(CreateMessageViewModel model)
        {
            var listing = db.Listings.Find(model.ListingId);
                if (listing == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                
                var userId = User.Identity.GetUserId();
                Message message = new Message();
                message.Detail = model.Detail;
                message.ListingId = model.ListingId;
                message.Phone = model.Phone;
                message.SenderEmail = model.SenderEmail;
                if(!String.IsNullOrEmpty(userId))
                    message.SenderId = userId;
                message.SenderName = model.SenderName;
                message.Subject = model.Subject;
                message.Type = "listing";
                message.Created = DateTime.Now;
                message.Updated = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                ViewBag.Message = "Message sent to seller successfully. Thank you for your message. ";
                
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    model.ListingId = listing.ListingId;
                    model.Phone = user.ClassifiedsPhone;
                    model.SenderEmail = user.Email;
                    model.SenderName = user.Alias ?? user.FullName;
                }
               
                model.Subject = "About listing #" + listing.ListingId + " - " + listing.Title;
            }
            ViewBag.Listing = listing;
            return View(model);
        }
    }
}