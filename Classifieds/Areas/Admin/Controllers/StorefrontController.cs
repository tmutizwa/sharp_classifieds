using Classifieds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Classifieds.Areas.Admin.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Classifieds.Areas.Admin.Controllers
{
    public class StorefrontController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        int pageSize = 10;
        public ActionResult Index(int page=1)
        {
            var storesQ = from s in db.Storefronts
                          select s;
            var model = storesQ.OrderByDescending(c=>c.Updated).ToPagedList(page, pageSize);
            return View(model);
        }
        public ActionResult check()
        {
            return View();
        }
        [HttpPost]
        public ActionResult check(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                var userQ = from u in db.Users
                            where u.Email.ToLower() == text.ToLower() || u.Alias.ToLower() == text.ToLower()
                            select u;
                var user = userQ.FirstOrDefault();
                if (user == null)
                    ViewBag.error = "No such user.";
                ViewBag.user = user;
            }
            else
            {
                ViewBag.error = "Please enter alias or email.";
            }
            
            return View();
        }
        public ActionResult create(string email)
        {
            var userQ = from u in db.Users
                        where u.Email.ToLower() == email.ToLower()
                        select u;
            var user = userQ.FirstOrDefault();
            var model = new CreateStorefrontViewModel();
            model.Address = user.Address;
            model.Email = user.Email;
            model.Name = user.Alias;
            model.Phone = user.ClassifiedsPhone;
            model.UserId = user.Id;
            return View(model);
        }
        [HttpPost]
        public ActionResult create(CreateStorefrontViewModel model)
        {
            string admin = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var chckQ = from s in db.Storefronts
                            where s.Email.ToLower() == model.Email.ToLower()
                            select s;
                var chkStore = chckQ.FirstOrDefault();
                if (chkStore == null)
                {
                    var store = new Storefront();
                    store.Address = model.Address;
                    store.Created = DateTime.Now;
                    store.Email = model.Email;
                    store.Expires = DateTime.Now.AddMonths(1);
                    store.Name = model.Name;
                    store.OwnerId = model.UserId;
                    store.Phone = model.Phone;
                    store.Status = "live";
                    store.Updated = DateTime.Now;
                    store.UpdaterId = admin;
                    db.Storefronts.Add(store);
                    db.SaveChanges();
                }
                return RedirectToAction("index");
            }
            return View(model);
        }
        public ActionResult edit(int id)
        {
            var store = db.Storefronts.Find(id);
            if (store == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(store);
        }
        [HttpPost]
        public ActionResult edit(Storefront model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }
    }
}