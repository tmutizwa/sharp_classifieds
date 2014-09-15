using Classifieds.Models.CreateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using PagedList;
using Classifieds.Models.EditViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Classifieds.Areas.Admin.Controllers
{
    public class DealsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        int pagesize = 15;
        public ActionResult Index(string ltitle,string username,int dealId = 0, int listingId=0,int page=1)
        {
            var deal = from d in db.Deals.Include("Listing").Include("Listing.Owner").Include("Listing.images")
                       select d;
            if (listingId > 0)
                deal = deal.Where(d=>d.ListingId == listingId);
            if (dealId > 0)
                deal = deal.Where(d=>d.DealId == dealId);
            if (!String.IsNullOrEmpty(ltitle))
            {
                deal = deal.Where(d => d.Listing.Title.ToLower().Contains(ltitle.ToLower()));
            }
            if (!String.IsNullOrEmpty(username))
            {
                deal = deal.Where(d => d.Listing.Owner.UserName.ToLower().Contains(username.ToLower()));
            }
            deal = deal.OrderByDescending(d =>d.Ends);
            return View(deal.ToPagedList(page,pagesize));
        }
        public ActionResult confirmListing()
        {
            return View();
        }
        [HttpPost]
        public ActionResult confirmListing(int listing = 0)
        {
            var now = DateTime.Now;
            var ln = db.Listings.Find(listing);
            if (ln == null)
            {
                ViewBag.Error = "Invalid Listing ID . ";
            }
            else
            {
                return RedirectToAction("create", new { id=listing});
            }

            return View();
        }
        [HttpGet]
        public ActionResult create(int id = 0)
        {
            var now = DateTime.Now;
            var lnQ = from l in db.Listings.Include("Owner")
                      where l.ListingId == id
                      select l;
            var ln = lnQ.FirstOrDefault();
            if (ln == null)
            {
                return RedirectToAction("confirmListing", new { listing = id});
            }
            else
            {
                 var dealQ = from d in db.Deals
                               where d.ListingId == id
                               select d;
                var deal = dealQ.FirstOrDefault();
                if (deal != null)
                    return RedirectToAction("edit", new { id=deal.DealId});
                return View(new CreateDealViewModel(ln));
            }
        }
        [HttpPost]
        public ActionResult create(CreateDealViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var user = User.Identity.GetUserId();
                    var md = new Deal();
                    var now = DateTime.Now;
                    md.BulkBuyingScore = model.BulkBuyingScore;
                    md.Created = DateTime.Now;
                    md.Duration = model.Duration;
                    md.DurationScore = model.DurationScore;
                    if (model.Starts != null)
                    {
                        md.Starts = model.Starts;
                        md.Ends = model.Starts.Value.AddDays(model.Duration);
                    }
                    else
                    {
                        md.Starts = now;
                        md.Ends = now.AddDays(model.Duration);
                    }
                    md.Listing = db.Listings.Find(model.ListingId);
                    md.ListingId = model.ListingId;
                    md.OutreachScore = model.OutreachScore;
                    md.PriceScore = model.PriceScore;
                    md.QualityScore = model.QualityScore;
                    md.Updated = now;
                    md.Created = now;
                    md.UpdaterId = user;
                    db.Deals.Add(md);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            var t = db.Listings.Find(model.ListingId);
            model.Listing = t;
            return View(model);
        }
        [HttpGet]
        public ActionResult edit(int id)
        {
            var dealQ = from d in db.Deals.Include("Listing").Include("Listing.Owner")
                       where d.DealId == id
                       select d;
            var deal = dealQ.FirstOrDefault();
            if (deal != null)
            {
                var model = new EditDealViewModel(deal);
                            return View(model);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             
        }
        [HttpPost]
        public ActionResult edit(EditDealViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                var deal = db.Deals.Find(model.DealId);
                if (deal == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                deal.BulkBuyingScore = model.BulkBuyingScore;
                deal.Duration = model.Duration;
                deal.DurationScore = model.DurationScore;
                if (model.Starts != null)
                {
                    deal.Starts = model.Starts;
                    deal.Ends = model.Starts.Value.AddDays(model.Duration);
                }
                deal.OutreachScore = model.OutreachScore;
                deal.ListingId = model.ListingId;
                deal.PriceScore = model.PriceScore;
                deal.QualityScore = model.QualityScore;
                deal.Updated = DateTime.Now;
                deal.UpdaterId = user;
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Successfuly updated deal info";
            }
            var listingQ = from l in db.Listings.Include("Owner")
                          where l.ListingId == model.ListingId
                          select l;
            model.Listing = listingQ.FirstOrDefault();
            return View(model);
        }
        public ActionResult delete(int id)
        {
            var dealQ = from d in db.Deals.Include("Listing").Include("Listing.Owner")
                       where d.DealId == id
                       select d;
            var deal = dealQ.FirstOrDefault();
            if (deal != null)
            {
                return View(deal);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }
        [HttpPost]
        public ActionResult delete(Deal model)
        {
            var deal = db.Deals.Find(model.DealId);
            if(deal != null){
                db.Deals.Remove(deal);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}